# Software Design — лабораторные работы

Репозиторий с лабораторными по предмету **Software Design**.

## Лабораторные

| Папка | Тема | Стек |
|---|---|---|
| [FieldMaskLab](FieldMaskLab/) | Field Mask: маски полей, битовые маски, `Intersect`/`Union`/`Invert`, слияние и копирование полей | C# / .NET 10, xUnit |
| [PhotoPipelineLab](PhotoPipelineLab/) | Pipeline обработки фото: абстракция, полиморфизм, Strategy, Decorator, Singleton, интроспекция | C# / .NET 10 |

## Запуск

FieldMaskLab:

```bash
cd FieldMaskLab
dotnet run --project FieldMaskLab
dotnet test
```

PhotoPipelineLab:

```bash
cd PhotoPipelineLab
dotnet run
```

Подробности по каждому заданию — в `README.md` внутри соответствующей папки.
