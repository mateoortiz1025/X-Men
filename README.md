# X-Men

Proyecto API que expone dos recursos, el primero se encarga de validar por medio de una cadena de ADN si una humano es mutante o no, y el segundo expone las estadísticas de los ADN evaluados.

## Requisitos de instalación

_.Net CLI_ para ejecutar por consola la aplicación o visual studio que soporte _.Net Core 3.1_
_pgAdmin_ para conectarse a la instancia de base de datos de _PostgreSQL_

## Iniciando

- _Clonar el proyecto_
- _Abrirlo en visual studio y ejecutar el proyecto API, actualmente el proyecto corre con una instancia de base de datos de AWS_

- _Si desea trabajar con una base de datos local:_
  - _Cree una base de datos desde pgAdmin._
  - _Diríjase al archivo en appsetting.json en el proyecto API y modifique la cadena de conexión y el nombre de la base de datos._
  - _Para aplicar las migraciones y tener la estructura de la base de datos podrá ejecutar el siguiente comando, ubicado en la capa de Infrastructure._

  ```
  Update-Database
  ```

- _Tambien puede ejecutar estos comando en consola para ejecutar la aplicacion (Debe tener instalado .Net Cli)_

  ```
  cd XMEN.Api
  dotnet run
  ```

### Firma de las APIs 📋

La aplicacion utiliza la libreria Swagger para generar la firma de las APIs, para acceder a la interfaz se debe ejecutar la aplicación en el entorno local e ingresar al link https://localhost:5001/ esta sera la pagina por defecto en el ambiende de desarrollo.

![Alt text](https://readme-resources.s3.amazonaws.com/swagger.PNG?raw=true "Optional Title")



## Consumo de API publicada

- El consumo de la API se puede hacer desde [X-Men Collection API](https://www.postman.com/satellite-meteorologist-93675466/workspace/x-men-collection/overview)

- Si prefiere importar el archivo, en la siguiente ruta se encuentra Json correspondiente a la colección de Postman.

```
/XMEN.Infrastructure/PostmanCollections
```

## Test


- Se realizaron test para cada uno de los servicios y controladores.
Para ejecutar los test puede ejecutar estos comandos dentro del proyecto XMEN.Test:


  ```
  cd XMEN.Test
  dotnet build
  dotnet test
  ```
  
  _Resultados_

![Alt text](https://readme-resources.s3.amazonaws.com/test.jpeg?raw=true "Optional Title")



## Despliegue de aplicación 📦

- _Se despliega el proyecto en el servicio Lambda Functions de AWS._
- _Se crea una instancia de base de datos RDS de PostgreSql que está dentro la misma VPC que la función Lambda._
- _Se crea un API Gateway en cual se implementa una etapa de producción que permite consumir los dos endpoints de la API._

## Construido con 🛠️

_El proyecto fue construido en .Net Core 3.1, el tipo es serverless, Se establece una arquitectura por capas, para la base de datos se utilizó PostgreSQL_

