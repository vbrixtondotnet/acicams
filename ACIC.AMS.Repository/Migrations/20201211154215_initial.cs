using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ACIC.AMS.Repository.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    Usdot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatePermit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LegalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dba = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperationType = table.Column<float>(type: "real", nullable: true),
                    OperationRadius = table.Column<float>(type: "real", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GarageAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GarageCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GarageState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GarageZip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<float>(type: "real", nullable: true),
                    YearClient = table.Column<int>(type: "int", nullable: true),
                    AgentId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "Agent",
                columns: table => new
                {
                    AgentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Inactive = table.Column<bool>(type: "bit", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoginId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommSplitNew = table.Column<double>(type: "float", nullable: true),
                    CommSplitRenew = table.Column<double>(type: "float", nullable: true),
                    CommFixedAmount = table.Column<double>(type: "float", nullable: true),
                    BrokerFeeSplit = table.Column<double>(type: "float", nullable: true),
                    CommPaymentPlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agent", x => x.AgentId);
                });

            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    BankId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.BankId);
                });

            migrationBuilder.CreateTable(
                name: "Carrier",
                columns: table => new
                {
                    CarrierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarrierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WritingState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgencyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ambest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarrierPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequirementsLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrier", x => x.CarrierId);
                });

            migrationBuilder.CreateTable(
                name: "Claim",
                columns: table => new
                {
                    ClaimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    LossDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReportedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LossType = table.Column<float>(type: "real", nullable: true),
                    CarrierId = table.Column<int>(type: "int", nullable: true),
                    PolicyId = table.Column<int>(type: "int", nullable: true),
                    DriverId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimNumber = table.Column<int>(type: "int", nullable: true),
                    ClaimStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaidOut = table.Column<double>(type: "float", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claim", x => x.ClaimId);
                });

            migrationBuilder.CreateTable(
                name: "Commission",
                columns: table => new
                {
                    CommId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccoutId = table.Column<int>(type: "int", nullable: true),
                    AgentId = table.Column<int>(type: "int", nullable: true),
                    TrackId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchedIndex = table.Column<short>(type: "smallint", nullable: true),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Commissions = table.Column<double>(type: "float", nullable: true),
                    BrokerFees = table.Column<double>(type: "float", nullable: true),
                    Incentives = table.Column<double>(type: "float", nullable: true),
                    Deductions = table.Column<double>(type: "float", nullable: true),
                    NetAmount = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentNo = table.Column<int>(type: "int", nullable: true),
                    DatePaid = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commission", x => x.CommId);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<float>(type: "real", nullable: true),
                    MblBusiness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MblDirect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MblMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.ContactId);
                });

            migrationBuilder.CreateTable(
                name: "DdAccountsOperationRadius",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RadiusName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DdAccountsOperationRadius", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DdAccountsOperationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DdAccountsOperationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DdAccountSource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DdAccountSource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DdAccountStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DdAccountStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DdAccountType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccTypes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DdAccountType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DdContactsTitle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DdContactsTitle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DdCoverageType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoverageTypes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverageDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DdCoverageType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DdUsstate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityAscii = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountyFips = table.Column<double>(type: "float", nullable: true),
                    CountyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountyFipsAll = table.Column<double>(type: "float", nullable: true),
                    CountyNameAll = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<double>(type: "float", nullable: true),
                    Lng = table.Column<double>(type: "float", nullable: true),
                    Population = table.Column<double>(type: "float", nullable: true),
                    Density = table.Column<double>(type: "float", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Military = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Incorporated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timezone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ranking = table.Column<double>(type: "float", nullable: true),
                    Zips = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DdUsstate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DdVehicleMake",
                columns: table => new
                {
                    Vmid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehMakeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DdVehicleMake", x => x.Vmid);
                });

            migrationBuilder.CreateTable(
                name: "DdVehicleType",
                columns: table => new
                {
                    Vtid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DdVehicleType", x => x.Vtid);
                });

            migrationBuilder.CreateTable(
                name: "Driver",
                columns: table => new
                {
                    DriverId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Excluded = table.Column<bool>(type: "bit", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Middle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cdlnumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CdlyearLic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateHired = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Terminated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OwnerOperator = table.Column<bool>(type: "bit", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Driver", x => x.DriverId);
                });

            migrationBuilder.CreateTable(
                name: "Endorsement",
                columns: table => new
                {
                    EndtId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PolicyId = table.Column<int>(type: "int", nullable: true),
                    Effective = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsuredAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverId = table.Column<int>(type: "int", nullable: true),
                    Vin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProRate = table.Column<double>(type: "float", nullable: true),
                    Pdvalue = table.Column<double>(type: "float", nullable: true),
                    CoverageTypes = table.Column<int>(type: "int", nullable: true),
                    BasePerUnit = table.Column<double>(type: "float", nullable: true),
                    NonTaxedRateUnit = table.Column<double>(type: "float", nullable: true),
                    Pdrate = table.Column<double>(type: "float", nullable: true),
                    TrailerRate = table.Column<double>(type: "float", nullable: true),
                    Bfrate = table.Column<double>(type: "float", nullable: true),
                    Strate = table.Column<double>(type: "float", nullable: true),
                    Premium = table.Column<double>(type: "float", nullable: true),
                    Surcharge = table.Column<double>(type: "float", nullable: true),
                    SurDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PolicyFees = table.Column<double>(type: "float", nullable: true),
                    Mgafees = table.Column<double>(type: "float", nullable: true),
                    SurplusTax = table.Column<double>(type: "float", nullable: true),
                    BrokerFees = table.Column<double>(type: "float", nullable: true),
                    EndtFee = table.Column<double>(type: "float", nullable: true),
                    OtherFees = table.Column<double>(type: "float", nullable: true),
                    TotalPremium = table.Column<double>(type: "float", nullable: true),
                    Commission = table.Column<double>(type: "float", nullable: true),
                    InvoiceRef = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinanceRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dpreceived = table.Column<bool>(type: "bit", nullable: false),
                    PaidTo = table.Column<bool>(type: "bit", nullable: false),
                    Apn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ern = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndtSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndtTrackId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endorsement", x => x.EndtId);
                });

            migrationBuilder.CreateTable(
                name: "EndtEstimate",
                columns: table => new
                {
                    EstEndtId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Effective = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProRate = table.Column<double>(type: "float", nullable: true),
                    Pdvalue = table.Column<double>(type: "float", nullable: true),
                    BasePerUnit = table.Column<double>(type: "float", nullable: true),
                    NonTaxedRateUnit = table.Column<double>(type: "float", nullable: true),
                    Pdrate = table.Column<double>(type: "float", nullable: true),
                    Bfrate = table.Column<double>(type: "float", nullable: true),
                    Strate = table.Column<double>(type: "float", nullable: true),
                    Premium = table.Column<double>(type: "float", nullable: true),
                    Surcharge = table.Column<double>(type: "float", nullable: true),
                    PolicyFees = table.Column<double>(type: "float", nullable: true),
                    Mgafees = table.Column<double>(type: "float", nullable: true),
                    SurplusTax = table.Column<double>(type: "float", nullable: true),
                    BrokerFees = table.Column<double>(type: "float", nullable: true),
                    EndtFee = table.Column<double>(type: "float", nullable: true),
                    OtherFees = table.Column<double>(type: "float", nullable: true),
                    TotalPremium = table.Column<double>(type: "float", nullable: true),
                    EndtTrackId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndtEstimate", x => x.EstEndtId);
                });

            migrationBuilder.CreateTable(
                name: "Mga",
                columns: table => new
                {
                    Mgaid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mganame = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WritingState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarrierId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mgaphone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndtFees = table.Column<double>(type: "float", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mga", x => x.Mgaid);
                });

            migrationBuilder.CreateTable(
                name: "Policy",
                columns: table => new
                {
                    PolicyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    Mgaid = table.Column<int>(type: "int", nullable: true),
                    CarrierId = table.Column<int>(type: "int", nullable: true),
                    CoverageTypes = table.Column<float>(type: "real", nullable: true),
                    BindDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Effective = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DirectBill = table.Column<bool>(type: "bit", nullable: false),
                    TotalFactor = table.Column<double>(type: "float", nullable: true),
                    BasePerUnit = table.Column<double>(type: "float", nullable: true),
                    NonTaxedRateUnit = table.Column<double>(type: "float", nullable: true),
                    Bfrate = table.Column<double>(type: "float", nullable: true),
                    Strate = table.Column<double>(type: "float", nullable: true),
                    Pdrate = table.Column<double>(type: "float", nullable: true),
                    TrailerRate = table.Column<double>(type: "float", nullable: true),
                    Premium = table.Column<double>(type: "float", nullable: true),
                    Surcharge = table.Column<double>(type: "float", nullable: true),
                    PolicyFees = table.Column<double>(type: "float", nullable: true),
                    Mgafees = table.Column<double>(type: "float", nullable: true),
                    SurplusTax = table.Column<double>(type: "float", nullable: true),
                    BrokerFees = table.Column<double>(type: "float", nullable: true),
                    OtherFees = table.Column<double>(type: "float", nullable: true),
                    TotalPremium = table.Column<double>(type: "float", nullable: true),
                    CommRate = table.Column<double>(type: "float", nullable: true),
                    Commission = table.Column<double>(type: "float", nullable: true),
                    AgentSplit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgentComm = table.Column<double>(type: "float", nullable: true),
                    AgentBfshare = table.Column<double>(type: "float", nullable: true),
                    PolicyType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankId = table.Column<int>(type: "int", nullable: true),
                    AccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InceptionStage = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policy", x => x.PolicyId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vinid = table.Column<int>(type: "int", nullable: false),
                    Vin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    VYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Make = table.Column<float>(type: "real", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pdvalue = table.Column<double>(type: "float", nullable: true),
                    BankId = table.Column<int>(type: "int", nullable: true),
                    AccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerOperator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    DriverId = table.Column<int>(type: "int", nullable: true),
                    PolicyInclude = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Agent");

            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropTable(
                name: "Carrier");

            migrationBuilder.DropTable(
                name: "Claim");

            migrationBuilder.DropTable(
                name: "Commission");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "DdAccountsOperationRadius");

            migrationBuilder.DropTable(
                name: "DdAccountsOperationType");

            migrationBuilder.DropTable(
                name: "DdAccountSource");

            migrationBuilder.DropTable(
                name: "DdAccountStatus");

            migrationBuilder.DropTable(
                name: "DdAccountType");

            migrationBuilder.DropTable(
                name: "DdContactsTitle");

            migrationBuilder.DropTable(
                name: "DdCoverageType");

            migrationBuilder.DropTable(
                name: "DdUsstate");

            migrationBuilder.DropTable(
                name: "DdVehicleMake");

            migrationBuilder.DropTable(
                name: "DdVehicleType");

            migrationBuilder.DropTable(
                name: "Driver");

            migrationBuilder.DropTable(
                name: "Endorsement");

            migrationBuilder.DropTable(
                name: "EndtEstimate");

            migrationBuilder.DropTable(
                name: "Mga");

            migrationBuilder.DropTable(
                name: "Policy");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Vehicle");
        }
    }
}
