# Implementación T04 — Gestión de Perfiles de Usuario (Patrón Composite)

Este documento detalla la implementación realizada para la gestión de perfiles de usuario utilizando el patrón de diseño Composite.

## Estructura del Patrón

Se implementó la siguiente jerarquía para los perfiles:

```
IComponentePerfil (Interface)
├── Permiso (Leaf / Hoja)
└── Rol (Composite / Compuesto)
```

- **[IComponentePerfil](file:///c:/Users/ignac/Repos/tp-ing-soft/TpIngSoft/BE/IComponentePerfil.cs)**: Define el contrato común (`Id`, `Nombre`, `ObtenerDescripcion()`).
- **[Permiso](file:///c:/Users/ignac/Repos/tp-ing-soft/TpIngSoft/BE/Permiso.cs)**: Actúa como la hoja del patrón. Retorna su descripción en formato `[Permiso] Nombre`.
- **[Rol](file:///c:/Users/ignac/Repos/tp-ing-soft/TpIngSoft/BE/Rol.cs)**: Actúa como el compuesto. Contiene una lista de `IComponentePerfil` (llamada `Permisos`) y proporciona propiedades de conveniencia para filtrar sub-roles y permisos directos (`OfType<Rol>()` y `OfType<Permiso>()`), además de los métodos de gestión del composite (`AgregarPermiso()`, `RemoverPermiso()`, `ObtenerPermisos()`).

## Cambios por Capa

### 1. Entidades de Negocio (BE)
- Creada la interfaz `IComponentePerfil`.
- Modificada la entidad `Permiso` para implementar la interfaz.
- Modificada la entidad `Rol` para soportar jerarquías recursivas (la lista de permisos ahora contiene objetos del tipo de la interfaz).
- Creada la clase de constantes `PermisosDefinidos`.

### 2. Acceso a Datos (DAL)
- Creado **[MapperRol.cs](file:///c:/Users/ignac/Repos/tp-ing-soft/TpIngSoft/DAL/MapperRol.cs)** para encapsular toda la persistencia de los roles y sus jerarquías.
- Modificado `MapperSeguridad.cs` para remover la lectura de roles (migrada al mapper especializado).

### 3. Lógica de Negocio (BLL)
- Creado **[RolBLL.cs](file:///c:/Users/ignac/Repos/tp-ing-soft/TpIngSoft/BLL/RolBLL.cs)** para manejar las operaciones del negocio relativas a los perfiles, incluyendo la carga recursiva del árbol y una rutina robusta de **detección de ciclos** (`TendriaCiclo()`) para prevenir relaciones recursivas infinitas en la base de datos.
- Modificada `UsuarioBLL.cs` para delegar la carga de roles del usuario y poblar su jerarquía recursivamente al iniciar sesión.

### 4. Servicios (SERVICIOS)
- Renombrado **`SessionManager.cs`** completamente a español (`UsuarioActual`, `IdSesion`, `IniciarSesion()`, `CerrarSesion()`, `EstaLogueado()`, `TienePermiso()`).
- Modificado el método `TienePermiso()` para evaluar recursivamente la jerarquía completa del usuario logueado en busca de permisos heredados.

### 5. Interfaz Gráfica (GUI)
- Renombrado el formulario a **Gestión de Roles** (tanto en el título del formulario `FormGestionPerfiles` como en la opción del menú en `FormMain` bajo "Admin -> Gestión de Roles").
- Refactorizado el panel de acciones en **[FormGestionPerfiles.cs](file:///c:/Users/ignac/Repos/tp-ing-soft/TpIngSoft/GUI/FormGestionPerfiles.cs)** implementando un patrón de modos seleccionables mediante `RadioButton` ("Crear Rol" y "Editar / Eliminar") inspirado en el formulario `FormInventario` del parcial:
  - **Modo Crear:** Limpia automáticamente el campo `txtNombreRol` y lo mantiene siempre habilitado. Muestra únicamente los botones para crear roles raíz o sub-roles (este último habilitado solo al seleccionar un rol en el árbol).
  - **Modo Editar:** Habilita el campo `txtNombreRol` con el nombre del rol seleccionado para su modificación. Muestra los botones de guardar cambios, eliminar rol, asignar permisos y desasignar.
  - **Layout Superpuesto:** Para mantener el diseño compacto y una transición fluida, los botones de acción mutuamente excluyentes se superpusieron en la misma posición (Y=135).
- Resueltos bugs críticos de UX/Persistencia:
  - Solucionado el problema donde al seleccionar un permiso el input de texto quedaba bloqueado impidiendo la creación de nuevos roles.
  - Corregido el error de conversión de `DBNull` que ocurría al intentar crear un rol raíz teniendo un rol seleccionado, al desvincular el estado de creación del nodo actualmente seleccionado.


---

## Migración de Base de Datos

Los cambios en la persistencia requieren migrar la tabla `RolPermiso` y crear/modificar ciertos stored procedures. El script de migración completo se generó en la raíz del repositorio:

👉 **[migrate_composite.sql](file:///c:/Users/ignac/Repos/tp-ing-soft/migrate_composite.sql)**

### Instrucciones para el usuario:
1. Abra SQL Server Management Studio (SSMS).
2. Conéctese a su servidor local de SQL Server.
3. Abra y ejecute el script `migrate_composite.sql` para aplicar las modificaciones de esquema y stored procedures sobre la base de datos `tpingsoft`.
4. Ejecute la aplicación normalmente.
