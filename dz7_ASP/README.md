# React & ASP.NET Core Web API (Movies)


## Інструкція запуску

### 1. Backend (ASP.NET Core Web API)
1. Відкрити рішення `less10.slnx` у Visual Studio (рядок підключення `"def"` до MS SQL Server LocalDB знаходиться в `less10/appsettings.json`).
2. Застосувати міграції БД у **Package Manager Console** (Default project: `Dll`):
   ```powershell
   Update-Database
   ```
3. Запустити проєкт `less10` (`F5`) — API запуститься на `https://localhost:7184`.

### 2. Frontend (React)
1. Відкрити термінал у папці фронтенду та встановити залежності:
   ```bash
   npm install
   ```
2. Запустити клієнтський застосунок:
   ```bash
   npm start
   ```
3. Перейти за посиланням з терміналу (зазвичай `http://localhost:5173`).


## Перевірка роботи

Відеодемонстрація всіх пунктів перевірки (реєстрація користувача, запис у таблиці `AspNetUsers`, помилка при неправильному паролі, вхід, додавання фільму, збереження після оновлення сторінки та видалення): **[`video/tests.mp4`](./video/tests.mp4)**