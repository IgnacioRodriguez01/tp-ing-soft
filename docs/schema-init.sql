
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'TpIngSoft')
BEGIN
    CREATE DATABASE [TpIngSoft];
END
GO

USE [TpIngSoft];
GO

-- 1. Creacion de tablas

IF OBJECT_ID('[dbo].[UsuarioRol]', 'U') IS NOT NULL DROP TABLE [dbo].[UsuarioRol];
IF OBJECT_ID('[dbo].[RolPermiso]', 'U') IS NOT NULL DROP TABLE [dbo].[RolPermiso];
IF OBJECT_ID('[dbo].[Sesion]', 'U') IS NOT NULL DROP TABLE [dbo].[Sesion];
IF OBJECT_ID('[dbo].[Usuario]', 'U') IS NOT NULL DROP TABLE [dbo].[Usuario];
IF OBJECT_ID('[dbo].[Rol]', 'U') IS NOT NULL DROP TABLE [dbo].[Rol];
IF OBJECT_ID('[dbo].[Permiso]', 'U') IS NOT NULL DROP TABLE [dbo].[Permiso];
GO

CREATE TABLE [dbo].[Usuario] (
    [id] INT IDENTITY(1,1) PRIMARY KEY,
    [nombre] VARCHAR(50) NOT NULL UNIQUE,
    [pass] VARCHAR(100) NOT NULL,
    [activo] BIT DEFAULT 1
);

CREATE TABLE [dbo].[Rol] (
    [id] INT IDENTITY(1,1) PRIMARY KEY,
    [nombre] VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE [dbo].[Permiso] (
    [id] INT IDENTITY(1,1) PRIMARY KEY,
    [nombre] VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE [dbo].[UsuarioRol] (
    [id_usuario] INT NOT NULL,
    [id_rol] INT NOT NULL,
    PRIMARY KEY ([id_usuario], [id_rol]),
    FOREIGN KEY ([id_usuario]) REFERENCES [dbo].[Usuario]([id]),
    FOREIGN KEY ([id_rol]) REFERENCES [dbo].[Rol]([id])
);

CREATE TABLE [dbo].[RolPermiso] (
    [id_rol] INT NOT NULL,
    [id_permiso] INT NOT NULL,
    PRIMARY KEY ([id_rol], [id_permiso]),
    FOREIGN KEY ([id_rol]) REFERENCES [dbo].[Rol]([id]),
    FOREIGN KEY ([id_permiso]) REFERENCES [dbo].[Permiso]([id])
);

CREATE TABLE [dbo].[Sesion] (
    [id] INT IDENTITY(1,1) PRIMARY KEY,
    [id_usuario] INT NOT NULL,
    [fecha_login] DATETIME DEFAULT GETDATE(),
    [fecha_logout] DATETIME NULL,
    FOREIGN KEY ([id_usuario]) REFERENCES [dbo].[Usuario]([id])
);
GO

-- 2. Stored Procedures

-- BuscarUsuarioPorNombre
CREATE PROCEDURE [dbo].[BuscarUsuarioPorNombre]
    @Nombre VARCHAR(50)
AS
BEGIN
    SELECT id, nombre, pass, activo 
    FROM Usuario 
    WHERE nombre = @Nombre AND activo = 1;
END
GO

-- CrearUsuario
CREATE PROCEDURE [dbo].[CrearUsuario]
    @Nombre VARCHAR(50),
    @Pass VARCHAR(100),
    @NuevoId INT OUTPUT
AS
BEGIN
    INSERT INTO Usuario (nombre, pass, activo)
    VALUES (@Nombre, @Pass, 1);
    SET @NuevoId = SCOPE_IDENTITY();
END
GO

-- LeerRolesPorUsuario
CREATE PROCEDURE [dbo].[LeerRolesPorUsuario]
    @IdUsuario INT
AS
BEGIN
    SELECT r.id, r.nombre
    FROM Rol r
    INNER JOIN UsuarioRol ur ON r.id = ur.id_rol
    WHERE ur.id_usuario = @IdUsuario;
END
GO

-- LeerPermisosPorRol
CREATE PROCEDURE [dbo].[LeerPermisosPorRol]
    @IdRol INT
AS
BEGIN
    SELECT p.id, p.nombre
    FROM Permiso p
    INNER JOIN RolPermiso rp ON p.id = rp.id_permiso
    WHERE rp.id_rol = @IdRol;
END
GO

-- CrearSesion
CREATE PROCEDURE [dbo].[CrearSesion]
    @IdUsuario INT,
    @NuevaSesionId INT OUTPUT
AS
BEGIN
    INSERT INTO Sesion (id_usuario, fecha_login)
    VALUES (@IdUsuario, GETDATE());
    SET @NuevaSesionId = SCOPE_IDENTITY();
END
GO

-- CerrarSesion
CREATE PROCEDURE [dbo].[CerrarSesion]
    @IdSesion INT
AS
BEGIN
    UPDATE Sesion
    SET fecha_logout = GETDATE()
    WHERE id = @IdSesion;
END
GO

-- 3. Seeds

-- Roles
INSERT INTO [dbo].[Rol] ([nombre]) VALUES ('Admin');
INSERT INTO [dbo].[Rol] ([nombre]) VALUES ('User');

-- Permisos
INSERT INTO [dbo].[Permiso] ([nombre]) VALUES ('AccesoAdmin');
INSERT INTO [dbo].[Permiso] ([nombre]) VALUES ('GestionUsuarios');

-- Mapeo Rol-Permiso
INSERT INTO [dbo].[RolPermiso] ([id_rol], [id_permiso]) VALUES (1, 1); -- Admin - AccesoAdmin
INSERT INTO [dbo].[RolPermiso] ([id_rol], [id_permiso]) VALUES (1, 2); -- Admin - GestionUsuarios

-- Usuario Admin por defecto
DECLARE @AdminId INT;
INSERT INTO [dbo].[Usuario] ([nombre], [pass], [activo]) VALUES ('admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 1);
SET @AdminId = SCOPE_IDENTITY();
INSERT INTO [dbo].[UsuarioRol] ([id_usuario], [id_rol]) VALUES (@AdminId, 1);
GO

-- Bitacora Table
IF OBJECT_ID('[dbo].[Bitacora]', 'U') IS NOT NULL DROP TABLE [dbo].[Bitacora];
GO

CREATE TABLE [dbo].[Bitacora] (
    [id] INT IDENTITY(1,1) PRIMARY KEY,
    [fecha_hora] DATETIME DEFAULT GETDATE(),
    [id_usuario] INT NULL,
    [actividad] VARCHAR(100) NOT NULL,
    [info_asociada] VARCHAR(500) NULL,
    FOREIGN KEY ([id_usuario]) REFERENCES [dbo].[Usuario]([id])
);
GO

-- InsertarBitacora
CREATE PROCEDURE [dbo].[InsertarBitacora]
    @IdUsuario INT = NULL,
    @Actividad VARCHAR(100),
    @InfoAsociada VARCHAR(500) = NULL
AS
BEGIN
    INSERT INTO Bitacora (id_usuario, actividad, info_asociada)
    VALUES (@IdUsuario, @Actividad, @InfoAsociada);
END
GO

-- BuscarBitacora
CREATE PROCEDURE [dbo].[BuscarBitacora]
    @IdUsuario INT = NULL,
    @Actividad VARCHAR(100) = NULL,
    @FechaDesde DATETIME = NULL,
    @FechaHasta DATETIME = NULL
AS
BEGIN
    SELECT b.id, b.fecha_hora, b.id_usuario, u.nombre as nombre_usuario, b.actividad, b.info_asociada
    FROM Bitacora b
    LEFT JOIN Usuario u ON b.id_usuario = u.id
    WHERE (@IdUsuario IS NULL OR b.id_usuario = @IdUsuario)
      AND (@Actividad IS NULL OR b.actividad LIKE '%' + @Actividad + '%')
      AND (@FechaDesde IS NULL OR b.fecha_hora >= @FechaDesde)
      AND (@FechaHasta IS NULL OR b.fecha_hora <= @FechaHasta)
    ORDER BY b.fecha_hora DESC;
END
GO
