# Assignment 5

## Goal

In this assignment we will 'DAPRize' the Weather API and run it from Tye.

After this assignment we know

- how to run this Api from Tye
- how we can test HTTP endpoints using the RestClient VS Code extension

## Steps

### Prerequisite

- Open VS Code
- Open the assignment-5 VS Code workspace from the workspaces folder using "File > Open Workspace from File"

### Step 1. Add Weather Api service to Tye and run it by using the bootstrap powershell script

Open the Tye.yaml file. Add the WeatherApi service to the 'APIs' section underneath the 'Tools' section:

```yaml
- name: weatherapi
  executable: powershell.exe 
  args: -file "../../../tools/bootstrap.ps1" ../src/assignments/assignment-5/WeatherApi weatherapi 5201 3501 60401 ../src/dapr-configuration.yaml ../src/assignments/assignment-5/dapr-components default dotnet
  workingDirectory: .
  replicas: 1
  bindings:
  - protocol: http
    port: 5201

```

You now have 2 API services defined in tye.yaml: mailapi and weatherapi.

### Step 2. Run the Tye solution
Press F5 :)

### Step 3. Test endpoints using REST Client extension

Add two http tests to the test.http file:
1. test weatherforecast Dapr service invocation
```http
### test weatherforecast Dapr service invocation
GET http://localhost:3501/v1.0/invoke/weatherapi/method/weatherforecast HTTP/1.1
```
2. test direct weatherforecast invocation
```http
### test direct weatherforecast invocation
GET http://localhost:5201/weatherforecast HTTP/1.1
```

Within VS Code you will see a 'Send Request' button rendered above each test.

Execute both tests and check the results.

### Step 4. Check the dashboards

Check the dashboard of:
- Consul on http://127.0.0.1:8500/ 

You can see that consul and mailapi and weatherapi are correctly running

- Tye on http://127.0.0.1:8000

You can see that all services including the weatherapi are correctly running

- Jaeger on http://127.0.0.1:16686/

In Jaeger UI you select the weatherapi service and see the DAPR service invocation call you just did from the test.http file.