# dapr-workshop
Dapr workshop

## Prerequisites
### 0. Prepare prerequisites
see [Assignment 1](#assignment-1-prepare-prerequisites)
- Copy folder structure from USB key to you hard drive

### 1. Run dapr init  
- run ```dapr.exe uninstall``` first when upgrading dapr!  
- run ```dapr.exe init --slim``` in tools dir  
  
### 2. Install tye  
- ```dotnet tool install -g Microsoft.Tye --version "0.11.0-alpha.22111.1"```

### 3. Install VS code extensions
- [Tye plugin](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-tye)
- [database client](https://marketplace.visualstudio.com/items?itemName=cweijan.vscode-mysql-client2)
- [Rest Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client)


### 4. Powershell
Install powershell


## Assignments

### Assignment 1: Prepare Prerequisites
    - Copy folder structure from USB key to you hard drive
      - consul.exe and otel.exe are to large for this repo. They need to be copied manually

### Assignment 2: Create Tye bootstrap 
- Assignment 2 : Create Tye bootstrap
    - Create tye yaml
    - Run services
        - consul
        - opentelemetry collector
        - Jaeger
        - Redis
        - Mailhog
    - check dashboards
        - consul
        - tye
        - mailhog
        - jaeger

### Assignment 3 : Create the Mail API
- Assignment 3 : Create the Mail API
    - create empty .NET 6 project
    - create minimal API
    - add health check on /health endpoint
    - create POST cronjob endpoint

### Assignment 4 : DAPRize the Mail API

- Assignment 4 : DAPRize the Mail API
    - add DAPR components folder
        - add cron input binding
        - add smtp output binding
        - add secret store compponent
        - add secrets file
    - run Mail API from TYE using bootstrap powershell
        - name the DAPR app : mailapi
    - call DAPR smtp output binding from cronjob endpoint using a DAPR Client
    - add http test file
        - test cronjob endpoint
    - check dashboards
        - consul
        - tye
        - jaeger

### Assignment 5 : Create the Weather API
- Assignment 5 : Create the Weather API
    - create default .NET 6 weather API project
        - add health check on /health endpoint

### Assignment 6 : DAPRize the Weather API
- Assignment 6 : DAPRize the Weather API
    - run Weather API from TYE using bootstrap powershell
        - name the DAPR app : weatherapi
    - check dashboards
        - consul
        - tye

### Assignment 7 : Call the Weather API from the MAIL API
- Assignment 7 : Call the Weather API from the MAIL API
    - add weatherforecast record to mail api
    - call weatherapi from mail api using DAPR service invocation
    - put the first returned weather summary to the body of the email
    - check dashboards
        - jaeger