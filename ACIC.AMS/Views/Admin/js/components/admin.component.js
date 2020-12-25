var Admin = {
    user: null,
    agents: [],
    agent: null,
    agentsTable: null,
    usersTable: null,
    deletedUsersTable: null,
    init: function () {
        $("#btnAddUser").click(function () {
            Admin.user = $.extend(true, {}, UserModel.new());
            $("#mdl-user-form-title").html("Add User");
            BindingService.bindModelToForm('frmUser', Admin.user);
            $("#mdlAddUser").modal({
                backdrop: 'static',
            });
        });

        $("#btnAddAgent").click(function () {
            Admin.agent = AgentModel.new();
            BindingService.bindModelToForm('frmAgent', Admin.agent);
            $("#mdl-agent-form-title").html('Add Agent');
            $("#mdlAgent").modal({
                backdrop: 'static',
            });
        });

        $("#frmUser").on("submit", function () {
            Admin.saveUser();
            return false;
        });

        $("#frmAgent").on("submit", function () {
            Admin.saveAgent();
            return false;
        });

        $("html").on("click", ".btn-user-delete", function () { 
            Admin.user = UserModel.new();
            Admin.user.id = $(this).attr('data-id');
            $("#mdlConfirmDelete").modal('show');
        });

        $("html").on("click", ".btn-user-delete-permanent", function () {
            Admin.user = UserModel.new();
            Admin.user.id = $(this).attr('data-id');
            $("#mdlConfirmDeletePermanent").modal('show');
        });

        $("html").on("click", ".btn-agent-view", function () {
            var agentId = $(this).attr('data-id');
            Admin.agent = Admin.agents.find((c) => { return c["id"] === parseInt(agentId) });
            BindingService.bindModelToForm('frmAgent', Admin.agent);

            $("#mdl-agent-form-title").html('Update Agent');
            $("#mdlAgent").modal({
                backdrop: 'static',
            });
        });

        $("#btnConfirmDelete").click(function () {
            $("#mdlConfirmDelete").modal('hide');
            Admin.setStatus(Admin.user.id, false);
        });

        $("#btnConfirmDeletePermanent").click(function () {
            $("#mdlConfirmDeletePermanent").modal('hide');
            Admin.deleteUser(Admin.user.id);
        });

        $("html").on("click", ".btn-user-edit", function () {
            Admin.editUser($(this).attr('data-id'));
        });

        $("html").on("click", ".btn-user-restore", function () {
            Admin.setStatus($(this).attr('data-id'), true);
        });

        Admin.getActiveUsers();
        Admin.getDeletedUsers();
        Admin.getAgents();
    },
    saveUser: function () {
      
        App.blockUI({
            target: "#frmUser",
            blockerOnly: true
        });

        UserService.saveUser(Admin.user,
            function (data) {
                App.unblockUI("#frmUser");
                $("#mdlAddUser").modal('hide');
                Admin.getActiveUsers();
            },
            function (error) {
                alert(error);
                App.unblockUI("#frmUser");
            }
        );
    },
    saveAgent: function () {

        App.blockUI({
            target: "#frmAgent",
            blockerOnly: true
        });

        AgentService.saveAgent(Admin.agent,
            function (data) {
                App.unblockUI("#frmAgent");
                $("#mdlAgent").modal('hide');
                Admin.getAgents();
            },
            function (error) {
                alert(error);
                App.unblockUI("#frmAgent");
            }
        );
    },
    editUser: function (id) {
        UserService.getUserDetails(id, function (data) {
            Admin.user = data;
            $("#mdl-user-form-title").html("Edit User");

            BindingService.bindModelToForm('frmUser', Admin.user);
            $("#mdlAddUser").modal({
                backdrop: 'static'
            });
        });
    },
    getActiveUsers: function () {
        $("#tbUserList").html('');
        $(".users-content").addClass('hide');
        $("#usersPreloader").removeClass('hide');
        UserService.getActiveUsers(function (data) {
            $("#tbUserList").html('');
            if ($.fn.dataTable.isDataTable('#tblUsers')) {
                Admin.usersTable.destroy();
                $('#tbUserList').html('');
            }
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
                                        <button type="button" class="btn btn-success btn-sm btn-user-edit" data-id="`+ data[i].id + `"><i class="fa fa-pencil"></i></button>
                                        <button type="button" class="btn btn-success btn-sm btn-user-delete" data-id="`+ data[i].id + `"><i class="fa fa-trash"></i></button>
                                    </td>
                                </tr>`;

                    $("#tbUserList").append(userRow);
                }
            }
            Admin.usersTable = $("#tblUsers").DataTable({
                "pageLength": 10,
                "searching": false,
                "bLengthChange": false, "bInfo": false,
                "columnDefs": [{
                    "targets": 4,
                    "orderable": false
                }]
            })
            $(".users-content").removeClass('hide');
            $("#usersPreloader").addClass('hide');
        });
    },
    getDeletedUsers: function () {
        $("#deletedUsersPreloader").removeClass('hide');
        $(".deleted-users-content").addClass('hide');

        UserService.getDeletedUsers(function (data) {

            $("#tbUserListDeleted").html('');
            if ($.fn.dataTable.isDataTable('#tblUsersDeleted')) {
                Admin.deletedUsersTable.destroy();
                $('#tbUserListDeleted').html('');
            }

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
                                        <button title="Restore User" type="button" class="btn btn-success btn-sm btn-user-restore" data-id="`+ data[i].id + `"><i class="fa fa-rotate-left"></i></button>
                                        <button title="Delete Permanently" type="button" class="btn btn-success btn-sm btn-user-delete-permanent" data-id="`+ data[i].id + `"><i class="fa fa-trash"></i></button>
                                    </td>
                                </tr>`;

                    $("#tbUserListDeleted").append(userRow);
                }
            }

            Admin.deletedUsersTable = $("#tblUsersDeleted").DataTable({
                "pageLength": 10,
                "searching": false,
                "bLengthChange": false, "bInfo": false,
                "columnDefs": [{
                    "targets": 4,
                    "orderable": false
                }]
            })

            $("#deletedUsersPreloader").addClass('hide');
            $(".deleted-users-content").removeClass('hide');
        });
    },
    getAgents: function () {
        $("#agentsPreloader").removeClass('hide');
        $(".agents-content").addClass('hide');
        AgentService.getAgents(function (data) {
            $('#tbAgentsList').html('');
            Admin.agents = data;
            if ($.fn.dataTable.isDataTable('#tblAgents')) {
                Admin.agentsTable.destroy();
                $('#tbAgentsList').html('');
            }

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
                                    <td> `+ data[i].fullName + ` </td>
                                    <td> `+ data[i].emailAddress + ` </td>
                                    <td class="text-center"> `+ data[i].commSplitNew + `</td>
                                    <td class="text-center"> `+ data[i].commSplitRenew + `</td>
                                    <td class="text-center"> `+ data[i].commFixedAmount +` </td>
                                    <td class="text-center"> `+ data[i].brokerFeeSplit +` </td>
                                    <td class="text-center"> `+ data[i].commPaymentPlan +` </td>
                                    <td class="action">
                                        <button title="View Details" type="button" class="btn btn-success btn-sm btn-agent-view" data-id="`+ data[i].id + `"><i class="fa fa-pencil"></i></button>
                                    </td>
                                </tr>`;

                    $("#tbAgentsList").append(userRow);
                }
            }

            Admin.agentsTable = $("#tblAgents").DataTable({
                "pageLength": 10,
                "searching": false,
                "bLengthChange": false, "bInfo": false,
                "columnDefs": [{
                    "targets": 7,
                    "orderable": false
                }]
            })
            $("#agentsPreloader").addClass('hide');
            $(".agents-content").removeClass('hide');
        });      
    },
    setStatus: function (id, active) {
        UserService.setStatus(id, active, function () {
            Admin.getActiveUsers();
            Admin.getDeletedUsers();
        })
    },
    deleteUser: function (id) {
        UserService.deleteUser(id, function () {
            Admin.getActiveUsers();
            Admin.getDeletedUsers();
        });
    }
}