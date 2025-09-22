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
COPY ["UserDataService/Api/UserDataAccessService.Api/UserDataAccessService.Api.csproj", "UserDataService/Api/UserDataAccessService.Api/"]
COPY ["UserDataService/Handlers/UserDataAccessService.Handlers.Commands/UserDataAccessService.Handlers.Commands.csproj", "UserDataService/Handlers/UserDataAccessService.Handlers.Commands/"]
COPY ["Common/DiceRollingMicroservices.Common.Models/DiceRollingMicroservices.Common.Models.csproj", "Common/DiceRollingMicroservices.Common.Models/"]
COPY ["MessageBus/DiceRollingMicroservices.MessageBus.Models/DiceRollingMicroservices.MessageBus.Models.csproj", "MessageBus/DiceRollingMicroservices.MessageBus.Models/"]
COPY ["MessageBus/DiceRollingMicroservices.MessageBus.Producer/DiceRollingMicroservices.MessageBus.Producer.csproj", "MessageBus/DiceRollingMicroservices.MessageBus.Producer/"]
COPY ["UserDataService/Data/UserDataService.Data.Models/UserDataAccessService.Data.Models.csproj", "UserDataService/Data/UserDataService.Data.Models/"]
COPY ["UserDataService/Data/UserDataService.Data/UserDataAccessService.Data.csproj", "UserDataService/Data/UserDataService.Data/"]
COPY ["UserDataService/Handlers/UserDataAccessService.Handlers.Queries/UserDataAccessService.Handlers.Queries.csproj", "UserDataService/Handlers/UserDataAccessService.Handlers.Queries/"]
COPY ["UserDataService/Infrastructure/UserDataAccessService.Infrastructure.IoC/UserDataAccessService.Infrastructure.IoC.csproj", "UserDataService/Infrastructure/UserDataAccessService.Infrastructure.IoC/"]
COPY ["UserDataService/Infrastructure/UserDataAccessService.Infrastructure.Mapping/UserDataAccessService.Infrastructure.Mapping.csproj", "UserDataService/Infrastructure/UserDataAccessService.Infrastructure.Mapping/"]
COPY ["UserDataService/Api/UserDataAccessService.Api.Models/UserDataAccessService.Api.Models.csproj", "UserDataService/Api/UserDataAccessService.Api.Models/"]
COPY ["Common/DiceRollingMicroservices.Common.Utils/DiceRollingMicroservices.Common.Utils.csproj", "Common/DiceRollingMicroservices.Common.Utils/"]
COPY ["OperativeService/Data/OperativeService.Data.Models/OperativeService.Data.Models.csproj", "OperativeService/Data/OperativeService.Data.Models/"]
RUN dotnet restore "./UserDataService/Api/UserDataAccessService.Api/UserDataAccessService.Api.csproj"
COPY . .
WORKDIR "/src/UserDataService/Api/UserDataAccessService.Api"
RUN dotnet build "./UserDataAccessService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UserDataAccessService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserDataAccessService.Api.dll"]