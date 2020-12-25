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
    public class EndorsementDataStore : BaseDataStore, IEndorsementDataStore
    {
        public EndorsementDataStore(ACICDBContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public Endorsement SaveDriverEndorsement(int accountId, int driverId, List<DriverCoverage> driverCoverages)
        {
            foreach(var coverage in driverCoverages)
            {
                // select latest policy under coverage type
                var policy = _context.Policy.Where(p => p.AccountId == accountId && p.CoverageTypes == coverage.CoverageTypeId && p.Expiration > DateTime.Now).FirstOrDefault();
                if(policy != null)
                {
                    var dbEndorsement = new Domain.Models.Endorsement
                    {
                        AccountId = accountId,
                        PolicyId = policy.PolicyId,
                        Action = "ADD",
                        Type = "Driver",
                        Effective = policy.Effective,
                        CoverageTypes = coverage.CoverageTypeId,
                        Description = "Initial Binding",
                        DriverId = driverId,
                        Premium = coverage.Premium,
                        SurplusTax = coverage.PremiumTax,
                        BrokerFees = coverage.BrokerFee,
                        TotalPremium = coverage.TotalAmount,
                        Status = "Reconciled",
                        DateCreated = DateTime.Now
                    };

                    _context.Endorsement.Add(dbEndorsement);
                    _context.SaveChanges();
                }
            }
            return new Endorsement();
        }

        public Endorsement SaveVehicleEndorsement(int accountId, int vehicleId, string type, List<VehicleCoverage> vehicleCoverages)
        {
            foreach (var coverage in vehicleCoverages)
            {
                // select latest policy under coverage type
                var policy = _context.Policy.Where(p => p.AccountId == accountId && p.CoverageTypes == coverage.CoverageTypeId && p.Expiration > DateTime.Now).FirstOrDefault();
                if (policy != null)
                {
                    var dbEndorsement = new Domain.Models.Endorsement
                    {
                        AccountId = accountId,
                        PolicyId = policy.PolicyId,
                        Action = "ADD",
                        Type = type,
                        Effective = policy.Effective,
                        CoverageTypes = coverage.CoverageTypeId,
                        Description = "Initial Binding",
                        DriverId = vehicleId,
                        Premium = coverage.Premium,
                        SurplusTax = coverage.PremiumTax,
                        BrokerFees = coverage.BrokerFee,
                        TotalPremium = coverage.TotalAmount,
                        Status = "Reconciled",
                        DateCreated = DateTime.Now
                    };

                    _context.Endorsement.Add(dbEndorsement);
                    _context.SaveChanges();
                }
            }
            return new Endorsement();
        }
    }
}
