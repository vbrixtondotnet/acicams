var Contacts = {
    contacts: [],
    contact: null,
    dTable: null,
    init: function () {
        this.initEventHandlers();
    },
    getContacts: function (id) {
        $(".contacts-content").addClass('hide');
        $("#contactsPreloader").removeClass('hide');
        $("#tblContacts").html('');
        ContactService.getContacts(id, function (data) {
            $(".contacts-content").removeClass('hide');
            $("#contactsPreloader").addClass('hide');
            Contacts.renderContactsTable(data);
        });
    },
    populateContactForm: function (data) {
        this.contact = data;
        $("#slcTitle").html('<option value=""></option>');
        ContactService.getTitles(function (data) {

            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    var title = data[i];
                    $("#slcTitle").append('<option value="'+title.id+'">'+title.titleName+'</option>');
                }
            }
            BindingService.bindModelToForm("frmContact", Contacts.contact);
            $("#slcCompany").html("<option>" + CurrentAccount.legalName + "</option>");
            $("#slcTitle").val(Contacts.contact.titleId).trigger('change');
        });

    },
    saveContact: function () {
        App.blockUI({
            target: "#frmContact",
            blockerOnly: true
        });

        ContactService.saveContact(Contacts.contact, function (data) {
            App.unblockUI("#frmContact");
            $("#mdlAddContact").modal('hide');
            Contacts.getContacts(CurrentAccount.accountId);
        });
    },
    renderContactsTable: function (contactsList) {
        Contacts.contacts = contactsList;
        if ($.fn.dataTable.isDataTable('#dTableContacts')) {
            Contacts.dTable.destroy();
            $("#tblContacts").html('');
        }
        if (Contacts.contacts.length > 0) {
            for (var i = 0; i < Contacts.contacts.length; i++) {
                var contact = Contacts.contacts[i];
                var contactRow = ` <tr data-id=` + contact.contactId + `>
                                <td> `+ contact.fullName + ` </td>
                                <td> `+ contact.title + ` </td>
                                <td> `+ contact.mblBusiness + `  </td>
                                <td> `+ contact.email1 + ` </td>
                                <td class="action"> <button class="btn btn-success btn-sm btn-contact-details" data-id="`+ contact.contactId + `" title="View Details"><i class="fa fa-search"></i></button> </td>
                            </tr>`
                $("#tblContacts").append(contactRow);
            }
        }

        Contacts.dTable = $("#dTableContacts").DataTable({
            "pageLength": 2,
            "searching": false,
            "bLengthChange": false,
            "columnDefs": [{
                "targets": 4,
                "orderable": false
            }]
        })
    },
    initEventHandlers: function () {
        $("html").on("click", "#tblContacts tr", function () {
            var contactId = parseInt($(this).attr("data-id"));
            $("#tblContacts tr").removeClass('active');
            $(this).addClass('active');

            var contact = Contacts.contacts.find((c) => { return c["contactId"] === parseInt(contactId) });
            $("#txtContactNotes").val(contact.notes);
        });

        $("html").on("click", ".btn-contact-details", function () {
            var contactId = parseInt($(this).attr("data-id"));
            var contact = Contacts.contacts.find((c) => { return c["contactId"] === parseInt(contactId) });
            contact.company = CurrentAccount.legalName;
            Contacts.contact = contact;
            $("#mdlContactDetails").modal('show');
            BindingService.bindModelToLabels("contactDetailsContent", contact);
        });

        $("#btnEditContact").click(function () {
            $("#mdlContactDetails").modal('hide');
            $("#mdlAddContact").modal('show');
            Contacts.populateContactForm(Contacts.contact);
        });

        $("#btnAddContact").click(function () {
            $("#mdlAddContact").modal('show');
            var contact = ContactModel.new();
            contact.refId = CurrentAccount.accountId;
            Contacts.populateContactForm(contact);
        });

        $("#frmContact").on('submit', function () {
            Contacts.saveContact();
            return false;
        });
    }
}