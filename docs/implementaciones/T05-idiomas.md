# Implementación T05 — Gestión de Múltiples Idiomas (Observer)

Esta documentación describe la arquitectura y los detalles técnicos de la implementación para la gestión dinámica de idiomas utilizando el patrón de diseño **Observer**.

## Arquitectura y Componentes

La solución se divide estrictamente en las capas de la arquitectura del proyecto (GUI -> BLL -> DAL -> BE) sin omitir ninguna física de acuerdo con las reglas de gobierno:

### 1. Business Entities (BE)
- **`BE/Idioma.cs`**: Representa la entidad de un idioma en la base de datos (`Id`, `Nombre`, `Activo`).
- **`BE/Traduccion.cs`**: Representa un registro de traducción para un control particular (`IdControl`, `IdIdioma`, `NombreControl`, `Formulario`, `Texto`).
- **`BE/NombreControl.cs`**: Contiene constantes estáticas para mapear todos los nombres de controles (por ejemplo, `FormLogin_label1`, `FormMain_adminToolStripMenuItem`, etc.).
- **`BE/Usuario.cs`**: Se extendió con la propiedad `IdIdioma` (nullable) para almacenar la preferencia del usuario.

### 2. Data Access Layer (DAL)
- **`DAL/MapperIdioma.cs`**: Realiza las consultas y actualizaciones en la base de datos a través de Stored Procedures parametrizados (`LeerIdiomasActivos`, `LeerTraduccionesPorIdioma`, `CrearIdioma`, `ActualizarTraduccion`, `LeerControles`, `CrearControl`, `GuardarIdiomaUsuario`, `LeerIdiomaUsuario`).
- **`DAL/MapperUsuario.cs`**: Se actualizó para mapear la columna `id_idioma` de la tabla `Usuario`.

### 3. Business Logic Layer (BLL)
- **`BLL/ISujeto.cs`**: Interfaz del sujeto observado (`Adjuntar`, `Separar`, `Notificar`).
- **`BLL/IObservador.cs`**: Interfaz de los observadores (`Actualizar`).
- **`BLL/GestorIdioma.cs`**: Singleton que actúa como el sujeto concreto (`ConcreteSubject`). Almacena la propiedad `IdiomaActual` y administra la lista de formularios activos suscritos, notificándoles un diccionario con las traducciones del idioma seleccionado al ocurrir un cambio.
- **`BLL/IdiomaBLL.cs`**: Administra la lógica de idiomas, implementando un caché en memoria thread-safe (`Dictionary<int, Dictionary<string, string>>`) para evitar accesos repetidos a la base de datos.
- **`BLL/UsuarioBLL.cs`**: Se actualizó para cargar y aplicar el idioma preferido del usuario al iniciar sesión o validar una sesión local.

### 4. Presentation Layer (GUI)
Para traducir los controles de WinForms de manera genérica sin usar herencia de controles customizada (manteniendo compatibilidad completa con el diseñador visual), se implementó una jerarquía de wrappers bajo `GUI/Traduccion/`:
- **`IControlTraducible`**: Interfaz base para elementos traducibles de la vista.
- **`EtiquetaTraducible`**: Wrapper para controles estándar que exponen una propiedad `Text` (`Label`, `Button`, `RadioButton`, `GroupBox`, `Form`).
- **`ComboTraducible`**: Wrapper para `ComboBox` que reconstruye la lista con textos traducidos y mantiene el índice seleccionado.
- **`DataGridTraducible`**: Wrapper para `DataGridView` que traduce los encabezados de columnas específicas.
- **`MenuStripTraducible`**: Wrapper para traducir jerarquías de `ToolStripItem` (`MenuStrip` o `ContextMenuStrip`).

Todos los formularios del sistema implementan `IObservador`, se registran en `GestorIdioma` al crearse (`Adjuntar`) y se desvinculan al cerrarse (`Separar`).

## Base de Datos
Se creó el script de migración `migrate_idiomas.sql` en la raíz del proyecto para crear las tablas necesarias y sus respectivos Stored Procedures:
- Tablas: `IDIOMA`, `CONTROL`, `TRADUCCIONES`.
- Relación: `Usuario` -> `id_idioma` FK `IDIOMA(id)`.

## Flujo de Trabajo para Agregar Nuevos Controles Traducibles
1. Declarar una constante con la clave del control en `BE/NombreControl.cs`.
2. Registrar la clave en el formulario correspondiente dentro del método `RegistrarControlesTraducibles()`.
3. Al abrir la pantalla de **Gestión de Idiomas**, la aplicación detectará y registrará automáticamente cualquier control nuevo en la base de datos, permitiendo su traducción inline desde la grilla.
