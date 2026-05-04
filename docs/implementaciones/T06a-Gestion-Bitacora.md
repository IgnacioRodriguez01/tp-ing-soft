# T06a. Gestión de Bitácora

Este documento detalla el análisis, diseño e implementación del subsistema de Bitácora, el cual permite registrar y auditar las actividades realizadas por los usuarios dentro del sistema.

## Objetivo
Proveer un mecanismo centralizado y genérico para el registro de eventos relevantes, permitiendo la trazabilidad de las acciones de los usuarios y facilitando la auditoría del sistema mediante búsquedas combinadas.

## Descripción de la Implementación
La solución se basa en una arquitectura de 4 capas, desacoplando la lógica de registro de la persistencia y la interfaz de usuario.

### 1. Modelo de Datos (Base de Datos)
Se definió una estructura en cumplimiento con la **Tercera Forma Normal (3NF)** para evitar redundancias y garantizar la integridad referencial.

- **Tabla `Bitacora`**:
  - `id` (INT, PK, IDENTITY): Identificador único del registro.
  - `fecha_hora` (DATETIME): Fecha y hora del evento (default `GETDATE()`).
  - `id_usuario` (INT, FK, NULL): Referencia al usuario que realizó la acción. Es nulable para permitir el registro de eventos sin una sesión activa (ej: logins fallidos).
  - `actividad` (VARCHAR): Descripción breve de la acción realizada.
  - `info_asociada` (VARCHAR): Detalles adicionales sobre el evento.

- **Stored Procedures**:
  - `InsertarBitacora`: Centraliza la lógica de inserción.
  - `BuscarBitacora`: Implementa la **búsqueda combinada** por usuario, actividad y rangos de fecha, ordenando los resultados de forma descendente por tiempo.

### 2. Capas de la Aplicación

#### Business Entities (BE)
Se creó la clase `Bitacora` que mapea la estructura de la tabla y expone una propiedad calculada `NombreUsuario` para facilitar la visualización en la interfaz, manejando casos de usuarios anónimos o del sistema.

#### Data Access Layer (DAL)
Se implementó `MapperBitacora.cs`, que utiliza la clase base `Acceso` para ejecutar los procedimientos almacenados mediante parámetros, protegiendo al sistema contra ataques de SQL Injection.

#### Business Logic Layer (BLL)
Se desarrolló `GestorBitacora.cs` utilizando el **patrón Singleton**. Esto permite que cualquier parte del sistema pueda registrar eventos de manera sencilla invocando `GestorBitacora.Instance.RegistrarEvento()`.

#### Graphical User Interface (GUI)
Se implementó `FormBitacora.cs`, una pantalla de administración que permite:
- Filtrar registros por un rango de fechas (Desde/Hasta).
- Filtrar por nombre de usuario o por tipo de actividad.
- Visualizar los resultados en un `DataGridView` configurado para lectura.

## Integración y Auditoría
El sistema registra automáticamente los siguientes eventos relevantes:
- **Login Exitoso**: Se registra el usuario y la hora de inicio.
- **Login Fallido**: Se registra con `id_usuario = NULL` e incluye en la información asociada el nombre de usuario con el que se intentó acceder, facilitando la detección de intentos de intrusión.
- **Logout**: Registra el cierre de sesión manual del usuario.
- **Alta de Usuarios**: Registra la creación de nuevas cuentas, indicando quién realizó la operación.
- **Errores Críticos**: Se capturan las excepciones en los flujos principales (Login, consultas) y se registran en la bitácora con el detalle del mensaje de error para facilitar el diagnóstico técnico.


## Consideraciones Técnicas
- El uso de un método genérico en BLL permite que nuevas entidades o procesos se sumen a la auditoría sin modificar la estructura del motor de bitácora.
- Las búsquedas combinadas se ejecutan de forma eficiente en el servidor SQL mediante el uso de parámetros opcionales en el Stored Procedure.
