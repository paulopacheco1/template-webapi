# dotnet test --collect:"XPlat Code Coverage"

filters="\
+Template.Application.Services.**.*Handler;\
+Template.Domain.Aggregates.*;\
+Template.Domain.Services.*;\
"
reportgenerator \
    -reports:"/home/paulopacheco/Projetos/ApiTemplate/template-webapi/Template.Tests/TestResults/**/coverage.cobertura.xml" \
    -targetdir:"Template.Tests/TestResults/CoverageReport" \
    -reporttypes:Html \
    -classfilters:$filters