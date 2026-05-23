# Resumen de Implementación: T06, T06b y T07

Este documento resume los cambios realizados para cumplir con los requerimientos de la Entrega 2 referidos a la Gestión de Bitácora, Control de Cambios y Dígitos Verificadores.

## Tarea 06 y 06b: Gestión de Bitácora y Control de Cambios

Se implementó un sistema de auditoría y trazabilidad para la entidad `Usuario`.

- **Entidad de Historial**: Se creó `BE.UsuarioHistorial` para representar snapshots de los datos.
- **Persistencia**: Se creó `DAL.MapperUsuarioHistorial` y la tabla `Usuario_Historial` en la base de datos.
- **Lógica de Auditoría**: En `BLL.UsuarioBLL`, los métodos `Actualizar` y `Restaurar` ahora guardan automáticamente el estado anterior del usuario antes de aplicar cambios, registrando quién realizó la acción, cuándo y qué valores se modificaron.
- **Interfaz de Usuario**: Se agregó `GUI.FormHistorialUsuario`, permitiendo a los administradores visualizar el historial de cambios de cualquier usuario y restaurar estados anteriores con un solo clic.

## Tarea 07: Gestión de Dígitos Verificadores (Integridad)

Se implementó un mecanismo robusto para detectar alteraciones externas en la base de datos.

- **Cálculo de DVH**: Se implementó en `SERVICIOS.GestorDV` un algoritmo que calcula un dígito verificador horizontal por cada registro de la tabla `Usuario`, basado en el contenido de sus campos y la posición de los caracteres/atributos.
- **Cálculo de DVV**: Se implementó el cálculo del dígito verificador vertical como la suma (o XOR acumulado) de todos los DVH de la tabla, permitiendo detectar inserciones o eliminaciones externas.
- **Validación al Arranque**: En `GUI.Program.cs` se agregó una llamada a `BLL.IntegridadBLL.VerificarIntegridad()`. Si la base de datos no es consistente (DVH o DVV inválidos), el sistema arroja un error crítico y bloquea el acceso al Login.
- **Gestión Automática**: El sistema recalcula y actualiza los dígitos verificadores automáticamente en cada operación de escritura (Registrar, Actualizar, Restaurar).

## Archivos Creados/Modificados

- **BE**: `Usuario.cs` (modificado), `UsuarioHistorial.cs` (creado).
- **BLL**: `UsuarioBLL.cs` (modificado), `IntegridadBLL.cs` (creado).
- **DAL**: `MapperUsuario.cs` (modificado), `MapperUsuarioHistorial.cs` (creado), `MapperDVV.cs` (creado).
- **SERVICIOS**: `GestorDV.cs` (creado).
- **GUI**: `Program.cs` (modificado), `FormMain.cs` (modificado), `FormHistorialUsuario.cs/Designer.cs` (creados).
- **DB**: `docs/schema-init.sql` (actualizado con nuevas tablas y procedimientos).

---
*Fecha de implementación: 06 de mayo de 2026*
