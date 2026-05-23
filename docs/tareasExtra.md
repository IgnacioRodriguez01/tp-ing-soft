- Título	
- Alcance y características funcionales
- Items relevantes

# Entrega 2

- T10. persistencia de sesión, bloqueos ante multiples intentos
- Implementar que una sesion se cierre por timeout, no por salir del programa. Para esto agregar un chequeo de sesion activa al arrancar el programa, y un tiempo de expiracion de sesion. Este tiempo debe ser guardado en la base de datos. Al entrar al programa, se debe buscar la sesion activa y verificar si el tiempo de expiracion no ha pasado. Si ha pasado, cerrar sesion. De lo contrario, iniciar sesion. 
Además, agregar bloqueos para el usuario ante varios login fallidos. Evaluar si hay algunas validaciones extra para reforzar el login.
- implementación

- T12. Permitir registro de admins
- Permitir que los admins puedan dar de alta a otros admins, eligiendo el rol del usuario a cargar. Esto es una modificacion de la seccion de registro de usuarios. Revisar junto a T04 de consignas.md.
- implementación

- T14. Al cerrar sesión, volver al login.
- Hoy en día, al cerrar Sesión, el programa se cierra. Lo que se espera es que vuelva a la pantalla de Login.
- implementación

- T15. Agregar error de alta de usuario en bitacora
- Al haber un error al ingresar un usuario nuevo, registrar en la bitacora. Agregar la comprobacion de si el usuario ya existe antes de registrarlo. Revisar junto a T04 de consignas.md.
- implementación

# Entrega 3

- Pendiente