# Resumen de Fixes e Implementaciones Adicionales

Este documento detalla los fixes y requerimientos especiales implementados para solucionar problemas de integridad, control de cambios e idiomas en la aplicación.

---

## 1. Control de Cambios y Gestión de Usuarios

Se rediseñó el sistema de restauración e historial para evitar alterar campos críticos de acceso y autenticación.

- **Campos Descriptivos**: Se incorporaron los atributos `NombrePersona` y `Apellido` en `BE.Usuario` y `BE.UsuarioHistorial`.
- **Restauración Segura**: En `BLL.UsuarioBLL.Restaurar(int idHistorial)`, se configuró para que al restaurar un registro del historial **no se incluyan ni alteren la contraseña (`Password`) ni el nombre de usuario de inicio de sesión (`Nombre`)**. De esta manera, se pueden corregir errores descriptivos sin alterar credenciales críticas.
- **Validaciones en UI**:
  - En `FormGestionUsuarios`, se agregaron campos interactivos para Nombre y Apellido.
  - En `FormHistorialUsuario`, se muestran los nombres reales y apellidos en la grilla del historial, y se añadió una advertencia descriptiva al usuario antes de confirmar la restauración sobre la preservación de credenciales.

---

## 2. Control de Idiomas (Soft Delete y Validaciones)

Se implementaron restricciones para evitar la inoperabilidad de la interfaz por falta de idiomas activos.

- **Soft Delete de Idiomas**: El procedimiento `EliminarIdioma` fue modificado para realizar un borrado lógico (setear `activo = 0`).
- **Validación de Idioma Único**: Tanto en `ToggleEstadoIdioma` como en `EliminarIdioma` (BLL), se verifica que el idioma a desactivar no sea el último idioma activo en el sistema, lanzando una excepción si es el caso.
- **Reasignación Automática**: Al desactivar un idioma, se ejecuta el procedimiento `ReasignarUsuariosIdioma` para migrar de forma masiva en la base de datos a los usuarios que tenían dicho idioma asignado, apuntándolos a otro idioma activo. De igual forma, en `Login` y `ValidarSesionLocal` se comprueba si el idioma del usuario fue desactivado, reasignándoles el primer idioma activo disponible.
- **Unificación de Interfaz**: Se removió el botón físico `btnEliminarIdioma` de `FormGestionIdiomas`, unificando su lógica con la de activación/desactivación en el botón `btnToggleActivo` (etiquetado como **Activar / Desactivar (Eliminar)**).
- **Prevención de Aplicar Idioma Inactivo**: En `FormGestionIdiomas.cs` (`btnAplicar_Click`), se añadió una validación para impedir que el usuario aplique al sistema un idioma que ha sido desactivado/soft-deleted, mostrando un aviso preventivo.

---

## 3. Integridad de Dígitos Verificadores en Caliente

Se extendieron las capacidades de verificación de consistencia.

- **Verificación en Sesión Activa**: Se agregó la opción **Admin -> Verificar Integridad de Datos** en el menú de `FormMain`. Permite a los administradores corroborar los DVH/DVV en caliente sin reiniciar la aplicación.
- **Reparación Online**: Si se detecta corrupción en caliente, la opción permite realizar una reparación automática mediante el recálculo y actualización de los DVs de toda la tabla de usuarios.
- **Advertencia Preventiva**: Al abrir el panel de Gestión de Usuarios (`FormGestionUsuarios`), se realiza un escaneo rápido del estado de consistencia del sistema para alertar al administrador antes de cualquier modificación.

---

## Archivos Creados/Modificados

- **BE**: `Usuario.cs`, `UsuarioHistorial.cs`, `NombreControl.cs`.
- **BLL**: `UsuarioBLL.cs`, `IdiomaBLL.cs`.
- **DAL**: `MapperUsuario.cs`, `MapperUsuarioHistorial.cs`, `MapperIdioma.cs`.
- **GUI**: `FormGestionUsuarios.cs/.Designer.cs`, `FormHistorialUsuario.cs`, `FormGestionIdiomas.cs/.Designer.cs`, `FormMain.cs/.Designer.cs`.
- **DB**: `docs/schema-init.sql` (columnas, función `CalcularDVH` y semillas), `docs/stored-procedures.sql` (SPs actualizados).

---
*Fecha de implementación: 30 de junio de 2026*
