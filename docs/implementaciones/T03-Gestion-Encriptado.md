# Implementación: T03. Gestión de Encriptado & Nueva Capa de Servicios

## Objetivo
Implementar la gestión de encriptado (hashing SHA256) para proteger las contraseñas de los usuarios y refactorizar la arquitectura para introducir una capa de servicios transversales (`SERVICIOS`).

## Cambios Realizados

### 1. Nueva Capa de Servicios (`SERVICIOS`)
Se creó un nuevo proyecto de biblioteca de clases (`SERVICIOS.csproj`) para centralizar las funcionalidades transversales al sistema que no pertenecen estrictamente a la lógica de negocio (BLL) ni al acceso a datos (DAL).
- **Ubicación**: `TpIngSoft/SERVICIOS`
- **Namespace**: `SERVICIOS`

### 2. Gestión de Encriptado (T03)
Se implementó la clase `Encriptador` en la capa de servicios, utilizando el algoritmo **SHA256** para el hashing de contraseñas.
- **Archivo**: `TpIngSoft/SERVICIOS/Encriptador.cs`
- **Funcionalidad**: Método estático `Hash(string value)` que devuelve la representación hexadecimal del hash.

### 3. Reubicación del SessionManager (T02)
Siguiendo la solicitud de desacoplamiento, se movió el `SessionManager` desde `BLL` hacia la nueva capa `SERVICIOS`.
- **Nuevo Archivo**: `TpIngSoft/SERVICIOS/SessionManager.cs`
- **Actualización**: Se cambió el namespace de `BLL` a `SERVICIOS`.

### 4. Integración en la Lógica de Negocio (BLL)
Se actualizó `UsuarioBLL` para integrar el encriptado en los procesos de:
- **Login**: Ahora compara el hash de la contraseña ingresada con el hash almacenado en la base de datos.
- **Registro (Alta)**: Ahora aplica el hash a la contraseña del nuevo usuario antes de persistirla en la base de datos.

### 5. Actualización de la Interfaz Gráfica (GUI)
Se actualizaron los formularios `FormLogin` y `FormMain` para referenciar el nuevo namespace `SERVICIOS`, asegurando que el acceso al Singleton de `SessionManager` siga funcionando correctamente.

### 6. Actualización de la Documentación Arquitectónica
Se actualizaron los archivos de guía de implementación para reflejar la nueva arquitectura de 5 capas:
- `prompts/architecture.md`
- `prompts/module-map.md`
- `prompts/structure.md`

## Verificación
1. **Compilación**: El proyecto compila correctamente con las nuevas referencias entre proyectos.
2. **Seguridad**: Las contraseñas ya no se gestionan en texto plano en la capa de lógica, sino que se transforman en hashes irreversibles antes de cualquier operación de comparación o persistencia.
3. **Desacoplamiento**: La lógica de infraestructura (sesión y seguridad) ahora está aislada de la lógica de negocio pura.
