var DriverEndorsementModel = {
    driverId: 0,
    excluded: false,
    lastName: null,
    middle: null,
    firstName: null,
    dob: null,
    phone: null,
    email: null,
    state: null,
    cdlnumber: null,
    cdlyearLic: null,
    dateHired: null,
    terminated: null,
    ownerOperator: false,
    accountId: null,
    notes: null,
    driverCoverages: [],
    new: function () {
        this.dateHired = null;
        this.driverId = 0;
        this.excluded = false;
        this.lastName = null;
        this.middle = null;
        this.firstName = null;
        this.dob = null;
        this.phone = null;
        this.email = null;
        this.state = null;
        this.cdlnumber = null;
        this.cdlyearLic = null;
        this.terminated = null;
        this.ownerOperator = false;
        this.accountId = null;
        this.notes = null;
        this.driverCoverages = [];
        return this;
    }
};

var DriverCoverageModel =
{
    coverageTypeId: 0,
    premium: 0,
    premiumTax: 0,
    brokerFee: 0,
    totalAmount: 0,
    new: function () {
        this.coverageTypeId = 0;
        this.premium = 0;
        this.premiumTax = 0;
        this.brokerFee = 0;
        this.totalAmount = 0;
        return this;
    }
};