# EFCoreRepositoryPattern
###### Change/set default connection string

    "ConnectionStrings": {
      "Default": "Server=.\\SQLExpress;Database=Rajanell.SoccerLeague;Trusted_Connection=True;"
    }

###### Create/Update the database
- In the package manager console, select Infrastructure.Data as default project the  run:
```
  Update-Database
```
