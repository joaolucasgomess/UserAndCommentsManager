FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

RUN groupadd -g 1001 dotnetgroup && \
    useradd -u 1001 -g dotnetgroup -ms /bin/bash dotnetuser 

USER dotnetuser

COPY --from=build  --chown=dotnetuser:dotnetgroup /app/publish/ .

ENV ASPNETCORE_URLS=http://+:8080 \
    DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true
    
EXPOSE 8080
ENTRYPOINT ["dotnet", "ProducerUserAndCommentsManager.dll"]