# RabbitMQ Client
A minimal solution for working with RabbitMQ messaging broker

#### Project Type
   - .NET 6
   - Minimal API
   - Service Worker

#### Project dependencies
   - RabbitMQ.Client
   - Newtonsoft.Json
   - Swashbuckle.AspNetCore
----------------------------------------------------------------
#### Install RabbitMQ Management
If you need to install RabbitMQ from docker hub

```sh
docker pull rabbitmq:3-management
```

then for running from the image

```sh
docker run --rm -d --hostname rabbit1 -p 15672:15672 -p 5672:5672 rabbitmq:3-management
```

-----------------------------------------------------------------
#### Run the solution from docker compose
+ Todo comming soon...
