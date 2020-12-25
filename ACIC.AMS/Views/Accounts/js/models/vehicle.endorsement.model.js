var VehicleEndorsementModel = {
    accountId: null,
    accountNo: null,
    bankId: null,
    driver: null,
    driverId: null,
    id: null,
    make: 4,
    model: null,
    notes: null,
    ownerOperator: null,
    pdvalue: null,
    policyInclude: true,
    type: null,
    vUnit: null,
    vYear: null,
    vehMakeName: "",
    vehTypeName: "",
    vin: null,
    vehicleCoverages: [],
    driverOwner:false,
    notOnLien: false,
    new: function () {
        this.id = 0;
        this.accountId = 0;
        this.accountNo = null;
        this.bankId = 0;
        this.driver = null;
        this.driverId = 0;
        this.make = null;
        this.model = null;
        this.notes = null;
        this.ownerOperator = null;
        this.pdvalue = null;
        this.policyInclude = true;
        this.type = 0;
        this.vUnit = null;
        this.vehMakeName = "";
        this.vehTypeName = "";
        this.vin = null;
        this.vehicleCoverages = [];
        this.driverOwner = false;
        this.notOnLien = false;
        return this;
    }
};

var VehicleCoverageModel =
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