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
In this assignment we will create the solution outline. We will create a Tye YAML file with a set of defined services to start.

After this assignment we
- know how te define and run 'executables' from within Project Tye

### [Assignment 2 : Create the Mail API](./docs/assignment-2.md)
In this assignment we will create a .NET6 minimal Web Api.

After this assignment we
- know how to create a new .NET6 minimal Web Api project
- how to create a POST operation on this Api
- how to run the Api from VS Code
- how to check the OpenApi swagger page and test the API

### [Assignment 3 : DAPRize the Mail API](./docs/assignment-3.md)
In this assignment we will 'DAPRize' the Mail API and run it from Tye.

After this assignment we 
- know how to create/add DAPR components
    - create input and output bindings
    - create secret store with a secrets file
- know how to run this Api from Tye
- know how to trigger the Web Api operation using the DAPR input binding
- know how to call the DAPR output binding to send an email
- know how we can test HTTP endpoints using the RestClient VS Code extension


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