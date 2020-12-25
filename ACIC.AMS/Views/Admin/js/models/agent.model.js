var AgentModel = {
    active: true,
    brokerFeeSplit: null,
    commFixedAmount: null,
    commPaymentPlan: "",
    commSplitNew: null,
    commSplitRenew: null,
    emailAddress: "",
    password: "",
    firstName: "",
    id: 0,
    lastName: "",
    middleName: null,
    notes: null,
    userId: 0,
    new: function () {
        this.id = 0;
        this.active = true;
        this.brokerFeeSplit = null;
        this.commFixedAmount = null;
        this.commPaymentPlan = null;
        this.commSplitNew = null;
        this.commSplitRenew = null;
        this.emailAddress = "";
        this.password = "";
        this.firstName = "";
        this.lastName = "";
        this.middleName = "";
        this.notes = "";
        this.userId = 0;
        return this;
    }
}