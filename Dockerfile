# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
EXPOSE 8080

WORKDIR /src
COPY src .

RUN dotnet restore "ports/WorkerService1/WorkerService1.csproj"
RUN dotnet restore "adapters/MillionRegister.Adapter.MongoDB/MillionRegister.Adapter.MongoDB.csproj"
RUN dotnet restore "core/MillionRegister.Core.Application/MillionRegister.Core.Application.csproj"
RUN dotnet restore "core/MillionRegister.Core.Domain/MillionRegister.Core.Domain.csproj"
RUN dotnet restore "core/MillionRegister.Core.Common/MillionRegister.Core.Common.csproj"

WORKDIR "/src/ports/WorkerService1"
RUN dotnet build "WorkerService1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkerService1.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerService1.dll"]
