# dotnet test --collect:"XPlat Code Coverage"

filters="\
+PetroTemplate.Application.Services.**.*Handler;\
+PetroTemplate.Domain.Aggregates.*;\
+PetroTemplate.Domain.Services.*;\
"
reportgenerator \
    -reports:"/home/paulopacheco/Projetos/IBM/PetroTemplate/PetroTemplate.Tests/TestResults/**/coverage.cobertura.xml" \
    -targetdir:"PetroTemplate.Tests/TestResults/CoverageReport" \
    -reporttypes:Html \
    -classfilters:$filters