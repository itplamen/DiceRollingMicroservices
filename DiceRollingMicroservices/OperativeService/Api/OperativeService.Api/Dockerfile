# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OperativeService/Api/OperativeService.Api/OperativeService.Api.csproj", "OperativeService/Api/OperativeService.Api/"]
COPY ["OperativeService/Data/OperativeService.Data/OperativeService.Data.csproj", "OperativeService/Data/OperativeService.Data/"]
COPY ["OperativeService/Data/OperativeService.Data.Models/OperativeService.Data.Models.csproj", "OperativeService/Data/OperativeService.Data.Models/"]
COPY ["OperativeService/Handlers/OperativeService.Handlers.Commands/OperativeService.Handlers.Commands.csproj", "OperativeService/Handlers/OperativeService.Handlers.Commands/"]
COPY ["Common/DiceRollingMicroservices.Common.Models/DiceRollingMicroservices.Common.Models.csproj", "Common/DiceRollingMicroservices.Common.Models/"]
COPY ["OperativeService/Handlers/OperativeService.Handlers.Queries/OperativeService.Handlers.Queries.csproj", "OperativeService/Handlers/OperativeService.Handlers.Queries/"]
COPY ["Common/DiceRollingMicroservices.Common.Utils/DiceRollingMicroservices.Common.Utils.csproj", "Common/DiceRollingMicroservices.Common.Utils/"]
COPY ["OperativeService/Infrastructure/OperativeService.Infrastructure.IoC/OperativeService.Infrastructure.IoC.csproj", "OperativeService/Infrastructure/OperativeService.Infrastructure.IoC/"]
COPY ["MessageBus/DiceRollingMicroservices.MessageBus.Consumer/DiceRollingMicroservices.MessageBus.Consumer.csproj", "MessageBus/DiceRollingMicroservices.MessageBus.Consumer/"]
COPY ["MessageBus/DiceRollingMicroservices.MessageBus.Models/DiceRollingMicroservices.MessageBus.Models.csproj", "MessageBus/DiceRollingMicroservices.MessageBus.Models/"]
COPY ["OperativeService/Infrastructure/OperativeService.Infrastructure.Mapping/OperativeService.Infrastructure.Mapping.csproj", "OperativeService/Infrastructure/OperativeService.Infrastructure.Mapping/"]
COPY ["OperativeService/Api/OperativeService.Api.Models/OperativeService.Api.Models.csproj", "OperativeService/Api/OperativeService.Api.Models/"]
RUN dotnet restore "./OperativeService/Api/OperativeService.Api/OperativeService.Api.csproj"
COPY . .
WORKDIR "/src/OperativeService/Api/OperativeService.Api"
RUN dotnet build "./OperativeService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OperativeService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OperativeService.Api.dll"]