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
5. Create **App.config** in folder **ABC.App** and change ConnectionString. Content in [Wiki](https://github.com/HCB2-NPT/foreign-language-center/wiki/App.config).
6. Create **App.config** in folder **ABC.Database** and change ConnectionString. Content in [Wiki](https://github.com/HCB2-NPT/foreign-language-center/wiki/App.config).
5. Type below commands
```PowerShell
clear

enable-migrations -ContextProjectName ABC.Database -StartupProjectName ABC.Database -ContextTypeName ABC.Database.ObjectContexts.MyDatabaseContext -ProjectName ABC.Database -Force

add-migration InitialCreation

update-database -Verbose
```

## Development
1. Ensure you created 2 **App.config** files.
2. Open **Package Manager Console**.
3. Type below commands
```PowerShell
clear

update-database -verbose

update-database
```

#### DO NOT CHANGE CONTENT OF THESE FILES IN ABC.DATABASE: Objects, ObjectContexts, Migrations