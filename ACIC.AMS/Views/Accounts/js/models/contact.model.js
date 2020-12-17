ContactModel = {
    contactId: 0,
    email1: "",
    email2: "",
    firstName: "",
    fullName: "",
    lastName: "",
    mblBusiness: "",
    mblDirect: "",
    mblMobile: "",
    middleName: "",
    notes: "",
    refId: "",
    title: "",
    titleId: 1,
    type: "Account",
    new: function () {
        this.contactId = 0;
        this.email1 = "";
        this.email2 = "";
        this.firstName = "";
        this.fullName = "";
        this.lastName = "";
        this.mblBusiness = "";
        this.mblDirect = "";
        this.mblMobile = "";
        this.middleName = "";
        this.refId = "";
        this.title = 0;
        this.titleId = "";
        this.type = "Account";
        this.notes = "";
        return this;
    }
}