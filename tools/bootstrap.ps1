BEGIN {
    # Set location naar path van het script
    Push-Location $PSScriptRoot
}
END {
    Push-Location $PSScriptRoot
}
PROCESS {
    $exeFilePath = "dapr.exe"
    $workingDir = $Args[0]
    $appId = $Args[1]
    $appPort = $Args[2]
    $daprHttpPort = $Args[3]
    $daprGrpcPort = $Args[4]
    $daprConfig = $Args[5]
    $daprComponentsPath = $Args[6]
    $namespace = $Args[7]
    $command = $Args[8]

    $exeFilePath = (Resolve-Path -LiteralPath "$($PSScriptRoot)\$($exeFilePath)").Path
    $daprConfig = (Resolve-Path -LiteralPath "$($PSScriptRoot)\$($daprConfig)").Path
    $daprComponentsPath = (Resolve-Path -LiteralPath "$($PSScriptRoot)\$($daprComponentsPath)").Path
    $workingDir = (Resolve-Path -LiteralPath "$($PSScriptRoot)\$($workingDir)").Path


    Write-Host $daprComponentsPath
    $env:NAMESPACE = $namespace

    Set-Location $workingDir

    [Array]$arguments = "run", "--app-id", "$appId", "--app-port", "$appPort", "--dapr-http-port", "$daprHttpPort", "--dapr-grpc-port", "$daprGrpcPort", "--log-as-json", "--log-level", "debug", "--config", "$daprConfig", "--components-path", "$daprComponentsPath", "--"
    if ($command -eq "dotnet") {
        $arguments += "dotnet", "run"
    }
    elseif ($command -eq "npm") {
        $arguments += "npm", "run", "dev"
    }

    Write-Host $arguments

    & $exeFilePath $arguments
}
