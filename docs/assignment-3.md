# Assignment 3

## Goal

In this assignment we will 'DAPRize' the Mail API and run it from Tye.

After this assignment we

- know how to create/add DAPR components
  - create input and output bindings
  - create secret store with a secrets file
- know how to run this Api from Tye
- know how to trigger the Web Api operation using the DAPR input binding
- know how to call the DAPR output binding to send an email
- know how we can test HTTP endpoints using the RestClient VS Code extension

## Steps

### Prerequisite

- Open VS Code
- Open the assignment-3 VS Code workspace from the workspaces folder using "File > Open Workspace from File"

### Step 1.

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
