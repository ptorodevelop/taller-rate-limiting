# Taller Rate Limiting con .NET

Este proyecto corresponde al taller referente a la sesión de **Rate Limiting con .NET**, donde se implementa la obtención de cotizaciones de acciones desde base de datos, reemplazando los datos hardcoded.

---

## 📌 Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [SQL Server Management Studio (SSMS)](https://aka.ms/ssmsfullsetup) (opcional para administrar la BD)

---

## ⚙️ Configuración

1. Clonar el proyecto:
   ```bash
   git clone https://github.com/tu-usuario/taller-rate-limiting.git
   cd taller-rate-limiting

2. Configurar la conexión a la base de datos en appsettings.json:
    ```bash
    "ConnectionStrings": {
      "QuotesDb": "Server=(localdb)\\MSSQLLocalDB;Database=QuotesDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
    },

3. Crear la base de datos y aplicar migraciones:
     ```bash
     a. dotnet ef migrations add InitialCreate
     b. dotnet ef database update

     


