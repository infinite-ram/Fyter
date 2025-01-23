#!/bin/bash

echo "Starting EF Core Migrations and Database Updates..."

# Define project paths
ROOT_DIR=$(pwd)
WEBAPP_DIR="$ROOT_DIR/Fyter.WebApp"
EFCORE_SQLITE_DIR="$ROOT_DIR/Plugins/Fyter.Plugins.EFCoreSqlite"

# Define migration name
MIGRATION_NAME="migration_$(date +"%Y-%m-%d_%H-%M-%S")"

# Define context
CONTEXT=${1:-"both"}

if [ "$CONTEXT" == "identity" ]; then
    # Migrate ApplicationDbContext
    echo "Migrating ApplicationDbContext..."
    cd "$WEBAPP_DIR"
    dotnet ef migrations add "$MIGRATION_NAME" -s ./Fyter.WebApp.csproj --context ApplicationDbContext --output-dir Data/Migrations
    dotnet ef database update -s ./Fyter.WebApp.csproj --context ApplicationDbContext

elif [ "$CONTEXT" == "ufc" ]; then
    echo "Migrating UfcSqliteContext..."
    cd "$EFCORE_SQLITE_DIR"
    dotnet ef migrations add "$MIGRATION_NAME" -s ../../Fyter.WebApp/Fyter.WebApp.csproj --context UfcSqliteContext
    dotnet ef database update -s ../../Fyter.WebApp/Fyter.WebApp.csproj --context UfcSqliteContext

else
    # Migrate both contexts
    echo "Migrating ApplicationDbContext..."
    cd "$WEBAPP_DIR"
    dotnet ef migrations add "$MIGRATION_NAME" -s ./Fyter.WebApp.csproj --context ApplicationDbContext
    dotnet ef database update -s ./Fyter.WebApp.csproj --context ApplicationDbContext

    echo "Migrating UfcSqliteContext..."
    cd "$EFCORE_SQLITE_DIR"
    dotnet ef migrations add "$MIGRATION_NAME" -s ../../Fyter.WebApp/Fyter.WebApp.csproj --context UfcSqliteContext
    dotnet ef database update -s ../../Fyter.WebApp/Fyter.WebApp.csproj --context UfcSqliteContext
fi

echo "Migrations and Database Updates Completed."
