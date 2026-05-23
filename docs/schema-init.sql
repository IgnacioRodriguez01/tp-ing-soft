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

IF OBJECT_ID('[dbo].[TRADUCCIONES]', 'U') IS NOT NULL DROP TABLE [dbo].[TRADUCCIONES];
IF OBJECT_ID('[dbo].[CONTROL]', 'U') IS NOT NULL DROP TABLE [dbo].[CONTROL];
IF OBJECT_ID('[dbo].[UsuarioRol]', 'U') IS NOT NULL DROP TABLE [dbo].[UsuarioRol];
IF OBJECT_ID('[dbo].[RolPermiso]', 'U') IS NOT NULL DROP TABLE [dbo].[RolPermiso];
IF OBJECT_ID('[dbo].[Sesion]', 'U') IS NOT NULL DROP TABLE [dbo].[Sesion];
IF OBJECT_ID('[dbo].[Bitacora]', 'U') IS NOT NULL DROP TABLE [dbo].[Bitacora];
IF OBJECT_ID('[dbo].[Usuario_Historial]', 'U') IS NOT NULL DROP TABLE [dbo].[Usuario_Historial];
IF OBJECT_ID('[dbo].[DigitoVerificadorVertical]', 'U') IS NOT NULL DROP TABLE [dbo].[DigitoVerificadorVertical];
IF OBJECT_ID('[dbo].[Usuario]', 'U') IS NOT NULL DROP TABLE [dbo].[Usuario];
IF OBJECT_ID('[dbo].[IDIOMA]', 'U') IS NOT NULL DROP TABLE [dbo].[IDIOMA];
IF OBJECT_ID('[dbo].[Rol]', 'U') IS NOT NULL DROP TABLE [dbo].[Rol];
IF OBJECT_ID('[dbo].[Permiso]', 'U') IS NOT NULL DROP TABLE [dbo].[Permiso];
GO

CREATE TABLE [dbo].[IDIOMA] (
    [id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [nombre] NVARCHAR(50) NOT NULL,
    [activo] BIT NOT NULL DEFAULT 1
);

CREATE TABLE [dbo].[Usuario] (
    [id] INT IDENTITY(1,1) PRIMARY KEY,
    [nombre] VARCHAR(50) NOT NULL UNIQUE,
    [pass] VARCHAR(100) NOT NULL,
    [activo] BIT DEFAULT 1,
    [intentos_fallidos] INT DEFAULT 0,
    [bloqueado_hasta] DATETIME NULL,
    [dvh] BIGINT DEFAULT 0,
    [id_idioma] INT NULL FOREIGN KEY REFERENCES [dbo].[IDIOMA]([id])
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
    [id_padre]   INT         NOT NULL REFERENCES [dbo].[Rol]([id]),
    [id_hijo]    INT         NOT NULL,        -- FK a Rol.id o Permiso.id según tipo_hijo
    [tipo_hijo]  VARCHAR(10) NOT NULL CHECK ([tipo_hijo] IN ('Rol', 'Permiso')),
    PRIMARY KEY ([id_padre], [id_hijo], [tipo_hijo]),
    CHECK ([id_padre] <> [id_hijo] OR [tipo_hijo] = 'Permiso')
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

CREATE TABLE [dbo].[CONTROL] (
    [id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [nombre] NVARCHAR(100) NOT NULL,
    [formulario] NVARCHAR(100) NOT NULL,
    CONSTRAINT UQ_Control_Nombre_Formulario UNIQUE ([nombre], [formulario])
);

CREATE TABLE [dbo].[TRADUCCIONES] (
    [idcontrol] INT NOT NULL FOREIGN KEY REFERENCES [dbo].[CONTROL]([id]),
    [ididioma] INT NOT NULL FOREIGN KEY REFERENCES [dbo].[IDIOMA]([id]),
    [texto] NVARCHAR(500) NULL,
    PRIMARY KEY ([idcontrol], [ididioma])
);
GO

-- ====================================================
-- 2. Seeds (Datos Iniciales)
-- ====================================================

-- Idiomas por defecto
INSERT INTO [dbo].[IDIOMA] ([nombre], [activo]) VALUES ('Español', 1);
INSERT INTO [dbo].[IDIOMA] ([nombre], [activo]) VALUES ('English', 1);

-- Roles
INSERT INTO [dbo].[Rol] ([nombre]) VALUES ('Admin');
INSERT INTO [dbo].[Rol] ([nombre]) VALUES ('User');

-- Permisos
INSERT INTO [dbo].[Permiso] ([nombre]) VALUES ('AccesoAdmin');
INSERT INTO [dbo].[Permiso] ([nombre]) VALUES ('GestionUsuarios');

-- Mapeo Rol-Permiso
INSERT INTO [dbo].[RolPermiso] ([id_padre], [id_hijo], [tipo_hijo]) VALUES (1, 1, 'Permiso'); -- Admin - AccesoAdmin
INSERT INTO [dbo].[RolPermiso] ([id_padre], [id_hijo], [tipo_hijo]) VALUES (1, 2, 'Permiso'); -- Admin - GestionUsuarios

-- Usuario Admin por defecto (apunta al Idioma Español = 1)
DECLARE @AdminId INT;
INSERT INTO [dbo].[Usuario] ([nombre], [pass], [activo], [id_idioma]) VALUES ('admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 1, 1);
SET @AdminId = SCOPE_IDENTITY();
INSERT INTO [dbo].[UsuarioRol] ([id_usuario], [id_rol]) VALUES (@AdminId, 1);

-- Digito Verificador Vertical Inicial
INSERT INTO [dbo].[DigitoVerificadorVertical] ([tabla], [dvv]) VALUES ('Usuario', 0);
GO
