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
- Creado **[FormGestionPerfiles.cs](file:///c:/Users/ignac/Repos/tp-ing-soft/TpIngSoft/GUI/FormGestionPerfiles.cs)** con un control `TreeView` que muestra recursivamente todos los roles y permisos del sistema. Soporta CRUD de roles, asignación polimórfica de ítems (tanto permisos fijos como sub-roles), edición de nombres y eliminación.
- Modificado `FormMain.cs` para integrar la opción en el menú bajo "Admin -> Gestión de Perfiles" y restringir su acceso sólo a usuarios que posean el permiso `AccesoAdmin`.

---

## Migración de Base de Datos

Los cambios en la persistencia requieren migrar la tabla `RolPermiso` y crear/modificar ciertos stored procedures. El script de migración completo se generó en la raíz del repositorio:

👉 **[migrate_composite.sql](file:///c:/Users/ignac/Repos/tp-ing-soft/migrate_composite.sql)**

### Instrucciones para el usuario:
1. Abra SQL Server Management Studio (SSMS).
2. Conéctese a su servidor local de SQL Server.
3. Abra y ejecute el script `migrate_composite.sql` para aplicar las modificaciones de esquema y stored procedures sobre la base de datos `tpingsoft`.
4. Ejecute la aplicación normalmente.
