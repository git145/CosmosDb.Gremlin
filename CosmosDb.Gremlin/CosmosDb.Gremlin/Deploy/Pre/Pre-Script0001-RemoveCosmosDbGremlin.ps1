$serviceName = 'CosmosDb.Gremlin'
If (Get-Service $serviceName -ErrorAction SilentlyContinue) {

    If ((Get-Service $serviceName).Status -eq 'Running') {

        Stop-Service $serviceName
        Write-Output "Stopping $serviceName"
    } Else {
        Write-Output "$serviceName found, but it is not running."
    }
$service = Get-WmiObject -Class Win32_Service -Filter "Name='$serviceName'"
$service.delete()

} Else {
    Write-Output "$serviceName not found"
}
Remove-Variable serviceName