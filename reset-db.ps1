# Reset database script for TpIngSoft
$Server = "localhost"
$Database = "TpIngSoft"

Write-Host "Iniciando restablecimiento de la base de datos '$Database' en '$Server'..." -ForegroundColor Cyan

# 1. Dropear la base de datos si existe, cerrando conexiones activas
Write-Host "1. Eliminando la base de datos existente..." -ForegroundColor Yellow
$DropQuery = @"
IF EXISTS (SELECT * FROM sys.databases WHERE name = '$Database')
BEGIN
    ALTER DATABASE [$Database] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$Database];
END
"@
sqlcmd -S $Server -E -C -Q $DropQuery

if ($LASTEXITCODE -ne 0) {
    Write-Error "Error al eliminar la base de datos."
    exit $LASTEXITCODE
}

# 2. Ejecutar schema-init.sql
Write-Host "2. Creando base de datos y cargando esquema inicial (schema-init.sql)..." -ForegroundColor Yellow
sqlcmd -S $Server -E -C -f 65001 -i "docs\schema-init.sql"

if ($LASTEXITCODE -ne 0) {
    Write-Error "Error al ejecutar schema-init.sql."
    exit $LASTEXITCODE
}

# 3. Ejecutar stored-procedures.sql
Write-Host "3. Creando procedimientos almacenados (stored-procedures.sql)..." -ForegroundColor Yellow
sqlcmd -S $Server -E -C -f 65001 -i "docs\stored-procedures.sql"

if ($LASTEXITCODE -ne 0) {
    Write-Error "Error al ejecutar stored-procedures.sql."
    exit $LASTEXITCODE
}

Write-Host "¡Base de datos restablecida con éxito!" -ForegroundColor Green
