$solutionName = "NswagTest"
$apiProject = "NswagTest.Api"
$clientProject = "NswagTest.Client"
$generatorProject = "NswagTest.ClientGenerator"
$version = Get-Date -Format "yy.M.d.HHmm"

Set-Location src

# Build project
Set-Location $apiProject
dotnet build

# Generate swagger.json
dotnet swagger tofile --output "..\swagger.json" "bin\Debug\net6.0\$apiProject.dll" v1

# Generate client files
Set-Location ..
dotnet $generatorProject\bin\Debug\net6.0\$generatorProject.dll --swaggerFile .\swagger.json --output $clientProject\Generated\Client.cs

Remove-Item "swagger.json"

# Generate nuget package
dotnet pack -p:PackageVersion=$version $clientProject\$clientProject.csproj --configuration release --output pack\