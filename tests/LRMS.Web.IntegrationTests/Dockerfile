FROM mcr.microsoft.com/dotnet/sdk:10.0
WORKDIR /app
ENV DOTNET_NUGET_SIGNATURE_VERIFICATION=false
ENV NUGET_CERT_REVOCATION_MODE=offline
COPY . .
RUN dotnet build ./tests/LRMS.Web.IntegrationTests/LRMS.Web.IntegrationTests.csproj -c Release
CMD ["dotnet", "test", "--filter", "Category=Integration", "--no-build", "-c", "Release"]