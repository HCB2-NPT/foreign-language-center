# foreign-language-center
Registration certificate of foreign language exem such as TOEIC, TOEFL, TFL, JPT,...

## Development Environment
* Windows 10
* Microsoft SQL Server 2008, 2016

## Required
* .NET Framework 4.5
* Entity Framework 6.1.3

## Configuration
1. Edit ConnectionString on App.Config
2. Right click on **ABC.Database** project
3. Set as StartUp Project
4. Tools -> Library Package Manager -> Package Manager Console
5. Type below commands
```
clear

enable-migrations -ContextProjectName ABC.Database -StartupProjectName ABC.Database -ContextTypeName ABC.Database.ObjectContexts.MyDatabaseContext -ProjectName ABC.Database -Force

add-migration InitialCreation

update-database -Verbose
```
