# Backend Catedra 3 Introduccion al desarrollo web/movil

## Requisitos previos
- [.NET] - Versión: 8.0.4
- [Entity Framework Core] - Versión: 8.0.8


# Instalacion
1.- En la carpeta de su preferencia abrir la cmd

2.- En la cmd colocar el siguiente comando para clonar de forma local el repositorio: 
```sh
git clone https://github.com/srCochayuyo/BackendCatedra3.git
```
3.- Abrir el proyecto clonado

4.- Abrir la carpeta del proyecto, puedues utilizar el siguiente comando:
```sh
cd BackendCatedra3
```

5.- Dentro del proyecto, abrir la terminal y ejecuta el siguiente comando:
```sh
dotnet restore
```
6.- En la raiz del proyecto crear un archivo con el nombre ".env"

7.- Copia esta informacion y pegala dentro del archivo .env, esta informacion corresponde a las variables de entorno: 
```sh
DATABASE_URL=DataSource=database.db

JWT_ISSUER=http://localhost:3328
JWT_AUDIENCE=http://localhost:3328
JWT_KEY=Contrasena-Backend-Tercera-Catedra-Introduccion-Desarrollo-Web-Movil12345

Cloudinary_cloudname=dms2bhrnq
Cloudinary_apiKey=559566477817167
Cloudinary_ApiSecret=9guxA1w4pFqJ424dPw6aMyccoHE

```
8.- Finalmente para iniciar el proyecto escribir en la terminal el siguiente comando: 
```sh
dotnet run
``` 

