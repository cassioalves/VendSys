# VendSys - DEX Data Import API

VendSys is a .NET 9 Web API designed to parse and import DEX audit files from vending machines.  
It supports `.txt` file uploads via multipart/form-data, parses relevant data using a custom parser, and persists the results into a SQL Server database using stored procedures via Dapper.

---

## 📦 Features

- ✅ Upload `.txt` files containing ASCII-based DEX audit data
- ✅ Parse and extract machine-level and product-level metrics
- ✅ Persist data using stored procedures (`InsertDEXMeter`, `InsertDEXLaneMeter`)
- ✅ Basic Authentication middleware
- ✅ Organized architecture with services, interfaces, middlewares, and parsing logic
- ✅ Parallel processing for performance
- ✅ Clean and testable code structure

---

## 🛠 Tech Stack

- [.NET 9](https://dotnet.microsoft.com/en-us/)
- ASP.NET Core Web API
- Dapper
- SQL Server (or LocalDB)
- Stored Procedures
- Swagger/OpenAPI
- C#

---

## 🔐 Authentication

This API uses **Basic Authentication** through a custom middleware.  
To configure credentials, update your `appsettings.json`:

```json
"BasicAuth": {
  "Username": "vendsys",
  "Password": "NFsZGmHAGWJSZ#RuvdiV"
}

