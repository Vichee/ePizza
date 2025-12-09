param(
    [string]$Environment = "Production",
    [string]$SolutionPath = "D:\ePizaaFinalDownload\ePizza",
    [string]$PublishRoot = "D:\Publish"
)

Write-Host "Starting deployment for $Environment..."

# 1. Find all .csproj files in the solution
$projects = Get-ChildItem -Path $SolutionPath -Recurse -Filter *.csproj

Import-Module WebAdministration

foreach ($proj in $projects) {
    $projectName = [System.IO.Path]::GetFileNameWithoutExtension($proj.Name)
    $publishPath = Join-Path $PublishRoot $projectName
    $siteName = $projectName
    $appPoolName = "${projectName}AppPool"
    $port = 8080

    Write-Host "Publishing $projectName..."
    dotnet publish $proj.FullName -c Release -o $publishPath

    # 2. Create Application Pool if missing
    if (-not (Test-Path "IIS:\AppPools\$appPoolName")) {
        Write-Host "Creating Application Pool: $appPoolName"
        New-WebAppPool -Name $appPoolName
    }
    Set-ItemProperty "IIS:\AppPools\$appPoolName" -Name managedRuntimeVersion -Value ""
    Set-ItemProperty "IIS:\AppPools\$appPoolName" -Name processModel.identityType -Value "ApplicationPoolIdentity"

    # 3. Create IIS site if missing
    if (-not (Get-Website | Where-Object { $_.Name -eq $siteName })) {
        Write-Host "Creating new IIS site: $siteName"
        New-Website -Name $siteName -Port $port -PhysicalPath $publishPath -ApplicationPool $appPoolName
        $port++
    } else {
        Write-Host "Site $siteName already exists. Updating physical path..."
        Set-ItemProperty "IIS:\Sites\$siteName" -Name physicalPath -Value $publishPath
        Set-ItemProperty "IIS:\Sites\$siteName" -Name applicationPool -Value $appPoolName
    }

    Write-Host "Deployment complete for $siteName at http://localhost:$port"
}
