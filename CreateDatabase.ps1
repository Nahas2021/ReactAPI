# PowerShell script to create the database using sqlcmd
# This script creates the ReactAPIDB database and all required tables based on the models

$sqlFilePath = ".\CreateDatabase.sql"
$connectionString = "Server=.;Integrated Security=True;"

# Check if sqlcmd is available
if (!(Get-Command sqlcmd -ErrorAction SilentlyContinue)) {
	Write-Error "sqlcmd is not available. Please install SQL Server Management Tools."
	exit 1
}

Write-Host "Creating database using SQL script..."

# Execute the SQL script
& sqlcmd -S . -E -i $sqlFilePath -b

if ($LASTEXITCODE -eq 0) {
	Write-Host "Database created successfully!" -ForegroundColor Green
	Write-Host ""
	Write-Host "Summary of created tables:"
	Write-Host "  - Employees"
	Write-Host "  - Registers"
	Write-Host "  - Bookings"
	Write-Host "  - Slots"
	Write-Host ""
	Write-Host "The database ReactAPIDB is ready to use with your ReactAPI application."
} else {
	Write-Error "Failed to create database. Exit code: $LASTEXITCODE"
	exit 1
}
