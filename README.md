# Тестовое задание
## Условие:
Реализовать HTTP REST API для обеспечения работы библиотеки видеоигр.


В приложении должны быть реализованы CRUD операции с играми:
- создание,
- получение списка игр с возможностью фильтрации по жанру,
- изменение,
- удаление.


Запись об игре содержит данные:
- название,
- студия разработчик,
- 1 или более жанров, которым соответствует игра.


При выполнении задания требуется использовать:
- ASP.NET Core для реализации HTTP REST API приложения,
- Entity Framework Core для работы работы с любой реляционной базой данных.


Примечание:
- Entity Framework Core рассматривается как заменяемая зависимость, поэтому ожидается абстрагирование от него.

## Решение:
Решение представлено в виде HTTP REST API приложения с Swagger UI в качестве пользовательского интерфейса.


Было реализованы методы для объекта Game
- POST: /api/games/create - создание,
- GET: /api/games/getlist - получение списка игр,
- GET: /api/games/getlistbygenre - получение списка игр с фильтрацией по жанру,
- PUT: /api/games/update - изменение,
- DELETE: /api/games/delete/{id} - удаление.


А также дополнительные GRUD операции для взаимодействия с сущностями Developer и Genre.


Посмотреть все доступные API методы можно после запуска проекта по ссылке:  https://localhost:7199/swagger/index.html


Также был реализован паттерн Repository для вынесения логики взаимодействия с базой данных в отдельный слой.

### Примечание:
Для подключения базы данных в файле appsettings.Development.json должна быть строка подключения:

    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=5432;Database=dbname;User Id=userid;Password=password;"
    }
  
### Стек:
- C#
- ASP.NET Core 8
- Entity Framework Core
- PostgreSQL 

