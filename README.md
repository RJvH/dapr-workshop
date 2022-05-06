# Dapr workshop

This dapr workshop focuses on setting op a micro-services architecture running on your local computer using Project Tye and DAPR without having the need to setup a local kubernetes cluster and can be run without docker. 

In this workshop you will be using the following DAPR building blocks:
- Service Invocation
- Bindings
- Secrets Management

We will use Tye to run all components, including the DAPR sidecars, the workloads and all other supporting services.

After having done all assigments, the final state of this workhop will be:

![architecture](./docs/images/architecture.excalidraw.svg)
1. [DAPR Service Invocation](https://docs.dapr.io/developing-applications/building-blocks/service-invocation/)
    We will use a synchronous service invocation from the MailApi to the WeatherApi using the Service Invocation Building block
2. [DAPR Secrets Management](https://docs.dapr.io/developing-applications/building-blocks/secrets/)
    We will retrieve secrets using Secrets Management and will swap the secret store being used
3. [DAPR Consul name resolution](https://docs.dapr.io/reference/components-reference/supported-name-resolution/setup-nr-consul/)
    Dapr will register the workload service using the Consul name resolition component
4. [DAPR Distributed Tracing](https://docs.dapr.io/developing-applications/building-blocks/observability/tracing-overview/)
    We will configure DAPR to use zipkin formatted traces and send them to the OpenTelemetry Collector
5. [DAPR Input Bindings](https://docs.dapr.io/developing-applications/building-blocks/bindings/bindings-overview/)
    We will use the DAPR cron input binding to trigger calls to the MailApi service
6. [Consul checks](https://www.consul.io/docs/discovery/checks)
    Consul will frequently check the health of all registered services
7. [DAPR Output Bindings](https://docs.dapr.io/developing-applications/building-blocks/bindings/bindings-overview/)
    We will use the DAPR SMTP output binding to send an email
8. [OpenTelemetry Collector Exporter](https://opentelemetry.io/docs/collector/configuration/#exporters)
    The OpenTelemetry Collector will send all received traces to Jaeger



## Workshop approach

This dapr and tye workshop consists of several assignments. The start of each assignment, is the end of the previous assignment. The start of each assignment can be found in the assignments folder as well, this way it's possible to start the workshop at every assignment.

## Prerequisites
### 0. Prepare prerequisites
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

### [Assignment 1: Create Tye bootstrap](./docs/assignment-1.md)
- Open the assignment 1 workspace from the workspaces folder using the File>Open Workspace From File option in VSCode.
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

### Assignment 2 : Create the Mail API
- create empty .NET 6 project
- create minimal API
- add health check on /health endpoint
- create POST cronjob endpoint
- run the MaiApi using dotnet run

### Assignment 3 : DAPRize the Mail API
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

### Assignment 4 : Create the Weather API
- create default .NET 6 weather API project
    - add health check on /health endpoint

### Assignment 5 : DAPRize the Weather API
- run Weather API from TYE using bootstrap powershell
    - name the DAPR app : weatherapi
- check dashboards
    - consul
    - tye

### Assignment 6 : Call the Weather API from the MAIL API
- add weatherforecast record to mail api
- call weatherapi from mail api using DAPR service invocation
- put the first returned weather summary to the body of the email
- check dashboards
    - jaeger