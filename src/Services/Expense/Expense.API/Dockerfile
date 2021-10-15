FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/Expense/Expense.API/Expense.API.csproj", "src/Services/Expense/Expense.API/"]
COPY ["src/Services/Expense/Expense.Infrastructure/Expense.Infrastructure.csproj", "src/Services/Expense/Expense.Infrastructure/"]
COPY ["src/Services/Expense/Expense.Core/Expense.Core.csproj", "src/Services/Expense/Expense.Core/"]
RUN dotnet restore "src/Services/Expense/Expense.API/Expense.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Expense/Expense.API"
RUN dotnet build "Expense.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Expense.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Expense.API.dll"]