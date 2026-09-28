# Shop Database (EF Core)

An **EF Core code-first** data model for an online shop (.NET 8, SQL Server). It practices entity relationships by convention: one-to-many (user → orders, order → items) and many-to-many through a join entity (products ↔ categories).

## Structure

- Models/ — User, Address, Product, Category, ProductCategory, Order, OrderItem, Payment, Review
- Data/DataContext.cs — DbSets and the SQL Server connection
- Migrations/ — initial schema

## Run

```bash
dotnet ef database update --project ShopM
```
