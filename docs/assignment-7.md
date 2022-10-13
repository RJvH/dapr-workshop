# Assignment 7

## Goal

In this assignment we will swap the Dapr secrets component from a file to environment variables. Both approaches are only recommend to be used within local development environments, in a real world example, the secret store could be changed to, for example, Azure keyvault.

After this assignment we know

- how to swap a Dapr component without changing the application code

## Steps

### Prerequisite

- Open VS Code
- Open the assignment-7 VS Code workspace from the workspaces folder using "File > Open Workspace from File"

### Step 1. Find the secret value that is currently in use

open ```test.http``` and find the test for the secret store:

```http
GET http://localhost:3500/v1.0/secrets/dapr-secrets/smtp.user HTTP/1.1
```

the following response should appear:

```http
HTTP/1.1 200 OK
Server: fasthttp
Date: Wed, 08 Jun 2022 13:40:03 GMT
Content-Type: application/json
Content-Length: 24
Traceparent: 00-5c2d78c96bfa88c44b81e75fdf106a6b-def8b6bbb3e2d9e3-01
Connection: close

{
  "smtp.user": "username"
}
```

### Step 2. Make use of the DAPR SDK to read the secrets in code

Find the line within Program.cs in the MailAPI project where the mailbody is created (line 35):

```c#
string body = $"Wow, it's {result.FirstOrDefault().Summary}!";
```

Change this code into:

```c#
var superSecretSecret = await daprClient.GetSecretAsync("dapr-secrets", "smtp.password");
string body = $"Wow, it's {result.FirstOrDefault().Summary}! - By the way, the super secret smtp password is {string.Join(", ", superSecretSecret)}";
```

This code gets the super secret smtp password from the secret building block and this secret will be injected in the mail body. When this code is executed, you will receive a mail which contains the smtp password which has been defined in the ```secrets.json```


### Step 3. Change the Dapr secrets component from file to environments variable

Add a new file called ```secretstores.local.env.yaml```. Add the following content to it:

```yml
apiVersion: dapr.io/v1alpha1
kind: Component
metadata:
  name: dapr-secrets
  namespace: default
spec:
  type: secretstores.local.env
  version: v1
  metadata:
```

### Step 4. Change the name of the local file secret store

In order to prevent naming conflicts, the name of the existing component must be changed.

- Find the dapr component in ```\src\dapr-components\secretstores.local.file.yaml```
- Change the "name" to: dapr-secrets-local-file (the value in the file, not the filename)

### Step 5. add environment variables to your dapr workloads in order to use secrets

Environment variables can be specified in various ways:

- specify name/value in yaml:
  
  ```yaml
  name: key
  value: value
  ```

- specify key/value pairs:
  ```name=value```
- point to a file with key/value pairs in it
  
  ```yaml
  key1=value1
  # comments are possible
  key2="value2"
  ```

In order to add environment variables, take the following steps:

- Open up ```\src\tye.yaml```
- Find the entry for ```Mailhog```
  - Add the following lines at the end of the Mailhog section:
  
    ```yaml
    env_file:
    - ./envfile.env
    ```

  > **note** the envfile.env is already present in this assignment

- Find the entry for ```MailAPI```
  - Add the following lines at the end of the MailAPI section:

  ```yaml
  env_file:
  - ./envfile.env
  env:
  - SharedName=MailAPISpecificKey
  - Mailapi_key=mailapi
  ```

- Find the entry for ```WeatherAPI```
  - Add the following lines at the end of the WeatherAPI section:

  ```yaml
  env:
  - name: SharedName
    value: WeatherAPISpecificKey
  ```

### Step 6. Find the new secret values

restart tye. Using the same http test, another secret value (which resides in the envfile.env) should be shown.

open ```test.http``` and find the test for the secret store:

```http
GET http://localhost:3500/v1.0/secrets/dapr-secrets/smtp.user HTTP/1.1
```

Please note that another value is shown as opposed to the value in step 1. Using this approach, specific secrets can be used amongst different servcies. When getting the secret value for "SharedName" from "MailAPI" or "WeatherAPI", different values are shown. This can be tested using the two following tests in ```test.http```:

```http
GET http://localhost:3500/v1.0/secrets/dapr-secrets/SharedName HTTP/1.1
```

```http
GET http://localhost:3501/v1.0/secrets/dapr-secrets/SharedName HTTP/1.1
```

> **NOTE**: using environment variables, also make all shell scoped environment variables available as a secret. By running the last test, a bulk request will be made and all defined environment variables will be shown

```http
GET http://localhost:3501/v1.0/secrets/dapr-secrets/bulk HTTP/1.1
```

When running the code, you will find out that the mails that are received, no longer contain the initial super secret value, but will contain the value of the environment variable that has been defined in the ```envfile.env```, instead of the ```secrets.json```. With other words: without having to modify the code, you was able to implement another secret provider. In this example the provider has been changed from a file-based to an environment based building block, but with the same effort, this could have been changed into, for example, Azure Keyvault or Hashicorp Vault.
