USE [tpingsoft];
GO

-- 1. MIGRACIÓN DE DATOS DE ROLPERMISO
PRINT 'Realizando backup de datos de RolPermiso...';
IF OBJECT_ID('tempdb..#RolPermisoBackup') IS NOT NULL DROP TABLE #RolPermisoBackup;
CREATE TABLE #RolPermisoBackup (
    id_rol INT,
    id_permiso INT
);

IF OBJECT_ID('[dbo].[RolPermiso]', 'U') IS NOT NULL
BEGIN
    INSERT INTO #RolPermisoBackup SELECT id_rol, id_permiso FROM [dbo].[RolPermiso];
    PRINT 'Backup completo. Eliminando tabla vieja...';
    DROP TABLE [dbo].[RolPermiso];
END
GO

-- 2. RECREACIÓN DE ROLPERMISO CON LA NUEVA ESTRUCTURA COMPOSITE
PRINT 'Creando nueva tabla RolPermiso...';
CREATE TABLE [dbo].[RolPermiso] (
    id_padre   INT         NOT NULL REFERENCES [dbo].[Rol](id),
    id_hijo    INT         NOT NULL,        -- FK a Rol.id o Permiso.id según tipo_hijo
    tipo_hijo  VARCHAR(10) NOT NULL CHECK (tipo_hijo IN ('Rol', 'Permiso')),
    PRIMARY KEY (id_padre, id_hijo, tipo_hijo),
    CHECK (id_padre <> id_hijo OR tipo_hijo = 'Permiso')  -- Evita ciclos directos simples
);
GO

-- 3. RESTAURACIÓN DE DATOS MIGRADOS
PRINT 'Restaurando permisos directos...';
IF OBJECT_ID('tempdb..#RolPermisoBackup') IS NOT NULL
BEGIN
    INSERT INTO [dbo].[RolPermiso] (id_padre, id_hijo, tipo_hijo)
    SELECT id_rol, id_permiso, 'Permiso' FROM #RolPermisoBackup;
    DROP TABLE #RolPermisoBackup;
END
GO

-- 4. ACTUALIZACIÓN / CREACIÓN DE STORED PROCEDURES

-- AsignarPermisoARol
PRINT 'Creando/Modificando Stored Procedures...';
IF OBJECT_ID('[dbo].[AsignarPermisoARol]', 'P') IS NOT NULL DROP PROCEDURE [dbo].[AsignarPermisoARol];
GO
CREATE PROCEDURE [dbo].[AsignarPermisoARol]
    @IdRol INT, @IdPermiso INT
AS BEGIN
    IF NOT EXISTS (SELECT 1 FROM RolPermiso WHERE id_padre=@IdRol AND id_hijo=@IdPermiso AND tipo_hijo='Permiso')
        INSERT INTO RolPermiso (id_padre, id_hijo, tipo_hijo) VALUES (@IdRol, @IdPermiso, 'Permiso');
END
GO

-- RemoverPermisoDeRol
IF OBJECT_ID('[dbo].[RemoverPermisoDeRol]', 'P') IS NOT NULL DROP PROCEDURE [dbo].[RemoverPermisoDeRol];
GO
CREATE PROCEDURE [dbo].[RemoverPermisoDeRol]
    @IdRol INT, @IdPermiso INT
AS BEGIN
    DELETE FROM RolPermiso WHERE id_padre=@IdRol AND id_hijo=@IdPermiso AND tipo_hijo='Permiso';
END
GO

-- AsignarSubRol
IF OBJECT_ID('[dbo].[AsignarSubRol]', 'P') IS NOT NULL DROP PROCEDURE [dbo].[AsignarSubRol];
GO
CREATE PROCEDURE [dbo].[AsignarSubRol]
    @IdPadre INT, @IdHijo INT
AS BEGIN
    IF NOT EXISTS (SELECT 1 FROM RolPermiso WHERE id_padre=@IdPadre AND id_hijo=@IdHijo AND tipo_hijo='Rol')
        INSERT INTO RolPermiso (id_padre, id_hijo, tipo_hijo) VALUES (@IdPadre, @IdHijo, 'Rol');
END
GO

-- RemoverSubRol
IF OBJECT_ID('[dbo].[RemoverSubRol]', 'P') IS NOT NULL DROP PROCEDURE [dbo].[RemoverSubRol];
GO
CREATE PROCEDURE [dbo].[RemoverSubRol]
    @IdPadre INT, @IdHijo INT
AS BEGIN
    DELETE FROM RolPermiso WHERE id_padre=@IdPadre AND id_hijo=@IdHijo AND tipo_hijo='Rol';
END
GO

-- LeerPermisosPorRol (Modificado)
IF OBJECT_ID('[dbo].[LeerPermisosPorRol]', 'P') IS NOT NULL DROP PROCEDURE [dbo].[LeerPermisosPorRol];
GO
CREATE PROCEDURE [dbo].[LeerPermisosPorRol]
    @IdRol INT
AS BEGIN
    SELECT p.id, p.nombre, 'Permiso' AS tipo
    FROM Permiso p
    INNER JOIN RolPermiso rp ON p.id = rp.id_hijo
    WHERE rp.id_padre = @IdRol AND rp.tipo_hijo = 'Permiso';
END
GO

-- LeerSubRolesPorRol
IF OBJECT_ID('[dbo].[LeerSubRolesPorRol]', 'P') IS NOT NULL DROP PROCEDURE [dbo].[LeerSubRolesPorRol];
GO
CREATE PROCEDURE [dbo].[LeerSubRolesPorRol]
    @IdRol INT
AS BEGIN
    SELECT r.id, r.nombre, 'Rol' AS tipo
    FROM Rol r
    INNER JOIN RolPermiso rp ON r.id = rp.id_hijo
    WHERE rp.id_padre = @IdRol AND rp.tipo_hijo = 'Rol';
END
GO

-- CrearRol
IF OBJECT_ID('[dbo].[CrearRol]', 'P') IS NOT NULL DROP PROCEDURE [dbo].[CrearRol];
GO
CREATE PROCEDURE [dbo].[CrearRol]
    @Nombre NVARCHAR(100), @NuevoId INT OUTPUT
AS BEGIN
    INSERT INTO Rol (nombre) VALUES (@Nombre);
    SET @NuevoId = SCOPE_IDENTITY();
END
GO

-- ActualizarRol
IF OBJECT_ID('[dbo].[ActualizarRol]', 'P') IS NOT NULL DROP PROCEDURE [dbo].[ActualizarRol];
GO
CREATE PROCEDURE [dbo].[ActualizarRol]
    @Id INT, @Nombre NVARCHAR(100)
AS BEGIN
    UPDATE Rol SET nombre = @Nombre WHERE id = @Id;
END
GO

-- EliminarRol
IF OBJECT_ID('[dbo].[EliminarRol]', 'P') IS NOT NULL DROP PROCEDURE [dbo].[EliminarRol];
GO
CREATE PROCEDURE [dbo].[EliminarRol]
    @Id INT
AS BEGIN
    -- Eliminar relaciones donde sea padre
    DELETE FROM RolPermiso WHERE id_padre = @Id;
    -- Eliminar relaciones donde sea hijo (subrol)
    DELETE FROM RolPermiso WHERE id_hijo = @Id AND tipo_hijo = 'Rol';
    -- Eliminar del mapeo Usuario-Rol
    DELETE FROM UsuarioRol WHERE id_rol = @Id;
    -- Eliminar el Rol
    DELETE FROM Rol WHERE id = @Id;
END
GO

-- LeerRolPorId
IF OBJECT_ID('[dbo].[LeerRolPorId]', 'P') IS NOT NULL DROP PROCEDURE [dbo].[LeerRolPorId];
GO
CREATE PROCEDURE [dbo].[LeerRolPorId]
    @Id INT
AS BEGIN
    SELECT id, nombre FROM Rol WHERE id = @Id;
END
GO

-- LeerPermisos
IF OBJECT_ID('[dbo].[LeerPermisos]', 'P') IS NOT NULL DROP PROCEDURE [dbo].[LeerPermisos];
GO
CREATE PROCEDURE [dbo].[LeerPermisos]
AS BEGIN
    SELECT id, nombre FROM Permiso;
END
GO

PRINT 'Migración de base de datos finalizada.';
GO
