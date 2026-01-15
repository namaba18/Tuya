La aplicacion está desarrollada en .Net 6 con clean arquitecture, utilizando entity framework para la base de datos y xUnit para los test. 
Está compuesta por dos modulos, uno para las ordenes y otro para los clientes. 

En appsettings.json se debe poner la cadena de conexion a la base de datos. 
Para poder iniciar la aplicación se debe correr el comando de update-database para que se cree la base de datos y se apliquen las migraciones. 

En la capa de presentation se usa una api que puede ser probada mediante swagger. Donde se encuentran los metodos para mostrar, crear, actualizar y borrar (CRUD) de ambos modulos.

Se debe tener en cuenta que para crear una orden primero se debe crear un cliente, ya que se necesita el Id del customer para su creacion.

