# Paths
$packFolder = (Get-Item -Path "./" -Verbose).FullName
$slnPath = Join-Path $packFolder "../"
$srcPath = Join-Path $slnPath "src"

# List of projects
$projects = (
    "Jane",
    "Jane.AspNetCore",
    "Jane.Autofac",
    "Jane.AutoMapper",
    "Jane.ENode",
    "Jane.ENode.AspNetCore",
    "Jane.FCM",
    "Jane.Hangfire",
    "Jane.Log4Net",
    "Jane.Masstransit.RabbitMq",
    "Jane.Mob.Push",
    "Jane.MongoDB",
    "Jane.QCloud.CKafka",
    "Jane.QCloud.Cos",
    "Jane.QCloud.Im",
    "Jane.QCloud.Sms",
    "Jane.QCloud.Xinge",
    "Jane.RedisCache",
    "Jane.Twilio.Sms",
    "Jane.UMeng.Push",
    "Jane.Web"
)

# Rebuild solution
Set-Location $slnPath
& dotnet restore

# Copy all nuget packages to the pack folder
foreach($project in $projects) {
    
    $projectFolder = Join-Path $srcPath $project

    # Create nuget pack
    Set-Location $projectFolder
    Remove-Item -Recurse (Join-Path $projectFolder "bin/Release")
    & dotnet msbuild /p:Configuration=Release /p:SourceLinkCreate=true
    & dotnet msbuild /t:pack /p:Configuration=Release /p:SourceLinkCreate=true

    # Copy nuget package
    $projectPackPath = Join-Path $projectFolder ("/bin/Release/" + $project + ".*.nupkg")
    Move-Item $projectPackPath $packFolder

}

# Go back to the pack folder
Set-Location $packFolder