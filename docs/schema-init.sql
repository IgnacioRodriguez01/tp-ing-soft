IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'TpIngSoft')
BEGIN
    CREATE DATABASE [TpIngSoft];
END
GO

USE [TpIngSoft];
GO

-- ====================================================
-- 1. Creación de tablas
-- ====================================================

IF OBJECT_ID('[dbo].[UsuarioRol]', 'U') IS NOT NULL DROP TABLE [dbo].[UsuarioRol];
IF OBJECT_ID('[dbo].[RolPermiso]', 'U') IS NOT NULL DROP TABLE [dbo].[RolPermiso];
IF OBJECT_ID('[dbo].[Sesion]', 'U') IS NOT NULL DROP TABLE [dbo].[Sesion];
IF OBJECT_ID('[dbo].[Bitacora]', 'U') IS NOT NULL DROP TABLE [dbo].[Bitacora];
IF OBJECT_ID('[dbo].[Usuario_Historial]', 'U') IS NOT NULL DROP TABLE [dbo].[Usuario_Historial];
IF OBJECT_ID('[dbo].[DigitoVerificadorVertical]', 'U') IS NOT NULL DROP TABLE [dbo].[DigitoVerificadorVertical];
IF OBJECT_ID('[dbo].[Usuario]', 'U') IS NOT NULL DROP TABLE [dbo].[Usuario];
IF OBJECT_ID('[dbo].[Rol]', 'U') IS NOT NULL DROP TABLE [dbo].[Rol];
IF OBJECT_ID('[dbo].[Permiso]', 'U') IS NOT NULL DROP TABLE [dbo].[Permiso];
GO

CREATE TABLE [dbo].[Usuario] (
    [id] INT IDENTITY(1,1) PRIMARY KEY,
    [nombre] VARCHAR(50) NOT NULL UNIQUE,
    [pass] VARCHAR(100) NOT NULL,
    [activo] BIT DEFAULT 1,
    [intentos_fallidos] INT DEFAULT 0,
    [bloqueado_hasta] DATETIME NULL,
    [dvh] BIGINT DEFAULT 0
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

CREATE TABLE [dbo].[Bitacora] (
    [id] INT IDENTITY(1,1) PRIMARY KEY,
    [fecha_hora] DATETIME DEFAULT GETDATE(),
    [id_usuario] INT NULL,
    [actividad] VARCHAR(100) NOT NULL,
    [info_asociada] VARCHAR(500) NULL,
    FOREIGN KEY ([id_usuario]) REFERENCES [dbo].[Usuario]([id])
);

CREATE TABLE [dbo].[Usuario_Historial] (
    [id_historial] INT IDENTITY(1,1) PRIMARY KEY,
    [id_usuario] INT NOT NULL,
    [nombre] VARCHAR(50) NOT NULL,
    [pass] VARCHAR(100) NOT NULL,
    [activo] BIT NOT NULL,
    [dvh] BIGINT NULL,
    [fecha_cambio] DATETIME DEFAULT GETDATE(),
    [id_usuario_autor] INT NOT NULL,
    [tipo_operacion] VARCHAR(20) NOT NULL
);

CREATE TABLE [dbo].[DigitoVerificadorVertical] (
    [tabla] VARCHAR(50) PRIMARY KEY,
    [dvv] BIGINT NOT NULL
);
GO

-- ====================================================
-- 2. Seeds (Datos Iniciales)
-- ====================================================

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

-- Digito Verificador Vertical Inicial
INSERT INTO [dbo].[DigitoVerificadorVertical] ([tabla], [dvv]) VALUES ('Usuario', 0);
GO
