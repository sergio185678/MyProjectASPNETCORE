# Proyecto C# ASP.NET Core: Guía para Desarrollo de API REST

Este proyecto sirve como base y guía para la creación de otros proyectos similares, implementando los conceptos básicos de una API REST utilizando C# y ASP.NET Core.

Nota: Debido a que esta desplegado el backend en una capa gratuita podría resultar en que la aplicación no cargue de inmediato. Si esto sucede, te recomiendo dejarla cargando; eventualmente, se inicializará y la aplicación funcionará correctamente.

Usuario con cargo administrador: 
  - Email: sergio16@gmail.com
  - Contraseña: 123

Link desplegado del backend: [[https://myprojectjavaspringboot.onrender.com](https://myprojectjavaspringboot.onrender.com/swagger-ui/index.html)](http://myprojectapinetcore.somee.com/swagger/index.html)
Creditos: https://somee.com/

![image](https://github.com/sergio185678/MyProjectASPNETCORE/assets/67492035/67cf0cdd-9570-4497-a89e-bb491910b945)

Link del repositorio del frontend:

## Estructura del Proyecto:

- **Controllers**: Gestiona todas las solicitudes HTTP. Se integra con Service, Models y Utils mediante inyección de dependencias.

- **Models**:
   Aqui se utilizo el comando que mediante una database ya creado en MysqlServer creo todas las entidades y su contexto
  - **Dto**: Representa las entidades del sistema, especialmente diseñadas para trabajar con las peticiones HTTP entrantes y salientes.
  - **Entidad**: Representa las tablas de la base de datos.

- **Service**: Contiene la lógica de negocio, trabajando en conjunto con al Context y los modelos.

- **Utils**: Contiene mi configuracion para el JWT y la encriptación.

Este proyecto utiliza una base de datos simple que incluye entidades de Usuario, Documento y Cargo.

## Características Adicionales:

Aparte de las funcionalidades básicas de una API CRUD simple, se implementaron las siguientes características:

- Se instalo los diferentes paquetes: Microsoft.EntityFramework.Core.SqlServer , Microsoft.EntityFramework.Core.Tools , X.PagedList (para la paginacion) , Microsoft.AspNetCore.Authentication.JwtBearer (para el JWT)

- Capa de seguridad:
  - Encriptación de las contraseñas de los usuarios.
  - Implementación de JWT, configurando los claims, la clave y el sujeto, y proporcionando funciones para facilitar la manipulación de los valores dentro del JWT.
  - Asignación de permisos específicos a cada rol de usuario.
  
- Se implemento paginación, donde utilize el paquete ya mencionado y empleando un poco de logica para que regrese un dto de paginación correspondiente a lo que quiero: IPagedList<UsuarioDto> content, int totalElementos, int totalPages, int numberOfElements, bool last.

- Implementación de un buscador que utiliza un dato de entrada para buscar y devolver la lista de usuarios que cumplan con los criterios.

- Administración completa de archivos, permitiendo la carga y descarga de documentos, almacenándolos en la carpeta "uploads".
