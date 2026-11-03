using System.Collections.Immutable;
using System.Runtime.CompilerServices;

// Главный билдер. Хранит mutable модель всей галереи,
// выдаёт билдеры для фото и альбомов, а в Build() превращает всё в immutable Gallery.
public class GalleryBuilder
{
    public GalleryModel Model { get; } = new();

    // ---- создание билдеров. CallerFilePath/CallerLineNumber компилятор подставляет сам:
    // так мы запоминаем строку кода, где объект был создан.
    public PhotoBuilder AddPhoto(
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
    {
        var model = new PhotoModel { CreatedAt = Where(file, line) };
        Model.Photos.Add(model);
        return new PhotoBuilder(model, new PhotoId(Model.Photos.Count - 1));
    }

    public AlbumBuilder AddAlbum(
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
    {
        var model = new AlbumModel { CreatedAt = Where(file, line) };
        Model.Albums.Add(model);
        return new AlbumBuilder(model, new AlbumId(Model.Albums.Count - 1));
    }

    // ---- ДЕЛЕГАТ КОНФИГУРАЦИИ: настройка внутри лямбды, переменная билдера локальна
    public GalleryBuilder AddPhoto(
        Action<PhotoBuilder> configure,
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
    {
        configure(AddPhoto(file, line));
        return this;
    }

    public GalleryScope Scope() => new GalleryScope(this);

    public GalleryBuilder Scope(Action<GalleryScope> configure)
    {
        configure(Scope());
        return this;
    }

    private static string Where(string file, int line) => $"{Path.GetFileName(file)}:{line}";

    // ---- ВАЛИДАЦИЯ: собирает ВСЕ ошибки по всем объектам
    public List<ValidationError> Validate()
    {
        var errors = new List<ValidationError>();

        for (int i = 0; i < Model.Albums.Count; i++)
        {
            var a = Model.Albums[i];
            if (string.IsNullOrWhiteSpace(a.Title))
                errors.Add(new ValidationError(a.CreatedAt, $"у альбома #{i} не задано название"));
        }

        for (int i = 0; i < Model.Photos.Count; i++)
        {
            var p = Model.Photos[i];
            if (p.Name is null)
                errors.Add(new ValidationError(p.CreatedAt, $"у фото #{i} не задано имя"));
            if (p.Rating == PhotoModel.NotSet)
                errors.Add(new ValidationError(p.CreatedAt, $"у фото #{i} ('{p.Name}') не задан рейтинг"));
            if (p.Width == PhotoModel.NotSet || p.Height == PhotoModel.NotSet)
                errors.Add(new ValidationError(p.CreatedAt, $"у фото #{i} ('{p.Name}') не задан размер"));
            if (p.InAlbum.HasValue && p.InAlbum.Id.Value >= Model.Albums.Count)
                errors.Add(new ValidationError(p.CreatedAt, $"у фото #{i} ('{p.Name}') указан несуществующий альбом"));
        }

        return errors;
    }

    // ---- BUILD: mutable модель -> immutable. Из-за идентификаторов словари не нужны:
    // индекс в mutable модели и в готовой Gallery совпадает.
    public Gallery Build()
    {
        var errors = Validate();
        if (errors.Count > 0)
            throw new GalleryValidationException(errors);

        var albums = Model.Albums
            .Select(a => new Album { Title = a.Title! })
            .ToImmutableArray();

        var photos = Model.Photos
            .Select(p => new Photo
            {
                Name = p.Name!,
                Rating = p.Rating,
                Width = p.Width,
                Height = p.Height,
                InAlbum = p.InAlbum,
            })
            .ToImmutableArray();

        return new Gallery { Photos = photos, Albums = albums };
    }
}
