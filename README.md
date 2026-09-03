# Blazor To Do

This is a small proof-of-concept to demonstrate common CRUD-operations (Create, Read, Update, Delete) using the .NET Blazor framework and a MariaDB.

To run the project, use the following commands:

```sh
# Start database container
docker compose up -d

# Start application
dotnet watch
```

## Project Setup

### Prerequisites

Please make sure the following items are installed:

* [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
* [Docker Desktop](https://www.docker.com/products/docker-desktop) (for local database hosting)
* [DBeaver](https://dbeaver.io/download) (or anything equivalent that can connect to a MariaDB)

### Database

> This is only required if the database is hosted locally.

1. Create a copy of [`.env.example`](/.env.example) named `.env` and fill out the variables:

    Key                | Description                                  | Default
    ------------------ | -------------------------------------------- | -------
    `DB_PORT`          | The port the database should be forwarded to | `3306`
    `DB_ROOT_PASSWORD` | The root password of your database           | *None*

2. Start the database container

    ```sh
    docker compose up
    ```

3. Connect to the database with DBeaver and run these commands:

    ```sql
    -- Create database
    CREATE DATABASE blazor_todo;

    -- Create Todos table
    CREATE TABLE blazor_todo.Todos (
        id UUID UNIQUE PRIMARY KEY NOT NULL,
        name VARCHAR(30) NOT NULL,
        status INT(1) NOT NULL,
        created DATETIME NOT NULL,
        updated DATETIME
    );
    ```

4. Done

### Application

1. Create a copy of [`appsettings.json`](/appsettings.json) named `appsettings.Development.json` and fill out the variables:

    Key                         | Description                                    | Default
    --------------------------- | ---------------------------------------------- | -------
    `ConnectionStrings.DB_CONN` | The connection string pointing at the database | *None*

    > Refer to the [MariaDB Documentation](https://mariadb.com/docs/connectors/connectors-quickstart-guides/mariadb-connector-net-guide#id-3.-basic-usage) for guidance on how to create your connection string.

2. Restore solution to install dependencies

    ```sh
    dotnet restore
    ```

3. Done
