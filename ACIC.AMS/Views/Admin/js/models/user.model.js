var UserModel = {
    id: 0,
    firstName: "",
    lastName: "",
    middleName: "",
    emailAddress: "",
    password: "",
    role: "",
    active: true,
    dateCreated: null,
    new: function () {
        this.id = 0;
        this.firstName = "";
        this.lastName = "";
        this.middleName = "";
        this.emailAddress = "";
        this.password = "";
        this.role = "";
        this.active = true;
        this.dateCreated = null;
        return this;
    }
}