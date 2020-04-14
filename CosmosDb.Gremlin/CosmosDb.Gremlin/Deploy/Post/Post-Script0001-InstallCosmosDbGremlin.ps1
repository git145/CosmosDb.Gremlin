$serviceName = 'CosmosDb.Gremlin'
$sourceRoot = "Evoke.Core\Configurations"
$destinationRoot = "c:\Evoke.Core\CosmosDb.Gremlin\Service\Configuration"

if(Test-Path -Path $sourceRoot)
{
	Copy-Item -Path $sourceRoot -Filter "*.*" -Recurse -Destination $destinationRoot -Container -Force
}

Write-Output "$serviceName installing"
new-service -Name $serviceName -DisplayName $serviceName -Description "Service to Support the CosmosDb.Gremlin Application" -BinaryPathName "c:\Evoke.Core\CosmosDb.Gremlin\Service\CosmosDb.Gremlin.exe" -StartupType Automatic
Write-Output "$serviceName starting"

If (Get-Service $serviceName -ErrorAction SilentlyContinue) {
    Write-Output "$serviceName found"
    If ((Get-Service $serviceName).Status -eq 'Running') {

        Stop-Service $serviceName
        Write-Output "Stopping $serviceName"
    } Else {
        Write-Output "$serviceName found, but it is not running."
    }
	Start-Service $serviceName
	Write-Output "$serviceName started"
} Else {
	Write-Output "$serviceName not found"
	Throw "$serviceName not found"
}
Remove-Variable serviceName