# 📚 Контрольная работа № 2  
### Тема: Синхронное межсервисное взаимодействие  
**Студент**: Насрулаев Шарапудин Махадович 

**Группа**: БПИ234

---

## 1. Общая структура и назначение проекта

Данный проект реализует микросервисную архитектуру, в которой взаимодействуют три основных сервиса:  
- Сервис хранения файлов  
- Сервис анализа текста  
- Шлюз (API Gateway), объединяющий обращения к микросервисам через единый вход  

Все сервисы запускаются с помощью **Docker Compose** и обмениваются данными синхронно через HTTP. Также используется база данных **PostgreSQL** для хранения информации о загруженных файлах.

---

## 2. Структура проекта

```
PlagiarismChecker/
├── ApiGateway/              # Шлюз для маршрутизации запросов между сервисами
├── FileStorageService/      # Хранение текстовых файлов и данных о них
├── FileAnalysisService/     # Анализ текста: слова, символы, абзацы
├── PlagiarismChecker.Tests/ # Модульные тесты для всех сервисов
├── docker-compose.yml       # Поднимает всю инфраструктуру
└── README.md                # Описание и инструкция
```

---

## 3. Назначение микросервисов

### 🗂 FileStorageService
- Загрузка и хранение `.txt` файлов
- Сохранение информации о файлах в PostgreSQL
- REST API:
  - `POST /api/fileupload` — загрузка файла
  - `GET /api/fileupload` — список всех файлов
  - `GET /api/fileupload/{filename}` — содержимое файла

### 🧠 FileAnalysisService
- Подсчет:
  - слов
  - символов
  - абзацев
- REST API:
  - `POST /api/analyze/{filename}` — анализ файла
- Использует `TextAnalyzer` для обработки текста

### 🌐 ApiGateway
- Шлюз, объединяющий все входящие запросы
- Получает файл из `FileStorageService` и отправляет его в `FileAnalysisService`
- Открыт по порту `8080`

---

## 4. Запуск проекта


### ⚙️ Команды запуска:

1. Запуск контейнеров:
   ```bash
   docker-compose up --build
   ```

2. После запуска доступны интерфейсы:
   - `http://localhost:8080/swagger` — API Gateway
   - `http://localhost:8081/swagger` — FileStorageService
   - `http://localhost:8082/swagger` — FileAnalysisService

  
3. Открытие интерфейса в браузере
    
🌐 Пользователь открывает:

```bash
http://localhost:8080/swagger
```
И видит интерфейс Swagger UI

✅ Это главное окно, откуда он взаимодействует с системой

---


## 5. Работа с базой данных

### 🐘 PostgreSQL
- Хост: `postgres` (внутри Docker)
- Порт: `5432`
- Имя БД: `filestorage`
- Пользователь: `admin`
- Пароль: `password`

### 🧱 EF Core миграции

Если при запуске возникают ошибки, связанные с отсутствием таблиц — выполни миграции вручную:

```bash
# Пример (вне контейнера)
cd FileStorageService
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## 6. Взаимодействие сервисов

```text
[Client/Swagger UI]
       │
       ▼
[ApiGateway]
       │
       ├─> [FileStorageService] — Получение содержимого файла
       │
       └─> [FileAnalysisService] — Отправка текста на анализ
```

- Пользователь загружает файл через Gateway
- Gateway пересылает его в FileStorageService
- При анализе Gateway получает файл → пересылает в FileAnalysisService → возвращает результат

---

## 7. Тестирование

Проект включает **модульные тесты** для всех сервисов:

📁 `PlagiarismChecker.Tests`:

- 🔹 `TextAnalyzerTests.cs` — проверяет работу анализа текста  
- 🔹 `FileUploadControllerTests.cs` — проверяет загрузку файла (InMemory БД)  
- 🔹 `GatewayControllerTests.cs` — проверяет HTTP-прокси вызов к FileAnalysisService

### ✅ Запуск тестов:
Через терминал:
```bash
dotnet test
```

Или через Visual Studio → `Тест → Выполнить все тесты`

---

## 8. Swagger-интерфейсы

После запуска ты можешь протестировать API через Swagger UI:

| Сервис               | Swagger URL                     |
|----------------------|----------------------------------|
| **ApiGateway**       | `http://localhost:8080/swagger` |
| **FileStorageService** | `http://localhost:8081/swagger` |
| **FileAnalysisService**| `http://localhost:8082/swagger` |

---

## 9. Краткая демонстрация

📄 **Загрузка файла через FileStorageService:**

```
POST /api/fileupload
+ файл .txt
```

📤 **Анализ текста через ApiGateway:**

```
POST /api/analyze/{filename}
→ получает текст → отправляет в FileAnalysisService
→ возвращает статистику (слов, символов, абзацев)
```

🧾 Пример ответа:
```json
{
  "words": 34,
  "characters": 212,
  "paragraphs": 3
}
```

---

## 10. Итог

В рамках контрольной работы реализовано:

- ✅ Микросервисная архитектура (3 сервиса + база)
- ✅ Взаимодействие по HTTP (синхронное)
- ✅ Docker Compose + PostgreSQL
- ✅ Swagger-документация
- ✅ Покрытие логики модульными тестами
- ✅ README с полной инструкцией

---

