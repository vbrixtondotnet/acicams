using Microsoft.EntityFrameworkCore.Migrations;

namespace ACIC.AMS.Repository.Migrations
{
    public partial class sp_GetPolicyEndorsementUnitStats_add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"CREATE OR ALTER PROC GetPolicyEndorsementUnitStats
								(
									@AccountId int,
									@PolicyId int
								)
								AS
								BEGIN
								DECLARE 
									@PowerUnitsInitial decimal(10,2),
									@PremiumInitial decimal(10,2),
									@PolicyFeesInitial decimal(10,2),
									@SurchargeInitial decimal(10,2),
									@MGAFeesInitial decimal(10,2),
									@SurplusTaxInitial decimal(10,2),
									@BrokerFeesInitial decimal(10,2),
									@OtherFeesInitial decimal(10,2),
									@EndtFeesInitial decimal(10,2),
									@PowerUnitsEndorsement decimal(10,2),
									@PremiumEndorsement decimal(10,2),
									@PolicyFeesEndorsement decimal(10,2),
									@SurchargeEndorsement decimal(10,2),
									@MGAFeesEndorsement decimal(10,2),
									@SurplusTaxEndorsement decimal(10,2),
									@BrokerFeesEndorsement decimal(10,2),
									@OtherFeesEndorsement decimal(10,2),
									@EndtFeesEndorsement decimal(10,2),
									@PowerUnitsCurrent decimal(10,2),
									@PremiumCurrent decimal(10,2),
									@PolicyFeesCurrent decimal(10,2),
									@MGAFeesCurrent decimal(10,2),
									@SurplusTaxCurrent decimal(10,2),
									@BrokerFeesCurrent decimal(10,2),
									@OtherFeesCurrent decimal(10,2),
									@EndtFeesCurrent decimal(10,2),
									@SurchargeCurrent decimal(10,2)


								DECLARE @PolicyEndorsementStats Table (
									Unit nvarchar(100),
									Initial decimal(10,2),
									Endorsements decimal(10,2),
									[Current] decimal(10,2)
								)

								/*##### POWER UNITS ###### */

									-- INITIAL
									SELECT @PowerUnitsInitial = COUNT(*) from Endorsement where AccountId = @AccountId and PolicyId = @PolicyId and Type in ('Tractor','Truck') and Description = 'Initial Binding' 

									-- ENDORSEMENTS
									;with ctePowerDetailsEndorsement as
									(
									SELECT
										(select count(*) from Endorsement where AccountId = @AccountId and PolicyId = @PolicyId and Type in ('Tractor','Truck') and Description <> 'Initial Binding' and Action= 'ADD') as [Add],
										(select count(*) from Endorsement  where AccountId = @AccountId and PolicyId = @PolicyId and Type in ('Tractor','Truck') and Description <> 'Initial Binding' and Action= 'DELETE') as [Delete]
									)
									select @PowerUnitsEndorsement = ISNULL([Add],0) - ISNULL([Delete],0) from ctePowerDetailsEndorsement

									INSERT INTO @PolicyEndorsementStats
									SELECT 'Power Units', @PowerUnitsInitial, @PowerUnitsEndorsement, (@PowerUnitsInitial + @PowerUnitsEndorsement) as [Current]

								/*##### END POWER UNITS ###### */

								/*##### PREMIUM ###### */

									SET @PremiumInitial =
									ISNULL((select sum(Premium) from Endorsement where AccountId = @AccountId and PolicyId = @PolicyId and Description = 'Initial Binding'),0) + 
									ISNULL((select sum(Premium) from [Policy] where AccountId = @AccountId and PolicyId = @PolicyId),0)
	
									SELECT @PremiumEndorsement = 	
									Sum(Premium) from Endorsement where AccountId = @AccountId and PolicyId = @PolicyId and Description != 'Initial Binding' 

									INSERT INTO @PolicyEndorsementStats
									SELECT 'Premium', @PremiumInitial, ISNULL(@PremiumEndorsement,0),  (@PremiumInitial + ISNULL(@PremiumEndorsement,0)) as [Current]
								/*##### END PREMIUM ###### */

								/*##### Surcharge ###### */

									SET @SurchargeInitial =
									ISNULL((select sum(Surcharge) from Endorsement where AccountId = @AccountId and PolicyId = @PolicyId and Description = 'Initial Binding'),0) + 
									ISNULL((select sum(Surcharge) from [Policy] where AccountId = @AccountId and PolicyId = @PolicyId),0)
	
									SELECT @SurchargeEndorsement = 	
									Sum(Surcharge) from Endorsement where AccountId = @AccountId and PolicyId = @PolicyId and Description != 'Initial Binding' 

									INSERT INTO @PolicyEndorsementStats
									SELECT 'Surcharge', @SurchargeInitial, ISNULL(@SurchargeEndorsement,0),  (@SurchargeInitial + ISNULL(@SurchargeEndorsement,0)) as [Current]

								/*##### END Surcharge ###### */

								/*##### Policy Fees ###### */

									SET @PolicyFeesInitial =
									ISNULL((select sum(PolicyFees) from Endorsement where AccountId = @AccountId and PolicyId = @PolicyId and Description = 'Initial Binding'),0) + 
									ISNULL((select sum(PolicyFees) from [Policy] where AccountId = @AccountId and PolicyId = @PolicyId),0)
	
									SELECT @PolicyFeesEndorsement = 	
									Sum(PolicyFees) from Endorsement where AccountId = @AccountId and PolicyId = @PolicyId and Description != 'Initial Binding' 

									INSERT INTO @PolicyEndorsementStats
									SELECT 'Policy Fees', @PolicyFeesInitial, ISNULL(@PolicyFeesEndorsement,0),  (@PolicyFeesInitial + ISNULL(@PolicyFeesEndorsement,0)) as [Current]

								/*##### END Policy Fees ###### */


								/*##### MGA Fees ###### */

									SET @MGAFeesInitial =
									ISNULL((select sum(Mgafees) from Endorsement where AccountId = @AccountId and PolicyId = @PolicyId and Description = 'Initial Binding'),0) + 
									ISNULL((select sum(Mgafees) from [Policy] where AccountId = @AccountId and PolicyId = @PolicyId),0)
	
									SELECT @MGAFeesEndorsement = 	
									Sum(Mgafees) from Endorsement where AccountId = @AccountId and PolicyId = @PolicyId and Description != 'Initial Binding' 

									INSERT INTO @PolicyEndorsementStats
									SELECT 'MGA Fees', @MGAFeesInitial, ISNULL(@MGAFeesEndorsement,0),  (@MGAFeesInitial + ISNULL(@MGAFeesEndorsement,0)) as [Current]

								/*##### END MGA Fees ###### */

								/*##### SurplusTax ###### */

									SET @SurplusTaxInitial =
									ISNULL((select sum(SurplusTax) from Endorsement where AccountId = @AccountId and PolicyId = @PolicyId and Description = 'Initial Binding'),0) + 
									ISNULL((select sum(SurplusTax) from [Policy] where AccountId = @AccountId and PolicyId = @PolicyId),0)
	
									SELECT @SurplusTaxEndorsement = 	
									Sum(SurplusTax) from Endorsement where AccountId = @AccountId and PolicyId = @PolicyId and Description != 'Initial Binding' 

									INSERT INTO @PolicyEndorsementStats
									SELECT 'Surplus Tax', @SurplusTaxInitial, ISNULL(@SurplusTaxEndorsement,0),  (@SurplusTaxInitial + ISNULL(@SurplusTaxEndorsement,0)) as [Current]

								/*##### END SurplusTax ###### */

								/*##### Broker Fees ###### */

									SET @BrokerFeesInitial =
									ISNULL((select sum(BrokerFees) from Endorsement where AccountId = @AccountId and PolicyId = @PolicyId and Description = 'Initial Binding'),0) + 
									ISNULL((select sum(BrokerFees) from [Policy] where AccountId = @AccountId and PolicyId = @PolicyId),0)
	
									SELECT @BrokerFeesEndorsement = 	
									Sum(BrokerFees) from Endorsement where AccountId = @AccountId and PolicyId = @PolicyId and Description != 'Initial Binding' 

									INSERT INTO @PolicyEndorsementStats
									SELECT 'Broker Fees', @BrokerFeesInitial, ISNULL(@BrokerFeesEndorsement,0),  (@BrokerFeesInitial + ISNULL(@BrokerFeesEndorsement,0)) as [Current]

								/*##### END Broker Fees ###### */

								/*##### Other Fees ###### */

									SET @OtherFeesInitial =
									ISNULL((select sum(OtherFees) + sum(NonTaxedRateUnit) from Endorsement where AccountId = @AccountId and PolicyId = @PolicyId and Description = 'Initial Binding'),0) + 
									ISNULL((select sum(OtherFees) + sum(NonTaxedRateUnit) from [Policy] where AccountId = @AccountId and PolicyId = @PolicyId),0)
	
									SELECT @OtherFeesEndorsement = 	
									ISNULL(Sum(OtherFees),0) + ISNULL(Sum(NonTaxedRateUnit),0) from Endorsement where AccountId = @AccountId and PolicyId = @PolicyId and Description != 'Initial Binding' 

									INSERT INTO @PolicyEndorsementStats
									SELECT 'Other Fees', @OtherFeesInitial, ISNULL(@OtherFeesEndorsement,0),  (@OtherFeesInitial + ISNULL(@OtherFeesEndorsement,0)) as [Current]

								/*##### END Other Fees ###### */

								/*##### Endt Fees ###### */

									SET @EndtFeesInitial =
									ISNULL((select sum(EndtFee) from Endorsement where AccountId = @AccountId and PolicyId = @PolicyId and Description = 'Initial Binding'),0) 
	
									SELECT @EndtFeesEndorsement = 	
									Sum(EndtFee) from Endorsement where AccountId = @AccountId and PolicyId = @PolicyId and Description != 'Initial Binding' 

									INSERT INTO @PolicyEndorsementStats
									SELECT 'Endt Fees', @EndtFeesInitial, ISNULL(@EndtFeesEndorsement,0),  (@EndtFeesInitial + ISNULL(@EndtFeesEndorsement,0)) as [Current]

									INSERT INTO @PolicyEndorsementStats
									SELECT 'Total', SUM(Initial), SUM(Endorsements),  SUM([Current]) from @PolicyEndorsementStats

								/*##### END Endt Fees ###### */

								SELECT * FROM @PolicyEndorsementStats
								END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
