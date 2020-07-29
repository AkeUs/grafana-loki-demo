# Logs con Serilog y Loki

El proyecto genera en cada método un estatus http (200, 400, 500) con random, el cual genera un log con [Serilog](https://serilog.net/) y los guarda en [Loki](https://grafana.com/oss/loki/).

Método | Url | Descripción
--- |--- | ---
GET | http://localhost:5000/User | Lista todos los usuarios
POST | http://localhost:5000/User | Crea un usuario
GET | http://localhost:5000/User/{id} | Lista un usuario
DELETE | http://localhost:5000/User/{id} | Elimina un usuario

Modelo para crear un nuevo usuario

``` json
{
    "name": "name lastname",
    "age": 20
}
```

Iniciar el proyecto, es requerido tener instalado [Docker](https://docs.docker.com/get-docker/)

``` bash
docker-compose up -d
```

Ingresar a [Grafana](https://grafana.com/grafana/) en http://localhost:3000 

Username | Password
--- | ---
admin | admin

Agregar el datasource de loki con la siguiente url http://loki:3100