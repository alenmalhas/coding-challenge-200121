%userprofile%\.nuget\packages\opencover\4.7.922\tools\OpenCover.Console.exe    -target:"c:\Program Files\dotnet\dotnet.exe"    -targetargs:"test"    -output:coverage.xml    -oldStyle    -filter:"+[ConstructionLine.CodingChallenge*]* -[ConstructionLine.CodingChallenge*Test*]*"     -register:user

%userprofile%\.nuget\packages\ReportGenerator\4.4.5\tools\netcoreapp3.0\ReportGenerator.exe     -reports:coverage.xml     -targetdir:coverage

start coverage\index.htm