# Manual de Uso de Inventario de Productos

Este manual proporciona instrucciones detalladas para usar la aplicación Inventario de Productos. La aplicación permite a los administradores gestionar usuarios y roles de manera eficiente.

##Requisitos Previos
- .Net core SDK
- Micriosoft SQL Server
- Cuenta de Admistrador con acceso completo

##Instalacion
1. Clonar el repositorio: `git clone https://github.com/tu-repositorio/inventario-productos.git`
2. Navegar al directiorio del proyecto: `cd inventario-productos`
3. Restaura paquetes NuGet: `dotnet restore`
4. Configura la cadena de conexión en 
5. Ejecuta las migraciones de base de datos: `dotnet ef database update`
6. Inicia la aplicación: `dotnet run`

## Uso Básico

### Registrarse 
1. Abre la aplicación en tu navegador.
2. Haz clic en el botón **Registrar**.
3. Completa todos los campos del formulario de registro:
   - Nombre de Usuario
   -  Correo Electrónico
     - Contraseña
       - Confirmar Contraseña
4. Selecciona el tipo de usuario:
    - **Usuario Normal**
    - **Administrador**
5. Haz clic en **Registrar** para crear tu cuenta.

### Iniciar Sesión
1. Abre la aplicación en tu navegador.
2. Ingresa tu **Nombre de Usuario** y **Contraseña** en la página de inicio de sesión.
3. Haz clic en **Iniciar Sesión**

 ## Funciones Avanzadas 

 ##Usuarios predeterminados
 -**User Admin**: 
 -admin@admin.com
 -**Contraseña**
 -82D7QokQ*

 ### Para Administradores
1. **Gestión de Usuarios**:
   - Puedes:
   - Editar informacion de los usuarios existentes
   - Eliminar usuarios
2**Crud de Productos**:
- **Crear Producto**:
  1. Ve a la seccion **inventario productos**
  2. Haz click en **Agregar Producto**
  3. Completa los campos necesarios (nombre, descripción, precio, etc.).
  4. Haz clic en **Guardar**.
- **Leer Producto**:
- Navega a la sección **Inventario Productos** para ver la lista completa de productos.
- **Actualizar Producto**:
1. En la sección **Listado de Productos**, selecciona el producto que deseas editar.
2. Haz clic en **Editar**.
3. Realiza las modificaciones necesarias.
4. Haz clic en **Guardar**.
- **Eliminar Producto**:
1. En la sección **Productos**, selecciona el producto que deseas eliminar.
2. Haz clic en **Eliminar**.
3. Confirma la eliminación.

### Para Usuarios Normales 
1. **Visualizar Productos**:
- Navega a la sección **Inventarios de Productos** para ver la lista de productos disponibles.

### Error HTTP 400 
- **Problema**:
El error HTTP 400 aparece al intentar eliminar un usuario.
 - **Solución**: Verifica que el ID del usuario se esté pasando correctamente y que los métodos de controlador y vistas estén configurados adecuadamente.

- ## Contacto

Para soporte adicional, contacta a `jerson_ramos1310@yahoo.es`
