FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/runtime:9.0
WORKDIR /app

RUN groupadd -g 1001 dotnetgroup && \
    useradd -u 1001 -g dotnetgroup -ms /bin/bash dotnetuser 

USER dotnetuser

COPY --from=build  --chown=dotnetuser:dotnetgroup /app/publish/ .

ENTRYPOINT ["dotnet", "ConsumerUserAndCommmentsManager.dll"]