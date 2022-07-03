#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/RabbitMqClient.WorkerService.Subscriber/RabbitMqClient.WorkerService.Subscriber.csproj", "src/RabbitMqClient.WorkerService.Subscriber/"]
RUN dotnet restore "src/RabbitMqClient.WorkerService.Subscriber/RabbitMqClient.WorkerService.Subscriber.csproj"
COPY . .
WORKDIR "/src/src/RabbitMqClient.WorkerService.Subscriber"
RUN dotnet build "RabbitMqClient.WorkerService.Subscriber.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RabbitMqClient.WorkerService.Subscriber.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RabbitMqClient.WorkerService.Subscriber.dll"]