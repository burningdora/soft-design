# Software Design — лабораторные работы

Репозиторий с лабораторными по предмету **Software Design**.

## Лабораторные

| Папка | Тема | Стек |
|---|---|---|
| [FieldMaskLab](FieldMaskLab/) | Field Mask: маски полей, битовые маски, `Intersect`/`Union`/`Invert`, слияние и копирование полей | C# / .NET 10, xUnit |
| [PhotoPipelineLab](PhotoPipelineLab/) | Pipeline обработки фото: абстракция, полиморфизм, Strategy, Decorator, Singleton, интроспекция | C# / .NET 10 |
| [PhotoBuilderLab](PhotoBuilderLab/) | Builder: fluent-билдер галереи фото, scope, immutable/mutable модели, валидация | C# / .NET 10 |
| [DynamicContextLab](DynamicContextLab/) | Контекст с динамическими свойствами: `World`/`Entity`, ключи и реестр ключей, модули Exif и PhotoApp | C# / .NET, solution из 3 проектов |

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

PhotoBuilderLab:

```bash
cd PhotoBuilderLab
dotnet run
```

DynamicContextLab:

```bash
cd DynamicContextLab
dotnet run --project PhotoApp
```

Подробности по каждому заданию — в `README.md` внутри соответствующей папки.
