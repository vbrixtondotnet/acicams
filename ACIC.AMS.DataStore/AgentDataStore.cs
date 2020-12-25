using ACIC.AMS.DataStore.Interfaces;
using ACIC.AMS.Dto;
using ACIC.AMS.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ACIC.AMS.Services;

namespace ACIC.AMS.DataStore
{
    public class AgentDataStore : BaseDataStore, IAgentDataStore
    {
        public AgentDataStore(ACICDBContext context, IMapper mapper) : base(context, mapper)
        {

        }
     
        public List<AgentUser> GetAll()
        {
            List<AgentUser> dbAgentUsers = this.ExecuteQuery<AgentUser>("[dbo].[GetAgents]", null);
            dbAgentUsers.ForEach(u => { 
                u.Password = EncryptionService.Decrypt(u.Password);
            });

            return dbAgentUsers;
        }

        public AgentUser Save(AgentUser agentUser)
        {
            if (agentUser.Id == 0)
            {
                //user account
                Domain.Models.User dbUser = new Domain.Models.User();

                dbUser.FirstName = agentUser.FirstName;
                dbUser.LastName = agentUser.LastName;
                dbUser.MiddleName = agentUser.MiddleName;
                dbUser.EmailAddress = agentUser.EmailAddress;
                dbUser.Role = "Agent";
                dbUser.Password = EncryptionService.Encrypt(agentUser.Password);
                dbUser.Active = true;
                dbUser.DateCreated = DateTime.Now;
                _context.User.Add(dbUser);

                _context.SaveChanges();

                //agent account;
                Domain.Models.Agent dbAgent = new Domain.Models.Agent();
                dbAgent.UserId = dbUser.Id;
                dbAgent.BrokerFeeSplit = agentUser.BrokerFeeSplit;
                dbAgent.CommFixedAmount = agentUser.CommFixedAmount;
                dbAgent.CommPaymentPlan = agentUser.CommPaymentPlan;
                dbAgent.CommSplitNew = agentUser.CommSplitNew;
                dbAgent.CommSplitRenew = agentUser.CommSplitRenew;
                dbAgent.DateCreated = DateTime.Now;
                dbAgent.Notes = agentUser.Notes;
                _context.Agent.Add(dbAgent);
                _context.SaveChanges();
            }
            else{

                Domain.Models.User dbUser = _context.User.Where(u => u.Id == agentUser.UserId).FirstOrDefault();

                dbUser.FirstName = agentUser.FirstName;
                dbUser.LastName = agentUser.LastName;
                dbUser.MiddleName = agentUser.MiddleName;
                dbUser.EmailAddress = agentUser.EmailAddress;
                dbUser.Password = EncryptionService.Encrypt(agentUser.Password);
                dbUser.DateModified = DateTime.Now;
                _context.User.Update(dbUser);

                var dbAgent = _context.Agent.Where(a => a.AgentId == agentUser.Id).FirstOrDefault();
                dbAgent.BrokerFeeSplit = agentUser.BrokerFeeSplit;
                dbAgent.CommFixedAmount = agentUser.CommFixedAmount;
                dbAgent.CommPaymentPlan = agentUser.CommPaymentPlan;
                dbAgent.CommSplitNew = agentUser.CommSplitNew;
                dbAgent.CommSplitRenew = agentUser.CommSplitRenew;
                dbAgent.Notes = agentUser.Notes;
                dbAgent.DateModified = DateTime.Now;

                _context.Update(dbAgent);
                _context.SaveChanges();
            }

            return agentUser;
        }
    }
}
