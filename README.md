# WishList App

Простое и удобное приложение для создания списков желаний. Позволяет пользователям создавать списки подарков, делиться ими с друзьями и управлять бронированием подарков.

## О проекте

WishList - это веб-приложение, которое помогает пользователям:
- Создавать списки желаемых подарков
- Делиться списками с друзьями
- Отмечать подарки как забронированные
- Управлять своими пожеланиями
- Безопасно регистрироваться и авторизоваться

## Технологии

### Backend
- **.NET 9.0** - основной фреймворк
- **Entity Framework Core** - ORM для работы с базой данных
- **MediatR** - реализация паттерна CQRS
- **FluentValidation** - валидация запросов
- **JWT** - аутентификация
- **MySQL** - система управления базами данных
- **Docker** - контейнеризация приложения
- **Minimal API** - современный подход к созданию API

### Безопасность
- **PBKDF2 с SHA256** - безопасное хэширование паролей
- **JWT токены** - аутентификация
- **FluentValidation** - валидация входных данных

## Функциональность

### Аутентификация и авторизация
- **Регистрация** - создание нового аккаунта
- **Аутентификация** - вход в систему с получением JWT токена
- **Авторизация** - защищенные эндпоинты требуют валидный токен

### Управление вишлистами
- **Создание** - добавление новых подарков в список
- **Просмотр** - просмотр списков пользователей
- **Бронирование** - отметка подарков как забронированных (0/1)
- **Удаление** - удаление подарков из списка

## Установка и запуск

### Предварительные требования
- Docker
- Docker Compose
- .NET 9.0 SDK (для разработки)

### Запуск в Docker
```bash
# Клонируйте репозиторий
git clone <repository-url>
cd WishList

# Запустите приложение
docker-compose up --build
```

Приложение будет доступно по адресу: `http://localhost:8080`

### Локальная разработка
```bash
# Восстановите зависимости
dotnet restore

# Запустите базу данных
docker-compose up mysql -d

# Запустите приложение
dotnet run
```

## API Endpoints

### Аутентификация
**Регистрация пользователя**
```http
POST /wishlist/reg
Content-Type: application/json

{
  "userName": "username",
  "password": "password",
  "birthday": "1990-01-01",
  "email": "user@example.com"
}
```

**Аутентификация**
```http
POST /wishlist/auth
Content-Type: application/json

{
  "userName": "username",
  "password": "password"
}
```

### Управление вишлистами
**Создание вишлиста** (требует авторизацию)
```http
POST /wishlist/create
Authorization: Bearer <token>
Content-Type: application/json

{
  "gift": "Название подарка",
  "url": "https://example.com/link-to-gift",
  "price": 1500
}
```

**Просмотр вишлиста пользователя**
```http
GET /wishlist/chek/{userId}
```

**Бронирование подарка** (требует авторизацию)
```http
POST /wishlist/booked
Authorization: Bearer <token>
Content-Type: application/json

{
  "id": 1,
  "booked": 1
}
```

**Удаление вишлиста** (требует авторизацию)
```http
DELETE /wishlist/delete/{wishListId}
Authorization: Bearer <token>
```

## База данных

### Таблицы

**Users**
- `Id` (PK) - идентификатор пользователя
- `UserName` (string) - имя пользователя
- `UserPassword` (string) - хэш пароля (PBKDF2)
- `Birthday` (DateOnly) - дата рождения
- `Email` (string) - электронная почта
- `WishLists` (Navigation) - список вишлистов пользователя

**WishList**
- `WishListId` (PK) - идентификатор вишлиста
- `UserId` (FK) - ссылка на пользователя
- `Gift` (string) - название подарка
- `Url` (string) - ссылка на подарок
- `Price` (int) - цена
- `Booked` (sbyte) - флаг бронирования (0/1)
- `CreatedAt` (DateTime) - дата создания

### Связи
- Пользователь (User) имеет множество вишлистов (WishList)
- Вишлист принадлежит одному пользователю

## Структура проекта

```
WishList/
├── DbModels/                 # Модели базы данных
├── Feaches/                  # Функциональные модули
│   ├── Aunification/         # Аутентификация
│   │   ├── AuthEndpoint.cs
│   │   ├── AuthHandler.cs
│   │   ├── AuthRequest.cs
│   │   └── AuthResponse.cs
│   ├── Authorization/        # Авторизация
│   │   └── AuthHelper.cs
│   ├── Booked/              # Бронирование подарков
│   │   ├── BookedEndpoint.cs
│   │   ├── BookedHandler.cs
│   │   ├── BookedRequest.cs
│   │   ├── BookedResponse.cs
│   │   └── BookedValidation.cs
│   ├── ChekList/            # Просмотр вишлистов
│   │   ├── ChekListEndpoint.cs
│   │   ├── ChekListHandler.cs
│   │   └── ChekListResponse.cs
│   ├── CreateList/          # Создание вишлистов
│   │   ├── CreateListEndpoint.cs
│   │   ├── CreateListHandler.cs
│   │   ├── CreateListRequest.cs
│   │   ├── CreateListResponse.cs
│   │   └── CreateValidation.cs
│   ├── DeleteWishList/      # Удаление вишлистов
│   │   ├── DeleteWishListEndpoint.cs
│   │   ├── DeleteWishListHandler.cs
│   │   ├── DeleteWishListRequest.cs
│   │   └── DeleteWishListResponse.cs
│   └── Registration/        # Регистрация
│       ├── RegEnapoint.cs
│       ├── RegHandler.cs
│       ├── RegRequest.cs
│       └── RegResponse.cs
├── DodoPizza.Feaches.Autification/
│   └── HashCreater.cs      # Утилиты хэширования
└── Program.cs              # Точка входа приложения
```

## Безопасность

### Хэширование паролей
Используется безопасное хэширование паролей с помощью алгоритма PBKDF2:
- Соль размером 16 байт
- Размер ключа 32 байта
- 100,000 итераций
- Алгоритм SHA256

### JWT Аутентификация
- Токены содержат ID пользователя, имя и email
- Настраиваемое время жизни токенов
- Подпись с использованием симметричного ключа

### Валидация
Все запросы валидируются с помощью FluentValidation:
- Проверка обязательных полей
- Валидация форматов данных
- Проверка бизнес-правил

## Планы по развитию

### Ближайшие задачи
- [ ] Веб-интерфейс
- [ ] Уведомления о бронировании
- [ ] Система комментариев к подаркам
- [ ] Категории подарков
- [ ] Поиск и фильтрация вишлистов

### Будущие возможности
- [ ] Мобильное приложение для iOS
- [ ] Система рекомендаций подарков
- [ ] Интеграция с социальными сетями
- [ ] Система рейтингов и отзывов
- [ ] Поддержка изображений для подарков
- [ ] Публичные/приватные списки

## Отладка

### Просмотр логов
```bash
docker logs wishlist-app
docker logs wishlist-mysql
```

### Проверка здоровья
```bash
docker-compose ps
```

### Миграции базы данных
```bash
# Создание миграции
dotnet ef migrations add MigrationName

# Применение миграций
dotnet ef database update
```

## Команда

**Ufa-Dynamics, Данияр Камаев**

Разрабатывается с любовью для упрощения процесса выбора и дарения подарков.

---

**Примечание**: Этот проект находится в активной разработке. API и функционал могут изменяться.
