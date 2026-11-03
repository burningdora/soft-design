# Лабораторная 3: Builder (галерея фото, C#)

Запуск: `dotnet run` в папке, где лежит `PhotoBuilderLab.csproj`.

## Структура
- `Models/Immutable` - итоговые модели `Gallery`, `Photo`, `Album` и идентификаторы
- `Models/Mutable` - модели для конфигурации внутри билдера
- `Builders` - `GalleryBuilder`, `PhotoBuilder`, `AlbumBuilder`, `GalleryScope`, интерфейс и расширения
- `Validation` - ошибки валидации
- `Program.cs` - демонстрация

## Дополнительные задания (выбрано 3 + бонус)
| Задание | Где |
|---|---|
| Fluent builder, делегаты конфигурации, паттерн scope | `PhotoModelExtensions` (возврат `T`), `GalleryBuilder.AddPhoto(Action)`, `Scope(Action)`, `GalleryScope` |
| Улучшенная валидация: строка кода + все ошибки | `Models/Mutable` (`CreatedAt`), `GalleryBuilder.Validate()`, `GalleryValidationException` |
| Структура как в БД: ссылки через идентификаторы | `Models/Immutable/Ids.cs`, `Gallery.Get(...)` |
| (бонус) Высокоуровневые функции конфигурации | `SetSize` (два поля с общим правилом), `CopyFrom` |

## Концепты -> где в коде
| Концепт | Где |
|---|---|
| Инициализация в один этап | `Program.cs`, раздел 1 |
| Инициализация поэтапно | `Program.cs`, раздел 2 |
| Immutable модели, плюсы и минусы | `Models/Immutable` |
| Mutable модели в билдере | `Models/Mutable` |
| Реализация по интерфейсу | `IHasPhotoModel` реализуют `PhotoBuilder` и `GalleryScope` |
| Нет инкапсуляции в mutable моделях, она в другом классе | публичные поля `PhotoModel`, правила в `PhotoModelExtensions` |
| Инкапсуляция настройки методами | `SetName`, `SetRating`, `SetSize`, `SetAlbum` |
| Scope | `GalleryScope` |
| Валидация | `Validate()`, проверки в методах `Set...` |
| Идентификаторы | `PhotoId`, `AlbumId`, `OptionalAlbumId` |
| Интерфейсы против повторяющегося кода | `IHasPhotoModel` + extension-методы |
| Fluent | методы возвращают `this` / `T` |
| Делегат конфигурации | `AddPhoto(Action<PhotoBuilder>)`, `Scope(Action<GalleryScope>)` |
