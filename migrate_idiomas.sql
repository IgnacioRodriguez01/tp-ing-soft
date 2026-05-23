USE [tpingsoft];
GO

-- 1. Create IDIOMA table
IF OBJECT_ID('[dbo].[IDIOMA]', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[IDIOMA] (
        [id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [nombre] NVARCHAR(50) NOT NULL,
        [activo] BIT NOT NULL CONSTRAINT DF_Idioma_Activo DEFAULT 1
    );
    PRINT 'Tabla IDIOMA creada.';
END
GO

-- 2. Create CONTROL table
IF OBJECT_ID('[dbo].[CONTROL]', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[CONTROL] (
        [id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [nombre] NVARCHAR(100) NOT NULL,
        [formulario] NVARCHAR(100) NOT NULL,
        CONSTRAINT UQ_Control_Nombre_Formulario UNIQUE ([nombre], [formulario])
    );
    PRINT 'Tabla CONTROL creada.';
END
GO

-- 3. Create TRADUCCIONES table
IF OBJECT_ID('[dbo].[TRADUCCIONES]', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[TRADUCCIONES] (
        [idcontrol] INT NOT NULL FOREIGN KEY REFERENCES [dbo].[CONTROL](id),
        [ididioma] INT NOT NULL FOREIGN KEY REFERENCES [dbo].[IDIOMA](id),
        [texto] NVARCHAR(500) NULL,
        PRIMARY KEY ([idcontrol], [ididioma])
    );
    PRINT 'Tabla TRADUCCIONES creada.';
END
GO

-- 4. Add id_idioma to Usuario table
IF NOT EXISTS (
    SELECT 1 FROM sys.columns 
    WHERE object_id = OBJECT_ID('[dbo].[Usuario]') 
    AND name = 'id_idioma'
)
BEGIN
    ALTER TABLE [dbo].[Usuario] 
    ADD [id_idioma] INT NULL FOREIGN KEY REFERENCES [dbo].[IDIOMA](id);
    PRINT 'Columna id_idioma agregada a Usuario.';
END
GO

-- 5. Seed default languages if empty
IF NOT EXISTS (SELECT 1 FROM IDIOMA)
BEGIN
    INSERT INTO IDIOMA (nombre, activo) VALUES ('Español', 1);
    INSERT INTO IDIOMA (nombre, activo) VALUES ('English', 1);
    PRINT 'Idiomas Español e English sembrados.';
END
GO

-- 6. Stored Procedures

-- LeerIdiomasActivos
IF OBJECT_ID('[dbo].[LeerIdiomasActivos]', 'P') IS NOT NULL DROP PROCEDURE [dbo].[LeerIdiomasActivos];
GO
CREATE PROCEDURE [dbo].[LeerIdiomasActivos]
AS BEGIN
    SELECT id, nombre, activo FROM IDIOMA WHERE activo = 1;
END
GO

-- LeerTraduccionesPorIdioma
IF OBJECT_ID('[dbo].[LeerTraduccionesPorIdioma]', 'P') IS NOT NULL DROP PROCEDURE [dbo].[LeerTraduccionesPorIdioma];
GO
CREATE PROCEDURE [dbo].[LeerTraduccionesPorIdioma]
    @IdIdioma INT
AS BEGIN
    SELECT t.idcontrol, t.ididioma, c.nombre AS NombreControl, c.formulario AS Formulario, t.texto
    FROM TRADUCCIONES t
    INNER JOIN CONTROL c ON t.idcontrol = c.id
    WHERE t.ididioma = @IdIdioma;
END
GO

-- CrearIdioma
IF OBJECT_ID('[dbo].[CrearIdioma]', 'P') IS NOT NULL DROP PROCEDURE [dbo].[CrearIdioma];
GO
CREATE PROCEDURE [dbo].[CrearIdioma]
    @Nombre NVARCHAR(50),
    @NuevoId INT OUTPUT
AS BEGIN
    INSERT INTO IDIOMA (nombre, activo) VALUES (@Nombre, 1);
    SET @NuevoId = SCOPE_IDENTITY();
END
GO

-- ActualizarTraduccion
IF OBJECT_ID('[dbo].[ActualizarTraduccion]', 'P') IS NOT NULL DROP PROCEDURE [dbo].[ActualizarTraduccion];
GO
CREATE PROCEDURE [dbo].[ActualizarTraduccion]
    @IdControl INT,
    @IdIdioma INT,
    @Texto NVARCHAR(500)
AS BEGIN
    IF EXISTS (SELECT 1 FROM TRADUCCIONES WHERE idcontrol = @IdControl AND ididioma = @IdIdioma)
        UPDATE TRADUCCIONES SET texto = @Texto WHERE idcontrol = @IdControl AND ididioma = @IdIdioma;
    ELSE
        INSERT INTO TRADUCCIONES (idcontrol, ididioma, texto) VALUES (@IdControl, @IdIdioma, @Texto);
END
GO

-- LeerControles
IF OBJECT_ID('[dbo].[LeerControles]', 'P') IS NOT NULL DROP PROCEDURE [dbo].[LeerControles];
GO
CREATE PROCEDURE [dbo].[LeerControles]
AS BEGIN
    SELECT id, nombre, formulario FROM CONTROL;
END
GO

-- CrearControl
IF OBJECT_ID('[dbo].[CrearControl]', 'P') IS NOT NULL DROP PROCEDURE [dbo].[CrearControl];
GO
CREATE PROCEDURE [dbo].[CrearControl]
    @Nombre NVARCHAR(100),
    @Formulario NVARCHAR(100),
    @NuevoId INT OUTPUT
AS BEGIN
    SELECT @NuevoId = id FROM CONTROL WHERE nombre = @Nombre AND formulario = @Formulario;
    IF @NuevoId IS NULL
    BEGIN
        INSERT INTO CONTROL (nombre, formulario) VALUES (@Nombre, @Formulario);
        SET @NuevoId = SCOPE_IDENTITY();
    END
END
GO

-- GuardarIdiomaUsuario
IF OBJECT_ID('[dbo].[GuardarIdiomaUsuario]', 'P') IS NOT NULL DROP PROCEDURE [dbo].[GuardarIdiomaUsuario];
GO
CREATE PROCEDURE [dbo].[GuardarIdiomaUsuario]
    @IdUsuario INT,
    @IdIdioma INT
AS BEGIN
    UPDATE Usuario SET id_idioma = @IdIdioma WHERE id = @IdUsuario;
END
GO

-- LeerIdiomaUsuario
IF OBJECT_ID('[dbo].[LeerIdiomaUsuario]', 'P') IS NOT NULL DROP PROCEDURE [dbo].[LeerIdiomaUsuario];
GO
CREATE PROCEDURE [dbo].[LeerIdiomaUsuario]
    @IdUsuario INT
AS BEGIN
    SELECT i.id, i.nombre, i.activo 
    FROM IDIOMA i
    INNER JOIN Usuario u ON u.id_idioma = i.id
    WHERE u.id = @IdUsuario;
END
GO

PRINT 'Migración de idiomas completada exitosamente.';
GO
