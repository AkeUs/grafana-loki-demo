# Logs con Serilog y Loki

El proyecto genera en cada endpoint un estatus http (200, 400, 500) con random y los guarda en Loki.

Metodo | Url
--- |---
GET | http://localhost:5000/User
POST | http://localhost:5000/User
GET | http://localhost:5000/User/{id}
DELETE | http://localhost:5000/User/{id}

modelo para crear un nuevo usuario

``` json
{
    "name": "name lastname",
    "age": 20
}
```

iniciar el proyecto

``` bash
docker-compose up -d
```

Ingresar a Grafana en http://localhost:3000 

Username | Password
--- | ---
admin | admin

Agregar el datasource de loki con la siguiente url http://loki:3100
