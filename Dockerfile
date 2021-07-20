
# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
# ARG BUILD_CONFIGURATION=Debug
# ENV ASPNETCORE_ENVIRONMENT=Development
# ENV DOTNET_USE_POLLING_FILE_WATCHER=true
# ENV ASPNETCORE_URLS=http://+:80
EXPOSE 5001
EXPOSE 5000

# Create app directory
RUN mkdir -p /source/API
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY API/*.csproj ./API/
RUN dotnet restore

# copy everything else and build app
COPY API/. ./API/
WORKDIR /source/API
# RUN dotnet publish -c release -o /app --no-restore

# # final stage/image
# FROM mcr.microsoft.com/dotnet/aspnet:5.0
# WORKDIR /app
# COPY --from=build /app ./
# ENTRYPOINT ["dotnet", "API.dll"]

###################### END ######################



# Andrei:
###############

# # Install app dependencies
# COPY package.json /usr/src/smart-brain-api
# RUN npm install

# # Bundle app source
# COPY . /usr/src/smart-brain-api

# # Build arguments
# ARG NODE_VERSION=8.11.1
 
# # Environment
# ENV NODE_VERSION $NODE_VERSION 





######################
