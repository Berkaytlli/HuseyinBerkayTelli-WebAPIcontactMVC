cd .\Context
dotnet ef --startup-project ../HuseyinBerkayTelli-WebAPI/ migrations add InitialMigration --context ApplicationDbContext
dotnet ef --startup-project ../HuseyinBerkayTelli-WebAPI/ database update --context ApplicationDbContext