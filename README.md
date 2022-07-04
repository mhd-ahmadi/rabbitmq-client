# RabbitMQ Client
A minimal solution for working with RabbitMQ messaging broker

* Project Type
*   - .NET 6
*   - Minimal API
*   - Service Worker

* Packges
*   - RabbitMQ.Client
*   - Newtonsoft.Json
*   - Swashbuckle.AspNetCore

first you need to install RabbitMQ from docker hub with this command

```sh
docker pull rabbitmq:3-management
```

then for running from the image

```sh
docker run --rm -d --hostname rabbit1 -p 15672:15672 -p 5672:5672 rabbitmq:3-management
```
