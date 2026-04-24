# DeliveryOrders

Простое веб-приложение для приемки заказов на доставку.

## Функциональность

Приложение позволяет:

- создать новый заказ на доставку;
- указать город и адрес отправителя;
- указать город и адрес получателя;
- указать вес груза;
- указать дату забора груза;
- посмотреть список созданных заказов;
- открыть заказ из списка в режиме чтения;
- увидеть автоматически сгенерированный номер заказа.

Формат номера заказа:

```text
ORD-yyyyMMdd-000001
```

Пример:

```text
ORD-20260424-000001
```

## Технологии

- .NET 9
- ASP.NET Core MVC
- Entity Framework Core
- SQLite
- FluentValidation
- Bootstrap
- Layered Architecture
- Repository Pattern
- Unit of Work через EF Core DbContext

## Архитектура проекта

Проект построен по Layered Architecture.

```text
DeliveryHub
├── DeliveryHub.Domain
├── DeliveryHub.Application
├── DeliveryHub.Infrastructure
└── DeliveryHub.Web
```

## Требования

Для локального запуска нужны:

- .NET 9 SDK
- установленный `dotnet-ef`, если миграции применяются вручную

Проверить версию .NET:

```bash
dotnet --version
```

Проверить EF Core CLI:

```bash
dotnet ef --version
```

Установить EF Core CLI:

```bash
dotnet tool install --global dotnet-ef --version 9.0.0
```

Обновить EF Core CLI:

```bash
dotnet tool update --global dotnet-ef --version 9.0.0
```

## Автоматическое создание базы данных

Приложение может автоматически создавать базу данных при запуске.

Для этого при старте вызывается:

```csharp
app.ApplyDatabaseMigrations();
```

Этот вызов применяет все ожидающие EF Core migrations.

Если SQLite-база ещё не создана, приложение создаст её автоматически.

## Локальный запуск

Из корня проекта нужно последовательно выполнить следующие команды:

```bash
dotnet restore
```

```bash
dotnet build
```

```bash
dotnet run --project DeliveryHub.Web
```

После запуска приложение будет доступно по адресу, указанному в консоли, например:

```text
https://localhost:5001
```

или:

```text
http://localhost:5000
```

Основная страница:

```text
/Orders
```

## Основные страницы приложения

```text
/Orders
```

Список созданных заказов.

```text
/Orders/Create
```

Форма создания нового заказа.

```text
/Orders/Details/{id}
```

Просмотр созданного заказа в режиме чтения.

## Health Checks

Приложение содержит health check endpoints:

```text
/health
```

```text
/health/ready
```

Проверка локально:

```bash
curl http://localhost:5000/health
```

Ожидаемый ответ:

```text
Healthy
```