# Minigrid Monitor API

The Minigrid Monitor is a web-based system to monitor and track performance of 
[Minigrids](https://en.wikipedia.org/wiki/Mini-grids) within a whole country.

This repo holds the Backend part and can be seen as independent API. 


## Public instances
- [Nigeria MinigridMonitor](https://minigridmonitor.nigeriase4all.gov.ng)

## Financed By
- [Deutsche Gesellschaft für Internationale Zusammenarbeit (GIZ)](https://www.giz.de/)
- [Federal Ministry of Power, Works and Housing ](http://www.power.gov.ng)
- under the Nigerian Energy Support Programme
- (Co-funded) by the German Government and the European Union

## Authors and Developed By
- [INTEGRATION environment & energy](http://www.integration.org/)
- [Altimi](https://altimi.com/?language=en)

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



