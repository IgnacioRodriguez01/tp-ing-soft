USE [TpIngSoft];
GO

-- ====================================================
-- BuscarUsuarioPorNombre
-- ====================================================
IF OBJECT_ID('[dbo].[BuscarUsuarioPorNombre]', 'P') IS NOT NULL 
    DROP PROCEDURE [dbo].[BuscarUsuarioPorNombre];
GO

CREATE PROCEDURE [dbo].[BuscarUsuarioPorNombre]
    @Nombre VARCHAR(50)
AS
BEGIN
    SELECT id, nombre, pass, activo, intentos_fallidos, bloqueado_hasta, dvh
    FROM Usuario 
    WHERE nombre = @Nombre AND activo = 1;
END
GO

-- ====================================================
-- BuscarUsuarioPorId
-- ====================================================
IF OBJECT_ID('[dbo].[BuscarUsuarioPorId]', 'P') IS NOT NULL 
    DROP PROCEDURE [dbo].[BuscarUsuarioPorId];
GO

CREATE PROCEDURE [dbo].[BuscarUsuarioPorId]
    @Id INT
AS
BEGIN
    SELECT id, nombre, pass, activo, intentos_fallidos, bloqueado_hasta, dvh
    FROM Usuario 
    WHERE id = @Id;
END
GO

-- ====================================================
-- LeerUsuarios
-- ====================================================
IF OBJECT_ID('[dbo].[LeerUsuarios]', 'P') IS NOT NULL 
    DROP PROCEDURE [dbo].[LeerUsuarios];
GO

CREATE PROCEDURE [dbo].[LeerUsuarios]
AS
BEGIN
    SELECT id, nombre, pass, activo, intentos_fallidos, bloqueado_hasta, dvh
    FROM Usuario;
END
GO

-- ====================================================
-- CrearUsuario
-- ====================================================
IF OBJECT_ID('[dbo].[CrearUsuario]', 'P') IS NOT NULL 
    DROP PROCEDURE [dbo].[CrearUsuario];
GO

CREATE PROCEDURE [dbo].[CrearUsuario]
    @Nombre VARCHAR(50),
    @Pass VARCHAR(100),
    @DVH BIGINT,
    @NuevoId INT OUTPUT
AS
BEGIN
    INSERT INTO Usuario (nombre, pass, activo, dvh)
    VALUES (@Nombre, @Pass, 1, @DVH);
    SET @NuevoId = SCOPE_IDENTITY();
END
GO

-- ====================================================
-- ActualizarUsuario
-- ====================================================
IF OBJECT_ID('[dbo].[ActualizarUsuario]', 'P') IS NOT NULL 
    DROP PROCEDURE [dbo].[ActualizarUsuario];
GO

CREATE PROCEDURE [dbo].[ActualizarUsuario]
    @Id INT,
    @Nombre VARCHAR(50),
    @Pass VARCHAR(100),
    @Activo BIT,
    @DVH BIGINT
AS
BEGIN
    UPDATE Usuario
    SET nombre = @Nombre, pass = @Pass, activo = @Activo, dvh = @DVH
    WHERE id = @Id;
END
GO

-- ====================================================
-- ActualizarIntentosFallidos
-- ====================================================
IF OBJECT_ID('[dbo].[ActualizarIntentosFallidos]', 'P') IS NOT NULL 
    DROP PROCEDURE [dbo].[ActualizarIntentosFallidos];
GO

CREATE PROCEDURE [dbo].[ActualizarIntentosFallidos]
    @Nombre VARCHAR(50),
    @Exitoso BIT
AS
BEGIN
    IF @Exitoso = 1
    BEGIN
        UPDATE Usuario SET intentos_fallidos = 0, bloqueado_hasta = NULL WHERE nombre = @Nombre;
    END
    ELSE
    BEGIN
        UPDATE Usuario SET intentos_fallidos = intentos_fallidos + 1 WHERE nombre = @Nombre;
        -- Bloqueo si llega a 3 intentos
        UPDATE Usuario SET bloqueado_hasta = DATEADD(MINUTE, 5, GETDATE()) 
        WHERE nombre = @Nombre AND intentos_fallidos >= 3;
    END
END
GO

-- ====================================================
-- LeerRolesPorUsuario
-- ====================================================
IF OBJECT_ID('[dbo].[LeerRolesPorUsuario]', 'P') IS NOT NULL 
    DROP PROCEDURE [dbo].[LeerRolesPorUsuario];
GO

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

-- ====================================================
-- LeerPermisosPorRol
-- ====================================================
IF OBJECT_ID('[dbo].[LeerPermisosPorRol]', 'P') IS NOT NULL 
    DROP PROCEDURE [dbo].[LeerPermisosPorRol];
GO

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

-- ====================================================
-- CrearSesion
-- ====================================================
IF OBJECT_ID('[dbo].[CrearSesion]', 'P') IS NOT NULL 
    DROP PROCEDURE [dbo].[CrearSesion];
GO

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

-- ====================================================
-- CerrarSesion
-- ====================================================
IF OBJECT_ID('[dbo].[CerrarSesion]', 'P') IS NOT NULL 
    DROP PROCEDURE [dbo].[CerrarSesion];
GO

CREATE PROCEDURE [dbo].[CerrarSesion]
    @IdSesion INT
AS
BEGIN
    UPDATE Sesion
    SET fecha_logout = GETDATE()
    WHERE id = @IdSesion;
END
GO

-- ====================================================
-- LeerRoles
-- ====================================================
IF OBJECT_ID('[dbo].[LeerRoles]', 'P') IS NOT NULL 
    DROP PROCEDURE [dbo].[LeerRoles];
GO

CREATE PROCEDURE [dbo].[LeerRoles]
AS
BEGIN
    SELECT id, nombre 
    FROM Rol;
END
GO

-- ====================================================
-- AsignarRolUsuario
-- ====================================================
IF OBJECT_ID('[dbo].[AsignarRolUsuario]', 'P') IS NOT NULL 
    DROP PROCEDURE [dbo].[AsignarRolUsuario];
GO

CREATE PROCEDURE [dbo].[AsignarRolUsuario]
    @IdUsuario INT,
    @IdRol INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM UsuarioRol WHERE id_usuario = @IdUsuario AND id_rol = @IdRol)
    BEGIN
        INSERT INTO UsuarioRol (id_usuario, id_rol) 
        VALUES (@IdUsuario, @IdRol);
    END
END
GO

-- ====================================================
-- ValidarYRefrescarSesion
-- ====================================================
IF OBJECT_ID('[dbo].[ValidarYRefrescarSesion]', 'P') IS NOT NULL 
    DROP PROCEDURE [dbo].[ValidarYRefrescarSesion];
GO

CREATE PROCEDURE [dbo].[ValidarYRefrescarSesion]
    @IdSesion INT,
    @IdUsuario INT OUTPUT
AS
BEGIN
    SELECT @IdUsuario = id_usuario 
    FROM Sesion 
    WHERE id = @IdSesion AND fecha_logout IS NULL;

    IF @IdUsuario IS NOT NULL
    BEGIN
        UPDATE Sesion 
        SET fecha_login = GETDATE() 
        WHERE id = @IdSesion;
    END
    ELSE
    BEGIN
        SET @IdUsuario = -1;
    END
END
GO

-- ====================================================
-- InsertarBitacora
-- ====================================================
IF OBJECT_ID('[dbo].[InsertarBitacora]', 'P') IS NOT NULL 
    DROP PROCEDURE [dbo].[InsertarBitacora];
GO

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

-- ====================================================
-- BuscarBitacora
-- ====================================================
IF OBJECT_ID('[dbo].[BuscarBitacora]', 'P') IS NOT NULL 
    DROP PROCEDURE [dbo].[BuscarBitacora];
GO

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

-- ====================================================
-- InsertarUsuarioHistorial
-- ====================================================
IF OBJECT_ID('[dbo].[InsertarUsuarioHistorial]', 'P') IS NOT NULL 
    DROP PROCEDURE [dbo].[InsertarUsuarioHistorial];
GO

CREATE PROCEDURE [dbo].[InsertarUsuarioHistorial]
    @IdUsuario INT, 
    @Nombre VARCHAR(50), 
    @Pass VARCHAR(100), 
    @Activo BIT, 
    @DVH BIGINT,
    @IdUsuarioAutor INT, 
    @TipoOperacion VARCHAR(20)
AS 
BEGIN
    INSERT INTO Usuario_Historial (id_usuario, nombre, pass, activo, dvh, id_usuario_autor, tipo_operacion)
    VALUES (@IdUsuario, @Nombre, @Pass, @Activo, @DVH, @IdUsuarioAutor, @TipoOperacion);
END;
GO

-- ====================================================
-- LeerHistorialUsuario
-- ====================================================
IF OBJECT_ID('[dbo].[LeerHistorialUsuario]', 'P') IS NOT NULL 
    DROP PROCEDURE [dbo].[LeerHistorialUsuario];
GO

CREATE PROCEDURE [dbo].[LeerHistorialUsuario]
    @IdUsuario INT
AS 
BEGIN
    SELECT * FROM Usuario_Historial WHERE id_usuario = @IdUsuario ORDER BY fecha_cambio DESC;
END;
GO

-- ====================================================
-- BuscarHistorialPorId
-- ====================================================
IF OBJECT_ID('[dbo].[BuscarHistorialPorId]', 'P') IS NOT NULL 
    DROP PROCEDURE [dbo].[BuscarHistorialPorId];
GO

CREATE PROCEDURE [dbo].[BuscarHistorialPorId]
    @IdHistorial INT
AS 
BEGIN
    SELECT * FROM Usuario_Historial WHERE id_historial = @IdHistorial;
END;
GO

-- ====================================================
-- LeerDVV
-- ====================================================
IF OBJECT_ID('[dbo].[LeerDVV]', 'P') IS NOT NULL 
    DROP PROCEDURE [dbo].[LeerDVV];
GO

CREATE PROCEDURE [dbo].[LeerDVV]
    @Tabla VARCHAR(50)
AS 
BEGIN
    SELECT dvv FROM DigitoVerificadorVertical WHERE tabla = @Tabla;
END;
GO

-- ====================================================
-- ActualizarDVV
-- ====================================================
IF OBJECT_ID('[dbo].[ActualizarDVV]', 'P') IS NOT NULL 
    DROP PROCEDURE [dbo].[ActualizarDVV];
GO

CREATE PROCEDURE [dbo].[ActualizarDVV]
    @Tabla VARCHAR(50),
    @DVV BIGINT
AS 
BEGIN
    UPDATE DigitoVerificadorVertical SET dvv = @DVV WHERE tabla = @Tabla;
END;
GO
