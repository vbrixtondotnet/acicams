using ACIC.AMS.DataStore.Interfaces;
using ACIC.AMS.Dto;
using ACIC.AMS.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ACIC.AMS.DataStore
{
    public class VehicleDataStore : BaseDataStore, IVehicleDataStore
    {
        IEndorsementDataStore endorsementDataStore;
        public VehicleDataStore(ACICDBContext context, IEndorsementDataStore endorsementDataStore, IMapper mapper) : base(context, mapper)
        {
            this.endorsementDataStore = endorsementDataStore;
        }

        public SPRowCountResult DeleteVehicle(int vehicleId)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("@Vehicle", vehicleId);

            return this.ExecuteQuery<SPRowCountResult>("[dbo].[DeleteVehicle]", parameters).FirstOrDefault();
        }
    

        public List<VehicleHistory> GetVehicleHistory(int vehicleId)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("@VehicleId", vehicleId);

            return this.ExecuteQuery<VehicleHistory>("[dbo].[GetVehicleHistory]", parameters);
        }

        public List<VehicleMake> GetVehicleMakes()
        {
            List<VehicleMake> retval = new List<VehicleMake>();
            var dbVehicleMakes = _context.DdVehicleMake.ToList();
            dbVehicleMakes.ForEach(vm => {
                retval.Add(_mapper.Map<VehicleMake>(vm));
            });
            return retval;
        }

        public List<Vehicle> GetVehicles(int accountId)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("@AccountId", accountId);

            return this.ExecuteQuery<Vehicle>("[dbo].[GetVehicles]", parameters);
        }

        public VehicleEndorsement Save(VehicleEndorsement vehicleEndorsement)
        {
            if (vehicleEndorsement.VehicleCoverages.Count > 0)
            {
                Domain.Models.Vehicle vehicle = new Domain.Models.Vehicle();
                vehicle.AccountId = vehicleEndorsement.AccountId;
                vehicle.Vin = vehicleEndorsement.Vin;
                vehicle.VUnit = (vehicleEndorsement.Type == 1 || vehicleEndorsement.Type == 3) ? vehicleEndorsement.VUnit : null;
                vehicle.Type = vehicleEndorsement.Type;
                vehicle.VYear = vehicleEndorsement.VYear;
                vehicle.Make = vehicleEndorsement.Make;
                vehicle.Model = vehicleEndorsement.Model;
                vehicle.Pdvalue = vehicleEndorsement.Pdvalue;
                vehicle.BankId = vehicleEndorsement.NotOnLien ? null : vehicleEndorsement.BankId;
                vehicle.AccountNo = vehicleEndorsement.NotOnLien ? null : vehicleEndorsement.AccountNo;
                vehicle.OwnerOperator = vehicleEndorsement.DriverOwner ? null : vehicleEndorsement.OwnerOperator;
                vehicle.Notes = vehicleEndorsement.Notes;
                vehicle.DriverId = vehicleEndorsement.DriverId;
                vehicle.PolicyInclude = true;
                vehicle.IsDriverOwner = vehicleEndorsement.DriverOwner;
                vehicle.NotOnLien = vehicleEndorsement.NotOnLien;
                vehicle.DateCreated = DateTime.Now;
                _context.Vehicle.Add(vehicle);
                _context.SaveChanges();

                //process endorsement
                var endorsementType = vehicleEndorsement.Type == 1 ? "Tractor" : (vehicleEndorsement.Type == 2 ? "Trailer" : "Truck");
                endorsementDataStore.SaveVehicleEndorsement((int)vehicle.AccountId, vehicle.Id, endorsementType, vehicleEndorsement.VehicleCoverages);
                return vehicleEndorsement;
            }

            return null;
        }

      
        public Vehicle UpdateVehicle(Vehicle vehicle)
        {
            var dbDriver = _context.Vehicle.Where(a => a.Id == vehicle.Id).AsNoTracking().FirstOrDefault();

            var updatedDriver = _mapper.Map<Domain.Models.Vehicle>(vehicle);
            updatedDriver.DateModified = DateTime.Now;
            updatedDriver.DateCreated = dbDriver.DateCreated;
            updatedDriver.BankId = vehicle.NotOnLien ? null : vehicle.BankId;
            updatedDriver.AccountNo = vehicle.NotOnLien ? null : vehicle.AccountNo;
            updatedDriver.OwnerOperator = vehicle.IsDriverOwner ? null : vehicle.OwnerOperator;

            _context.Update(updatedDriver);
            _context.SaveChanges();

            return vehicle;
        }
    }
}
