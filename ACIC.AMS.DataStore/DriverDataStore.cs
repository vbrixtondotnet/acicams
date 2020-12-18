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
    public class DriverDataStore : BaseDataStore, IDriverDataStore
    {
        IEndorsementDataStore endorsementDataStore;
        public DriverDataStore(ACICDBContext context, IEndorsementDataStore endorsementDataStore, IMapper mapper) : base(context, mapper)
        {
            this.endorsementDataStore = endorsementDataStore;
        }

        public SPRowCountResult DeleteDriver(int driverId)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("@DriverId", driverId);

            return this.ExecuteQuery<SPRowCountResult>("[dbo].[DeleteDriver]", parameters).FirstOrDefault();
        }

        public List<DriverHistory> GetDriverHistories(int driverId)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("@DriverId", driverId);

            return this.ExecuteQuery<DriverHistory>("[dbo].[GetDriverHistory]", parameters);
        }

        public List<Driver> GetDrivers(int accountId)
        {
            List<Driver> retval = new List<Driver>();
            var dbDrivers = _context.Driver.Where(s => s.AccountId == accountId).ToList();
            dbDrivers.ForEach(d => {
                retval.Add(_mapper.Map<Dto.Driver>(d));
            });

            return retval;
        }

        public DriverEndorsement Save(DriverEndorsement driverEndorsement)
        {
            if (driverEndorsement.DriverCoverages.Count > 0)
            {
                Domain.Models.Driver dbDriver = new Domain.Models.Driver
                {
                    AccountId = driverEndorsement.AccountId,
                    Cdlnumber = driverEndorsement.Cdlnumber,
                    DateHired = driverEndorsement.DateHired,
                    CdlyearLic = driverEndorsement.CdlyearLic,
                    Dob = driverEndorsement.Dob,
                    Email = driverEndorsement.Email,
                    State = driverEndorsement.State,
                    FirstName = driverEndorsement.FirstName,
                    LastName = driverEndorsement.LastName,
                    Middle = driverEndorsement.Middle,
                    Notes = driverEndorsement.Notes,
                    OwnerOperator = driverEndorsement.OwnerOperator,
                    Phone = driverEndorsement.Phone,
                    Terminated = driverEndorsement.Terminated,
                    Active = true,
                    DateCreated = DateTime.Now
                };

                _context.Driver.Add(dbDriver);
                _context.SaveChanges();

                //process endorsement
                endorsementDataStore.SaveDriverEndorsement((int)driverEndorsement.AccountId, dbDriver.DriverId, driverEndorsement.DriverCoverages);
                return driverEndorsement;
            }

            return null;

        }
            
    }
}
