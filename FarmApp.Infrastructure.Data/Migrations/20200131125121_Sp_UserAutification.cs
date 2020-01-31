using Microsoft.EntityFrameworkCore.Migrations;

namespace FarmApp.Infrastructure.Data.Migrations
{
    public partial class Sp_UserAutification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
CREATE PROCEDURE api.UserAutification
(
	@param NVARCHAR(MAX),
	@result BIT = 0 OUTPUT,
	@ErrorMsg NVARCHAR(255) = '' OUTPUT
) AS

SET FMTONLY OFF
SET NOCOUNT ON
SET ANSI_WARNINGS OFF
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

BEGIN
	BEGIN TRY
		DECLARE @isDisabled BIT, @iSDeleted BIT, @userName NVARCHAR(255), @password NVARCHAR(255)
		IF OBJECT_ID('tempdb..#tAutrification') IS NOT NULL DROP TABLE #tAutrification

		IF ISNULL(ISJSON(@param), 0) = 0
			RAISERROR('Некорректный запрос!', 16, 1);

			SET @userName = JSON_VALUE(@param, '$.User.login')
		SET @password = JSON_VALUE(@param, '$.User.password')

		SELECT TOP 1
			@isDisabled = IsDisabled,
			@isDisabled = IsDeleted
		FROM dist.Users
			WHERE Login = @userName AND Password = @password


		IF @isDisabled IS NULL OR @isDisabled IS NULL
			RAISERROR('Неверный логин или пароль!', 16, 1);
			IF @isDisabled = 1
			RAISERROR('Пользователь заблокирован!', 16, 1);
			IF @isDisabled = 1
			RAISERROR('Пользователь удален!', 16, 1);

			SET @result = 1

	END TRY
	BEGIN CATCH
		SET @ErrorMsg = ERROR_MESSAGE()
		SET @result = 0
	END CATCH;


END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			var sp = @"
IF OBJECT_ID('api.UserAutification', 'P') IS NOT NULL
	DROP PROCEDURE api.UserAutification;
GO
";
			migrationBuilder.Sql(sp);

		}
    }
}
