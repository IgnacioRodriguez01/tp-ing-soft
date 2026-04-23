- Título	
- Alcance y características funcionales
- Items relevantes

- T01. Arquitectura Base
- Análisis, diseño e implementación. debe incluir esquema de persistencia y diagrama de componentes. Deberán realizarse pruebas unitarias de arquitectura. Considerar una arquitectura de (al menos) cuatro capas. Deberán utilizarse todos los criterios relacionados a la POO (cohesión, acoplamiento, reuso, etc). Se recomienda que la UI sea por medio de formularios tipo MDI. Debe incluír también el mapa tentativo de navegación de los menús del sistema.	
- Arquitectura - esquema de persistencia - diagrama de componentes

- T02. Gestión de Login/Logout y gestión de usuarios
- Análisis, diseño e implementación. Se espera la utilización del patrón Singleton. Permite verificar la identidad del usuario a través del ingreso de su nombre de usuario y su clave, asignándole el perfil que tenga asignado en el sistema. Se debe describir como será la política de ‘log-in’ / ‘log-out‘. También deberán diferenciarse y documentarse los procesos que se correrán en el arranque del sistema, el log in, y el apagado de sistema, el log-out (permisos, audioria, control de integridad, etc). .  Deberá considerar los siguientes items: Objetivo, Descripción detallada de cómo funciona, Diagrama de clases, DER (Si es necesario), Secuencia (Si es necesario) y el diseño de los algoritmos que sean necesarios. Se espera el uso del patrón singleton.
- documentación - implementación - patrón singleton

- T06a. Gestión de Bitácora.
- Análisis, diseño e implementación. En ella deben quedar registradas todas las operaciones que realicen los usuarios durante la utilización del sistema. Esto permitirá hacer un trazado de las actividades desarrolladas por el usuario dentro de la aplicación. Los datos mínimos que la bitácora debe incluir son fecha, hora, usuario, actividad, información asociada con la actividad. El subsistema de bitácora deberá prever la posibilidad de realizar búsquedas por los datos almacenados de manera combinada.  Deberá considerar los siguientes items: Objetivo, Descripción detallada de cómo funciona, Diagrama de clases, DER (Si es necesario), Secuencia (Si es necesario) y el diseño de los algoritmos que sean necesarios.
- documentación - implementación

- T03. Gestión de encriptado
- Análisis, diseño e implementación. La gestión de encriptado es la responsable implementar los algoritmos de encriptación para proteger los datos sensibles del sistema.La gestión de encriptado es la responsable implementar los algoritmos de encriptación para proteger los datos sensibles del sistema.  Se espera la utilización de un algoritmo de encriptado simétrico o asimétrico (por ejemplo, para datos sensibles y la utilización de algún mecanismo de hash (por ejemplo, para las contraseñas)..  Deberá considerar los siguientes items: Objetivo, Descripción detallada de cómo funciona, Diagrama de clases, DER (Si es necesario), Secuencia (Si es necesario) y el diseño de los algoritmos que sean necesarios.
- documentación - implementación 