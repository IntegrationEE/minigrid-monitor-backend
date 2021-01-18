# Minigrid Monitor API

The Minigrid Monitor is a web-based system to monitor and track performance of 
[Minigrids](https://en.wikipedia.org/wiki/Mini-grids) within a whole country.

This repo holds the Backend part and can be seen as independent API. 

## Feature List:
- dedicated User-Mangement
- access performance via Portfolio and Site Level
- map view of all minigrids within the country
- enter data by manual entry, csv and api 
- data validation on entry

## Architecture:
- MVC Arcitecture:
    - Backend .net core 
    - Frontend Angular
    - Database mainly build for Postgres

## Public instances
- [Nigeria MinigridMonitor](https://minigridmonitor.nigeriase4all.gov.ng)


## Development Setup
- you need a running Postgres database 
- for the first setup you can seed the database with your setting including your mail-server, admin cred and so on here: 

        *\minigrid-monitor-backend\MonitorBackend\Monitor.Infrastructure\DatabaseInitializer.cs
- set the connection string for the database here:

        *\minigrid-monitor-backend\MonitorBackend\Monitor.Infrastructure\DbContextFactory.cs
 
        *\minigrid-monitor-backend\MonitorBackend\Monitor.WebApi\appsettings.Development.json

- run the dotnet application 

## Production Setup 
- use the following file for production:

        *\minigrid-monitor-backend\MonitorBackend\Monitor.WebApi\appsettings.Production.json
    
- enter
    - ApiKey
    - AllowedOrigins
    - Authority
    - IdentityServerUrl
    - GuestSecret



