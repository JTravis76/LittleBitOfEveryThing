# Permitting Tool

## Conceptual Overview

(Work-in-Progess)

## Setup

### Database

- Database migration via RedGate Flyway

```cmd
...\WebAppFunction.Api\.azuredeploy\database>npm ci
```

then run

```cmd
>npm run migrate
```

OR if flyway-cli is installed globally.

```cmd
>cd C:\Users\USERPROFILE\repos\PermittingTool-API\WebAppFunction.Api\.azuredeploy\database
>flyway -X -configFiles=config/base.conf,config/environment.conf migrate
```

> NOTE! if connecting to "MS LocalDB" instead of "MS SQL Server", you must follow the instruction below.

### MS LocalDB Setup

- download `jtds-1.3.3-dist.zip` from [here](https://github.com/milesibastos/jTDS/releases)
- Extract All then copy `jtds-1.3.3.jar` to `...\WebAppFunction.Api\.azuredeploy\database\node_modules\flywaydb-cli\bin\drivers`
- Delete or rename `jtds-1.3.1.jar`
- Rename `jtds-1.3.3.jar` to `jtds-1.3.1.jar`
- Get LocalDB Pipe instance via command prompt
  - ```cmd
    >sqllocaldb info MSSQLLocalDB
    ```
- Update "environment.config"
  - flyway.url=jdbc:jtds:sqlserver://./LSA;instance=LOCALDB#BDD8F68B;namedPipe=true

> NOTE! NamedPipe Id is changed everytime LocalDB is restarted.
