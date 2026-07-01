# Guía de Actualización para Diagramas de Clases y Secuencia

Este documento enumera exhaustivamente los cambios estructurales (atributos, propiedades y métodos agregados, modificados o eliminados) realizados en la rama `fixes-consignas` respecto a `main`. Su objetivo es facilitar la actualización de los **Diagramas de Clases (UML)** y los **Diagramas de Secuencia**.

---

## 1. Capa de Entidades (`BE`)

### Clase `BE.Usuario`
* **Atributos / Propiedades Agregados:**
  * `public string NombrePersona { get; set; }` *(Representa el nombre real del usuario)*
  * `public string Apellido { get; set; }` *(Representa el apellido del usuario)*

### Clase `BE.UsuarioHistorial`
* **Atributos / Propiedades Agregados:**
  * `public string NombrePersona { get; set; }`
  * `public string Apellido { get; set; }`

### Clase `BE.NombreControl`
* **Constantes Agregadas:**
  * `public const string FormMain_verificarIntegridadToolStripMenuItem = "FormMain.verificarIntegridadToolStripMenuItem";`

---

## 2. Capa de Negocio (`BLL`)

### Clase `BLL.UsuarioBLL`
* **Métodos Modificados (Impacto en Diagrama de Secuencia):**
  * `public void Restaurar(int idHistorial)`:
    * *Cambio en Secuencia:* Al recuperar el snapshot del historial, **ya no sobrescribe** las propiedades `Nombre` (username de login) ni `Password` de la entidad en base de datos. Solo envía a `_mapper.Actualizar(...)` los nuevos campos `NombrePersona`, `Apellido` y el estado `Activo`.
  * `public Usuario Login(string nombre, string password)` y `public void ValidarSesionLocal()`:
    * *Cambio en Secuencia:* Tras validar al usuario, verifica si su idioma asignado está inactivo (`!usuario.Activo` en idioma). Si está inactivo, invoca a `_idiomaBLL.ObtenerIdiomasActivos()`, selecciona un idioma fallback activo, reasigna en BD (`_idiomaBLL.GuardarIdiomaUsuario`) y actualiza la sesión actual.

### Clase `BLL.IdiomaBLL`
* **Métodos Modificados (Impacto en Diagrama de Secuencia):**
  * `public void ToggleEstadoIdioma(int idIdioma)`:
    * *Cambio en Secuencia:* Antes de desactivar, consulta `ObtenerIdiomasActivos()`. Si la cantidad es `<= 1`, lanza excepción impidiendo dejar el sistema sin idiomas activos. Si procede a desactivar, llama a `mapper.ReasignarUsuariosIdioma(idIdioma, fallbackId)` para migrar a los usuarios afectados.
  * `public void EliminarIdioma(int idIdioma)`:
    * *Cambio en Secuencia:* En lugar de ejecutar un `DELETE` físico, valida el mínimo de idiomas activos y ejecuta un borrado lógico (soft-delete, seteando `activo = 0`), reasignando también los usuarios asociados mediante `mapper.ReasignarUsuariosIdioma(...)`.

---

## 3. Capa de Acceso a Datos (`DAL`)

### Clase `DAL.MapperUsuario`
* **Métodos Modificados:**
  * `public List<Usuario> ObtenerTodos()`, `public void Insertar(Usuario entity)`, `public void Actualizar(Usuario entity)`:
    * Se amplían las firmas de parámetros SQL y el mapeo de `SqlDataReader` para incluir `@nombre_persona` y `@apellido`.

### Clase `DAL.MapperUsuarioHistorial`
* **Métodos Modificados:**
  * `public List<UsuarioHistorial> ObtenerPorUsuario(int idUsuario)` e `public void Insertar(...)`:
    * Mapeo ampliado para registrar y recuperar `nombre_persona` y `apellido`.

### Clase `DAL.MapperIdioma`
* **Métodos Agregados:**
  * `public void ReasignarUsuariosIdioma(int idIdiomaAnterior, int idIdiomaNuevo)`:
    * *Acción:* Ejecuta el procedimiento almacenado `ReasignarUsuariosIdioma` en BD para traspasar masivamente los usuarios de un idioma desactivado al nuevo idioma fallback.

---

## 4. Capa de Servicios (`SERVICIOS`)

### Clase `SERVICIOS.GestorDV`
* **Métodos Modificados:**
  * `public string CalcularDVH(Usuario user)`:
    * *Cambio:* Incorpora en la cadena de concatenación del hash SHA-256 los valores de `user.NombrePersona` (posición 5) y `user.Apellido` (posición 6).

---

## 5. Capa de Presentación (`GUI`)

### Clase `GUI.FormGestionIdiomas`
* **Atributos / Controles Eliminados:**
  * `- private System.Windows.Forms.Button btnEliminarIdioma;` *(El botón independiente de eliminar fue removido de la interfaz)*
* **Métodos Eliminados:**
  * `- private void btnEliminarIdioma_Click(object sender, EventArgs e);`
* **Métodos Modificados:**
  * `private void btnToggleActivo_Click(object sender, EventArgs e)`:
    * Unifica la acción de activar y desactivar (soft-delete). Incluye validaciones de diálogo de confirmación y cambio dinámico de idioma en la sesión actual si se desactiva el idioma en uso.
  * `private void btnAplicar_Click(object sender, EventArgs e)`:
    * Incorpora validación previa `if (!seleccionado.Activo)` para impedir aplicar idiomas inactivos al sistema.

### Clase `GUI.FormGestionUsuarios`
* **Atributos / Controles Agregados:**
  * `+ private System.Windows.Forms.TextBox txtNombrePersona;`
  * `+ private System.Windows.Forms.TextBox txtApellido;`
* **Métodos Modificados (Secuencia de Carga):**
  * `private void FormGestionUsuarios_Load(object sender, EventArgs e)`:
    * *Cambio en Secuencia:* Al iniciar el formulario, realiza una invocación preventiva a `IntegridadBLL.VerificarIntegridad()` para advertir al administrador en caso de detectar inconsistencias en la base de datos antes de operar.

### Clase `GUI.FormMain`
* **Atributos / Controles Agregados:**
  * `+ private System.Windows.Forms.ToolStripMenuItem verificarIntegridadToolStripMenuItem;`
* **Métodos Agregados:**
  * `+ private void verificarIntegridadToolStripMenuItem_Click(object sender, EventArgs e)`:
    * *Secuencia:* Invoca a `new BLL.IntegridadBLL().VerificarIntegridad()`. Si el reporte indica fallo (`!reporte.EsValido`), muestra un cuadro de diálogo y opcionalmente invoca a `new BLL.IntegridadBLL().RepararIntegridad()` para recalcular y restaurar los DVs en caliente.

---

## Resumen Rápido para Impacto en Diagramas

1. **Diagrama de Clases UML:**
   * Agregar `NombrePersona: string` y `Apellido: string` a `Usuario` y `UsuarioHistorial`.
   * Agregar operación `+ ReasignarUsuariosIdioma(idIdiomaAnterior: int, idIdiomaNuevo: int): void` en `MapperIdioma`.
   * En `FormGestionIdiomas`, eliminar `- btnEliminarIdioma` y su método `btnEliminarIdioma_Click`.
   * En `FormMain`, agregar la opción de menú de verificación de integridad y su respectivo handler `verificarIntegridadToolStripMenuItem_Click`.

2. **Diagramas de Secuencia:**
   * **Secuencia de Restauración de Historial:** Mostrar que `UsuarioBLL.Restaurar()` actualiza la entidad preservando `Password` y `Nombre` originales.
   * **Secuencia de Eliminación/Desactivación de Idioma:** Mostrar el flujo desde `FormGestionIdiomas` $\rightarrow$ `IdiomaBLL.ToggleEstadoIdioma` / `EliminarIdioma` $\rightarrow$ validación de mínimo activo $\rightarrow$ llamada a `MapperIdioma.ReasignarUsuariosIdioma()`.
   * **Secuencia de Verificación de Integridad en Caliente:** Mostrar el flujo desde `FormMain` $\rightarrow$ `IntegridadBLL.VerificarIntegridad()` $\rightarrow$ (opcional si hay daño) $\rightarrow$ `IntegridadBLL.RepararIntegridad()`.
