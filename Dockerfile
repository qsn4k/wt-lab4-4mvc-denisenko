FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["wt-lab4-4mvc-denisenko.csproj", "."]
RUN dotnet restore "wt-lab4-4mvc-denisenko.csproj"

COPY . .
RUN dotnet publish "wt-lab4-4mvc-denisenko.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://0.0.0.0:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "wt-lab4-4mvc-denisenko.dll"]
