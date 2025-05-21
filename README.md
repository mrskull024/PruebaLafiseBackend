# Prueba tecnica para backed en sistematica Lafise

## Descripcion del proyecto: Uso de arquitectura limpia para mantener escabilidad y testeabilidad del proyecto, Inyeccion de dependencias, Swagger para la documentacion de apis, estructuracion de codigo y separacion de responsabilidades.
-- PruebaLafise.Infraestructure: Contiene el Contexto hacia la base de datos y las clases que cumplen los contratos con los metodos a cumplir de cada entidad
-- PruebaLafise.Domain: Contiene las entidades que son la representacion de las tablas de la base de datos y las interfaces que contienen los contratos con los metodos a cumplir
-- PruebaLafise.Application: Contiene los servicios que implementan las interfaces de los repositorios para mantener separacion de responsabilidades
-- PruebaLafise.API: contiene el web api que se encarga de consumir los servicios de Application para poder consumir los contratos que hacen las operaciones de lectura y ecritura hacia la base de datos.

## Instalacion
Requiere ASP.Net Core y SQLite Studio prevamiente instalado en el equipo

cambie la cadena de conexion en appSettingsDevelopment.json a: "Data Source=PruebaLafiseSqllite.db" en caso de que esta no exista para consumir el archivo con extension .sqlite y ejecuta la solucion para validar cada uno de los endpoints del API disponibles para las pruebas.

-API Clientes se pueden realizar operaciones de creacion, actualizacion, obtencion por id, obtencion de todos y remocion de un cliente.
-API Catalogos, se pueden obtener catalogos de monedas, genero y tipos de transacciones
-API Cuenta de Banco, se pueden realizar operaciones de Crear cuenta bancaria, consulta de saldo, creacion de retiros y depositos, y un resumen de las transacciones asociadas a las cuenta de un cliente
