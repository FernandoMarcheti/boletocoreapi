# Stage 0 (prepare image base)
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Stage 1 (copy && restore layers from app)
FROM  mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY *.sln ./
COPY *.config ./
COPY Api/Api.csproj/ Api/
COPY BoletoNetCore/BoletoNetCore.csproj/ BoletoNetCore/
COPY BoletoNetCore.Pdf/BoletoNetCore.Pdf.csproj/ BoletoNetCore.Pdf/
COPY BoletoNetCore.QuestPdf/BoletoNetCore.QuestPdf.csproj/ BoletoNetCore.QuestPdf/
COPY BoletoNetCore.QuestPDF.AppTeste/BoletoNetCore.QuestPDF.AppTeste.csproj/ BoletoNetCore.QuestPDF.AppTeste/
COPY BoletoNetCore.Testes/BoletoNetCore.Testes.csproj/ BoletoNetCore.Testes/

RUN dotnet restore
COPY . .

# Stage 2 (build app)
WORKDIR /src/Api
RUN dotnet build --configuration Release --nologo --no-restore

# Stage 3 (publish release version)
FROM build AS publish
WORKDIR /src/Api
RUN dotnet publish --configuration Release --nologo --no-build -o /app/publish

# Stage 4 (generate final imagem)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]