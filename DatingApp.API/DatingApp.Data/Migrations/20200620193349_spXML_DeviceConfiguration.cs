using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.Data.Migrations
{
    public partial class spXML_DeviceConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"ALTER PROCEDURE [dbo].[spXML_DeviceConfiguration]
                    @ServerName [nvarchar](100),
                    @Chanel [nvarchar](100) ,
                    @Device [nvarchar](100) ,
                    @Tags nvarchar(max)    
                AS
                BEGIN
                    DECLARE @cmd VARCHAR(MAX)
                    DECLARE @Satisfactorio BIT
                    DECLARE @Id INT
                    DECLARE @ErrorId INT	
                    SET @Satisfactorio = 1
                    SET @Id = 0
                    SET @ErrorId = 0	

                    BEGIN TRY

                        --DECLARE @adhoc INT
                        DECLARE @count INT		
                        DECLARE @idTag INT		

                        CREATE TABLE #TempDeviceConfiguration(Indice INT IDENTITY (1,1)
                        --,ServerName [nvarchar](100) NOT NULL,
                        --Chanel [nvarchar](100) NOT NULL,
                        --Device [nvarchar](100) NOT NULL,
                        ,TagName [nvarchar](100) NOT NULL
                        ,IsStart [bit] NOT NULL
                        ,IsActive [bit] NOT NULL
                        ,ColumnDataType [nvarchar](100) NOT NULL
                        ,ColumnNullable [nvarchar](100) NOT NULL)

                        -- abrir el archivo xml de Campos
                        --EXEC sp_xml_preparedocument @adhoc OUTPUT, @XMLCampos

                        --obtener la informacion del xml
                        INSERT INTO #TempDeviceConfiguration (
                                    --ServerName	
                                    --,Chanel	
                                    --,Device	
                                    TagName	
                                    ,IsStart	
                                    ,IsActive
                                    ,ColumnDataType 
                                    ,ColumnNullable)

                        SELECT  TagName	
                                ,IsStart	
                                ,IsActive
                                ,ColumnDataType 
                                ,ColumnNullable
                        FROM OPENJSON(@Tags)
                        WITH(			
                            TagName [nvarchar](100) ,
                            IsStart [bit] ,
                            IsActive [bit] 
                            ,ColumnDataType [nvarchar](100)
                            ,ColumnNullable [nvarchar](100)
                        )

                        -- Eliminar el documento xml
                        --EXEC sp_xml_removedocument @adhoc		
                        
                        DECLARE @Contador INT = 1
                        DECLARE @Renglones INT

                        DECLARE @TagName [nvarchar](100) ,
                                @IsStart [bit] ,
                                @IsActive [bit] ,
                                @ColumnDataType [nvarchar](100),
                                @ColumnNullable [nvarchar](100)

                        -- 1.- Obtenemos la cantidad de Campos de Accesos
                    
                        SELECT @Renglones = COUNT(*) FROM #TempDeviceConfiguration	
                        SET @cmd = 'CREATE TABLE dbo.' + quotename(@ServerName+'_'+@Chanel+'_'+@Device, '[') + '( '
                        
                            CICLO:	
                                    SELECT 	@TagName	= TagName	
                                            ,@IsStart	= IsStart	
                                            ,@IsActive	= IsActive		
                                            ,@ColumnDataType = ColumnDataType
                                            ,@ColumnNullable = ColumnNullable	
                                        from #TempDeviceConfiguration	where Indice = @Contador

                                        INSERT INTO DeviceConfiguration (
                                                    ServerName	
                                                    ,Chanel	
                                                    ,Device	
                                                    ,TagName	
                                                    ,IsStart	
                                                    ,IsActive
                                                    ,ColumnDataType
                                                    ,ColumnNullable)
                                    
                                        SELECT @ServerName 
                                                ,@Chanel 
                                                ,@Device 
                                                ,@TagName 
                                                ,@IsStart 
                                                ,@IsActive 	
                                                ,@ColumnDataType
                                                ,@ColumnNullable

                                    SET @idTag = SCOPE_IDENTITY()

                                    SET @cmd = @cmd + @TagName+' '+@ColumnDataType +' '+@ColumnNullable                    	
                            
                                SET @Contador = @Contador + 1
                            IF @Renglones >= @Contador 
                            BEGIN 
                                SET @cmd = @cmd +',' 
                                GOTO CICLO
                            END
                            
                            SET @cmd = @cmd +')'
                            
                            Select @cmd
                            exec (@cmd)

                            SET @Id = @Contador			
                        --Select * from #TempDeviceConfiguration
                        DROP TABLE #TempDeviceConfiguration

                    END TRY
                    BEGIN CATCH
                            SET @ErrorId = ERROR_NUMBER()
                            SET @Satisfactorio = 0			
                    END CATCH
                    --SELECT @Id 'Id', @ErrorId 'ErrorId', @Satisfactorio 'Satisfactorio', @Mensaje 'Mensaje'
                END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"IF OBJECT_ID('spXML_DeviceConfiguration') IS NOT NULL DROP PROCEDURE spXML_DeviceConfiguration
                        GO";
            migrationBuilder.Sql(sp);
        }
    }
}
