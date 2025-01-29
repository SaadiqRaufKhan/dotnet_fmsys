# this is a multi stage build dokerfile

# use the official .net image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5222

# build stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS build
WORKDIR /src
# fleetsystem directory will be created within the src directory
COPY ["fleetsystem/fleetsystem.csproj", "fleetsystem/"]
RUN dotnet restore "fleetsystem/fleetsystem.csproj"
COPY . .
# setting the working directory
WORKDIR "/src/fleetsystem"
RUN dotnet build "fleetsystem.csproj" -c Release -o /app/build

# publish stage 
FROM build AS publish
RUN dotnet publish "fleetsystem.csproj" -c Release -o /app/publish

# optimization stage
FROM base AS final
WORKDIR /app
ENTRYPOINT [ "dotnet", "fleetsystem.dll" ]



