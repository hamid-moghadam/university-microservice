FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /src

COPY ["University.sln","University.sln"]

COPY ["Core.API/Core.API.csproj","Core.API/Core.API.csproj"]
COPY ["Core.Modules.EF.Abstraction/Core.Modules.EF.Abstraction.csproj","Core.Modules.EF.Abstraction/Core.Modules.EF.Abstraction.csproj"]
COPY ["Core.Modules.EF.Infrastructure/Core.Modules.EF.Infrastructure.csproj","Core.Modules.EF.Infrastructure/Core.Modules.EF.Infrastructure.csproj"]
COPY ["Core.Application/Core.Application.csproj","Core.Application/Core.Application.csproj"]
COPY ["Core.Domain/Core.Domain.csproj","Core.Domain/Core.Domain.csproj"]
COPY ["Core.Infrastructure/Core.Infrastructure.csproj","Core.Infrastructure/Core.Infrastructure.csproj"]
COPY ["Core.Persistence/Core.Persistence.csproj","Core.Persistence/Core.Persistence.csproj"]
COPY ["Core.Events/Core.Events.csproj","Core.Events/Core.Events.csproj"]

COPY ["Identity.API/Identity.API.csproj","Identity.API/Identity.API.csproj"]
COPY ["OcelotGateway/OcelotGateway.csproj","OcelotGateway/OcelotGateway.csproj"]
COPY ["Curriculum.API/Curriculum.API.csproj","Curriculum.API/Curriculum.API.csproj"]
COPY ["HttpAggregator/HttpAggregator.csproj","HttpAggregator/HttpAggregator.csproj"]

COPY ["HttpAggregator.Events/HttpAggregator.Events.csproj","HttpAggregator.Events/HttpAggregator.Events.csproj"]
COPY ["Identity.Extensions/Identity.Extensions.csproj","Identity.Extensions/Identity.Extensions.csproj"]
COPY ["Curriculum.Events/Curriculum.Events.csproj","Curriculum.Events/Curriculum.Events.csproj"]


RUN dotnet restore "University.sln"

COPY . .
WORKDIR /src/Curriculum.API
RUN dotnet publish --no-restore -c Release -o /app

FROM build-env AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Curriculum.API.dll"]