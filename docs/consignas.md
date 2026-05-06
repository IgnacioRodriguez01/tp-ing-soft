- Título	
- Alcance y características funcionales
- Items relevantes

# Entrega 1

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

# Entrega 2

- T04. Gestión de Perfiles de Usuario, Composite
- Análisis, diseño e implementación. Se espera la utilización del patrón composite. Se deberán utilizar funciones recursivas para mostrar el árbol de permisos en un control de usuario tipo TreeView. Deberá considerar los siguientes items: Objetivo, Descripción detallada de cómo funciona, Diagrama de clases, DER (Si es necesario), Secuencia (Si es necesario) y el diseño de los algoritmos que sean necesarios.
- documentación - implementación - patrón composite

- T05. Gestión de Múltiples Idiomas, Observer
- Análisis, diseño e implementación. Se espera un modelo reutilizable no acoplado con la UI. Se espera la utilización del patron observer, sin la utilización de hojas de recursos estáticos. Debe permitir el cambio de idioma de todas las leyendas y títulos que se lean en las interfaces de usuario. El cambio debe ser dinámico. Este concepto implica que desde el sistema se puedan incorporar nuevos idiomas y las leyendas que estén afectadas al mismo. Deberá considerar los siguientes items: Objetivo, Descripción detallada de cómo funciona, Diagrama de clases, DER (Si es necesario), Secuencia (Si es necesario) y el diseño de los algoritmos que sean necesarios.
- documentación - implementación - patrón observer

- T06. Gestión de Bitácora y Control de Cambios
- Refinar análisis y diseño. Implementación.
- documentación - implementación

- T06b. Control de cambios
- De, al menos, los cambios realizados durante el ciclo de vida de alguna entidad. Se entiende que esta funcionalidad responde al concepto de auditoria y se espera que el sistema permita realizar una trazabilidad detallada sobre todos los cambios realizados en la entidad elegida. De esta manera responde a "quién?", "cuándo?" y "qué?" proponiendo un historial y con la posibilidad de recomponer el estado anterior de un objeto determinado
-

- T07. Gestión de DV
- Análisis, diseño e implementación. La función de los dígitos verificadores es la de permitir comprobar la integridad de los datos almacenados en la base de datos. Se desea poder detectar dos cosas. La primera es si se han agregado o quitado datos de la base de datos por fuera del sistema y la segunda es si se han intercambiado datos de posición. Para esto último es importante, al momento de determinar el algoritmo de cálculo a emplear, que en el cálculo no sólo participe el contenido del atributo sino también la posición del carácter y la posición del atributo dentro de la entidad. Al iniciar la aplicación, y antes de dar acceso a la ventana de log-in, se debe realizar el proceso de verificación de integridad de la base de datos. En caso de error, se deberá informar al administrador para que tome las medidas adecuadas. Los dígitos verificadores horizontales se guardan en un atributo de las entidades bajo análisis mientras que los verticales se pueden guardar en una entidad adicional creada para ese fin, la cual deberá formar parte del DER. Se espera que la verificación de integridad se realice en al menios una entidad de negocio, siendo esta la más sensible y relevante del sistema. Cuando se desarrolle la especificación correspondiente a esta funcionalidad se deberá contemplar: Detalle de cómo se utilizarán los dígitos verificadores horizontal y vertical. Descripción de las operaciones de restauración a realizar en caso de error en alguno de ellos. Algoritmo a implementar para los cálculos. SE valorará el diseño de un mecanismo genérico para que sea aplicable a cualquier entidad. Deberá considerar los siguientes items: Objetivo, Descripción detallada de cómo funciona, Diagrama de clases, DER (Si es necesario), Secuencia (Si es necesario) y el diseño de los algoritmos que sean necesarios. En caso de que el estado de un objeto se proviene de varias tablas, deberá proponer una solución al problema de la integridad.
- documentación - implementación
