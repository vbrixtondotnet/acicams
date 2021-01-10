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

        public bool DriverExists(string firstName, string lastName, string cdlNumber, string state)
        {
            var dbDriver = _context.Driver.Where(d => (d.FirstName != null && d.FirstName.ToLower().Trim() == firstName.ToLower().Trim()) &&
                                                      (d.LastName != null && d.LastName.ToLower().Trim() == lastName.ToLower().Trim()) &&
                                                      (d.Cdlnumber != null && d.Cdlnumber.ToLower().Trim() == cdlNumber.ToLower().Trim()) &&
                                                      (d.State != null && d.State.ToLower().Trim() == state.ToLower().Trim())
                                                ).FirstOrDefault();

            return dbDriver != null;
        }

        public List<DriverHistory> GetDriverHistories(int driverId)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("@DriverId", driverId);

            return this.ExecuteQuery<DriverHistory>("[dbo].[GetDriverHistory]", parameters);
        }

        public List<Driver> GetDrivers()
        {
            List<Driver> drivers = new List<Driver>();
            var dbDrivers = _context.Driver.Where(d => d.Active).ToList();
            dbDrivers.ForEach(d => {
                drivers.Add(_mapper.Map<Driver>(d));
            });
            return drivers;
        }

        public List<Driver> GetDriversByAccount(int accountId)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("@AccountId", accountId);

            return this.ExecuteQuery<Driver>("[dbo].[GetDrivers]", parameters);
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

                Domain.Models.AccountDriver dbAccountDriver = new Domain.Models.AccountDriver
                {
                    AccountId = (int)driverEndorsement.AccountId,
                    DriverId = dbDriver.DriverId,
                    Active = true,
                    DateCreated = DateTime.Now
                };

                // process accountDriver
                _context.AccountDriver.Add(dbAccountDriver);
                _context.SaveChanges();

                //process endorsement
                endorsementDataStore.SaveDriverEndorsement((int)driverEndorsement.AccountId, dbDriver.DriverId, driverEndorsement.DriverCoverages);
                return driverEndorsement;
            }

            return null;

        }

        public Driver Update(Driver driver)
        {
            var dbDriver = _context.Driver.Where(a => a.DriverId == driver.DriverId).AsNoTracking().FirstOrDefault();

            var updatedDriver = _mapper.Map<Domain.Models.Driver>(driver);
            updatedDriver.DateModified = DateTime.Now;
            updatedDriver.DateCreated = dbDriver.DateCreated;
            _context.Update(updatedDriver);
            _context.SaveChanges();

            return driver;
        }
    }
}
