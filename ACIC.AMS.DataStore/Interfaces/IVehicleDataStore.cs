using ACIC.AMS.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.DataStore.Interfaces
{
    public interface IVehicleDataStore
    {
        List<Vehicle> GetVehicles(int accountId);
        List<VehicleMake> GetVehicleMakes();

        VehicleEndorsement Save(VehicleEndorsement vehicleEndorsement);
        List<VehicleHistory> GetVehicleHistory(int vehicleId);
        Vehicle UpdateVehicle(Vehicle vehicle); 
        SPRowCountResult DeleteVehicle(int vehicleId);
    }
}
