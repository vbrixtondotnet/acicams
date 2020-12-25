
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ACIC.AMS.Dto
{
    public partial class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string EmailAddress { get; set; } 
        public string Password { get; set; }
        public string Role { get; set; }
        public bool Active { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string FullName { get {
                return $"{this.FirstName} {this.MiddleName} {this.LastName}";
            } }
    }

    public class AgentUser
    {
        private double? commSplitNew;
        private double? commSplitRenew;
        private double? commFixedAmount;
        private double? brokerFeeSplit;
        private string commPaymentPlan;
        private string password;


        public double? CommSplitNew { get { return commSplitNew == null ? 0 : commSplitNew; } set { this.commSplitNew = value; } }
        public double? CommSplitRenew { get { return commSplitRenew == null ? 0 : commSplitRenew; } set { this.commSplitRenew = value; } }
        public double? CommFixedAmount { get { return commFixedAmount == null ? 0 : commFixedAmount; } set { this.commFixedAmount = value; } }
        public double? BrokerFeeSplit { get { return brokerFeeSplit == null ? 0 : brokerFeeSplit; } set { this.brokerFeeSplit = value; } }
        public string CommPaymentPlan { get { return commPaymentPlan == null ? string.Empty : commPaymentPlan; } set { this.commPaymentPlan = value; } }
        public string Notes { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public bool Active { get; set; }
        public string FullName
        {
            get
            {
                return $"{this.FirstName} {this.MiddleName} {this.LastName}";
            }
        }
    }
}
