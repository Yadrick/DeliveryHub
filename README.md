# DeliveryHub

Простое веб-приложение для приемки заказов на доставку.

## Демо-экземпляр

Приложение развернуто на сервере и доступно как рабочий экземпляр для проверки функциональности.

Адрес приложения:

```text
https://a39161-b5f2.n.d-f.pw/
```

Инструкция для локального запуска находится ниже.

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

## Локальный запуск после скачивания из GitHub

Этот раздел предназначен для тех, кто скачивает проект из GitHub и запускает его локально.

### 1. Клонировать репозиторий (или скачать и распаковать архив)

```bash
git clone https://github.com/Yadrick/DeliveryHub
```

Перейти в папку проекта:

```bash
cd DeliveryHub
```

### 2. Проверить установленный .NET SDK

Проект использует .NET 9.

```bash
dotnet --version
```

Если команда не работает или версия ниже 9.x, установите .NET 9 SDK с официального сайта Microsoft.

### 3. Восстановить NuGet-пакеты

Из корня проекта выполни:

```bash
dotnet restore
```

### 4. Собрать проект

```bash
dotnet build
```

Ожидаемый результат:

```text
Build succeeded.
```

### 5. Запустить приложение

```bash
dotnet run --project DeliveryHub.Web
```

После запуска в консоли появятся локальные адреса приложения, например:

```text
https://localhost:5001
http://localhost:5000
```

Откройте в браузере:

```text
https://localhost:5001
```

или основную страницу заказов:

```text
https://localhost:5001/Orders
```

### 6. Что делать, если HTTPS-сертификат не доверенный

Если браузер показывает предупреждение о локальном HTTPS-сертификате, выполните:

```bash
dotnet dev-certs https --trust
```

После этого перезапустите приложение.

### 7. Быстрый запуск одной командной последовательностью

```bash
git clone https://github.com/your-user/DeliveryHub.git
cd DeliveryHub
dotnet restore
dotnet build
dotnet run --project DeliveryHub.Web
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

```text
http://localhost:5000/health
```

Ожидаемый ответ:

```text
Healthy
```
