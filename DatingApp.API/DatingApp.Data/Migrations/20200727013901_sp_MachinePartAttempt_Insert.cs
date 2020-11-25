using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.Data.Migrations
{
    public partial class sp_MachinePartAttempt_Insert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                var sp = @"ALTER PROCEDURE [dbo].[sp_MachinePartAttempt_Insert]
                                @MachineModel [nvarchar](100)
                                ,@PartModel [nvarchar](100)
                                ,@InternalSequence [nvarchar](100)
                                ,@Successful BIT OUTPUT
                                ,@Id INT OUTPUT
                                ,@Message VARCHAR(250) OUTPUT
                                
                            AS
                            BEGIN
                                
                                DECLARE @PartId INT	= 0
                                DECLARE @ModelId INT = 0	
                                DECLARE @Attempts INT = 0
                                SET @Successful = 1
                                SET @Id = 0		

                                BEGIN TRY
                                    
                                    Select @PartId = Id, @Attempts = Attempts From PartModel where Name = @PartModel
                                    Select @ModelId = id From MachineModel where Name = @MachineModel      
                                    
                                    INSERT INTO MachinePartAttempt
                                        (MachineModelId,PartModelId,InternalSequence,DefaultAttempts,AvailableAttempts)
                                        VALUES
                                        (@ModelId,@PartId,@InternalSequence,@Attempts,@Attempts)   

                                        SET @Id = SCOPE_IDENTITY() 

                                        SET @Message = 'Registro Exitoso'                     

                                END TRY
                                BEGIN CATCH
                                        SET @Id = ERROR_NUMBER()
                                        SET @Message = ERROR_MESSAGE()
                                        SET @Successful = 0            
                                END CATCH	
                            END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"IF OBJECT_ID('sp_MachinePartAttempt_Insert') IS NOT NULL DROP PROCEDURE sp_MachinePartAttempt_Insert
                        GO";
            migrationBuilder.Sql(sp);

        }
    }
}
