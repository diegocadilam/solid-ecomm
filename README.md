**Criação do banco:**

**Aplicar dependência**

dotnet add ../Ecomm.API/Ecomm.API.csproj package Microsoft.EntityFrameworkCore.Design

Inicializar o banco  
dotnet ef migrations add InitialCreate --project Ecomm.Infrastructure.csproj --startup-project ../Ecomm.API/Ecomm.API.csproj --output-dir Data/Migrations  
dotnet ef database update --project Ecomm.Infrastructure.csproj --startup-project ../Ecomm.API/Ecomm.API.csproj  
 

**Incluir novas entidades**

dotnet ef migrations add CreateCustomerTable --project Ecomm.Infrastructure.csproj --startup-project ../Ecomm.API/Ecomm.API.csproj --output-dir Data/Migrations  
dotnet ef database update --project Ecomm.Infrastructure.csproj --startup-project ../Ecomm.API/Ecomm.API.csproj
