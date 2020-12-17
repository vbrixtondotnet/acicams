var Admin = {
    user: {
        id: 0
    },
    init: function () {
        $("#btnAddUser").click(function () {
            Admin.user.id = 0;
            $("#mdl-user-form-title").html("Add User");
            Admin.resetForm();
            $("#mdlAddUser").modal('show');
        });

        $("#slcRole").change(function () {
            var role = $(this).val();

            $("#agentIdField").addClass('hide');
            $("#txtAgentId").removeAttr("required");
            if (role == 'Agent') {
                $("#agentIdField").removeClass('hide');
                $("#txtAgentId").attr("required", true);
            }

        });

        $("#frmUser").on("submit", function () {
            Admin.saveUser()
            return false;
        });

        $("html").on("click", ".btn-user-delete", function () { 
            Admin.user.id = $(this).attr('data-id');
            $("#mdlConfirmDelete").modal('show');
        });

        $("html").on("click", ".btn-user-delete-permanent", function () {
            Admin.user.id = $(this).attr('data-id');
            $("#mdlConfirmDeletePermanent").modal('show');
        });

        $("#btnConfirmDelete").click(function () {
            $("#mdlConfirmDelete").modal('hide');
            Admin.setStatus(false);
        });

        $("#btnConfirmDeletePermanent").click(function () {
            $("#mdlConfirmDeletePermanent").modal('hide');
            Admin.deleteUser();
        });

        $("html").on("click", ".btn-user-edit", function () {
            Admin.user.id = $(this).attr('data-id');
            $("#mdl-user-form-title").html("Edit User");
            Admin.resetForm();
            $("#mdlAddUser").modal('show');
            Admin.editUser()
        });

        $("html").on("click", ".btn-user-restore", function () {
            var id = $(this).attr('data-id');
            Admin.user.id = id;
            Admin.setStatus(true);
        });

        Admin.getActiveUsers();
        Admin.getDeletedUsers();
    },
    resetForm: function () {
        $("#txtEmailAddress").val('');
        $("#txtFullName").val('');
        $("#txtPassword").val('');
        $("#txtConfirmPassword").val('');
        $("#slcRole").val('Admin');
        $("#txtAgentId").val('');
    },
    saveUser: function () {
        var user = {
            "id": Admin.user.id,
            "emailAddress": $("#txtEmailAddress").val(),
            "fullName": $("#txtFullName").val(),
            "password": $("#txtPassword").val(),
            "role": $("#slcRole").val(),
            "agentId": $("#txtAgentId").val(),
            "active": true
        };

        App.blockUI({
            target: "#frmUser",
            iconOnly: true
        });

        $.ajax({
            type: "POST",
            url: "/api/users",
            data: JSON.stringify(user),
            dataType: "json",
            contentType: "application/json",
            success: function (data) {
                $("#mdlAddUser").modal('hide');
                Admin.getActiveUsers();
            },
            error: function (errMsg) {
                if (callback) callback();
            },
            complete: function () {
                App.unblockUI("#frmUser");
            }
        });
    },
    editUser: function () {
        $.ajax({
            type: "GET",
            url: "/api/users/" + Admin.user.id,
            success: function (data) {
                $("#txtEmailAddress").val(data.emailAddress);
                $("#txtFullName").val(data.fullName);
                $("#txtPassword").val(data.password);
                $("#txtConfirmPassword").val(data.password);
                $("#slcRole").val(data.role);
                $("#txtAgentId").val(data.agentId);
            }
        });
    },
    getActiveUsers: function () {
        $("#tbUserList").html('');
        App.blockUI({
            target: "#tbUserList",
            iconOnly: true
        });
        $.ajax({
            type: "GET",
            url: "/api/users?deleted=false",
            success: function (data) {
                if (data.length > 0) {
                    for (var i = 0; i < data.length; i++) {
                        var role = data[i].role;
                        var roleClass = 'label-success';
                        switch (role) {
                            case 'Manager':
                                roleClass = 'label-warning';
                                break;
                            case 'Agent':
                                roleClass = 'label-info';
                                break;
                            case 'CSR':
                                roleClass = 'label-default';
                                break;
                        }
                        var userRow = `<tr>
                                    <td> `+ data[i].emailAddress + ` </td>
                                    <td> `+ data[i].fullName + ` </td>
                                    <td> `+ (data[i].agentId == null ? '' : data[i].agentId)  +` </td>
                                    <td>
                                        <span class="label label-sm `+ roleClass+`"> `+ role+` </span>
                                    </td>
                                    <td class="action">
                                        <button type="button" class="btn default btn-sm btn-user-edit" data-id="`+ data[i].id+`"><i class="fa fa-pencil"></i></button>
                                        <button type="button" class="btn default btn-sm btn-user-delete" data-id="`+ data[i].id +`"><i class="fa fa-trash"></i></button>
                                    </td>
                                </tr>`;

                        $("#tbUserList").append(userRow);
                    }

                }
            },
            complete: function () {
                App.unblockUI("#tbUserList");
            }
        });
    },
    getDeletedUsers: function () {
        $("#tbUserListDeleted").html('');
        App.blockUI({
            target: "#tbUserListDeleted",
            iconOnly: true
        });
        $.ajax({
            type: "GET",
            url: "/api/users?deleted=true",
            success: function (data) {
                if (data.length > 0) {
                    for (var i = 0; i < data.length; i++) {
                        var role = data[i].role;
                        var roleClass = 'label-success';
                        switch (role) {
                            case 'Manager':
                                roleClass = 'label-warning';
                                break;
                            case 'Agent':
                                roleClass = 'label-info';
                                break;
                            case 'CSR':
                                roleClass = 'label-default';
                                break;
                        }
                        var userRow = `<tr>
                                    <td> `+ data[i].emailAddress + ` </td>
                                    <td> `+ data[i].fullName + ` </td>
                                    <td> `+ (data[i].agentId == null ? '' : data[i].agentId) + ` </td>
                                    <td>
                                        <span class="label label-sm `+ roleClass + `"> ` + role + ` </span>
                                    </td>
                                    <td class="action">
                                        <button title="Restore User" type="button" class="btn default btn-sm btn-user-restore" data-id="`+ data[i].id + `"><i class="fa fa-rotate-left"></i></button>
                                        <button title="Delete Permanently" type="button" class="btn btn-danger btn-sm btn-user-delete-permanent" data-id="`+ data[i].id + `"><i class="fa fa-trash"></i></button>
                                    </td>
                                </tr>`;

                        $("#tbUserListDeleted").append(userRow);
                    }

                }
            },
            complete: function () {
                App.unblockUI("#tbUserListDeleted");
            }
        });
    },
    setStatus: function (active) {
        $.ajax({
            type: "POST",
            url: "/api/users/" + Admin.user.id + "/active/" + active,
            success: function (data) {
                Admin.getActiveUsers();
                Admin.getDeletedUsers();
            }
        });
    },
    deleteUser: function () {
        $.ajax({
            type: "DELETE",
            url: "/api/users/" + Admin.user.id,
            success: function (data) {
                Admin.getActiveUsers();
                Admin.getDeletedUsers();
            }
        });
    }
}