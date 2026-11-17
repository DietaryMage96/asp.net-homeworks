# Movie API — Контракти та валідація (ДЗ 4)

## Запуск проєкту
1. Відкрити рішення у папці `dz4_ASP` та перевірити рядок підключення `"def"` у `less10/appsettings.json`.
2. Виконати міграцію в Package Manager Console: `Update-Database`.
3. Запустити проєкт (`F5`) та відкрити `/swagger`.

---

## Перевірка через Swagger

### 1. Успішне створення фільму (`200 OK`)
* **Запит:** `POST /api/Movie` з валідними даними (`Interstellar`, `2014`, `8.7`).
* **Очікуваний та фактичний результат:** `200 OK`, сервер зберігає запис у БД і повертає `MovieResponse` зі згенерованим `Id`.
![Test 1](images/Test%201.jpg)

### 2. Помилка 1 — Порожня назва (`400 Bad Request`)
* **Запит:** `POST /api/Movie` зі значенням `"title": ""`.
* **Очікуваний та фактичний результат:** `400 Bad Request` (`Title is required`), у БД не збережено.
![Test 2](images/Test%202.png)

### 3. Помилка 2 — Надто довгий текст (`400 Bad Request`)
* **Запит:** `POST /api/Movie` з `title` на 103 символи та `description` на 503 символи.
* **Очікуваний та фактичний результат:** `400 Bad Request` (`Max length is 100`, `Max length is 500`), у БД не збережено.
![Test 3](images/Test%203.png)

### 4. Помилка 3 — Недопустимий рейтинг (`400 Bad Request`)
* **Запит:** `POST /api/Movie` зі значенням `"rating": 15`.
* **Очікуваний та фактичний результат:** `400 Bad Request` (`Must be between 0 and 10`), у БД не збережено.
![Test 4](images/Test%204.png)

### 5. Помилка 4 — Звернення до відсутнього фільму (`404 Not Found`)
* **Запит:** `GET /api/Movie/999`.
* **Очікуваний та фактичний результат:** `404 Not Found` (`Movie not found`).
![Test 6](images/Test%206.png)

### 6. Помилка 5 — Недопустимий рік випуску (`400 Bad Request`)
* **Запит:** `PUT /api/Movie/1` зі значенням `"releaseYear": 1800`.
* **Очікуваний та фактичний результат:** `400 Bad Request` (`Must be between 1888 and 2030`), змін у БД немає.
![Test 5](images/Test%205.jpg)

---


### Data Annotations vs FluentValidation
* **Data Annotations** — атрибути прямо у класі DTO (`[Required]`, `[StringLength]`, `[Range]`). Швидко для простих правил, але змішує модель і валідацію.
* **FluentValidation** — окремі класи-валідатори (`AbstractValidator<T>`). Не засмічує контракти, підтримує складні умови (`Must`, `When`) та легко тестується окремо.

**Одне правило в обох підходах:**
```csharp
// Data Annotations
[Required(ErrorMessage = "Title is required")]
[StringLength(100, ErrorMessage = "Max length is 100")]
public string Title { get; set; } = string.Empty;

// FluentValidation
RuleFor(x => x.Title)
    .NotEmpty().WithMessage("Title is required")
    .MaximumLength(100).WithMessage("Title cannot exceed 100 characters");
```
