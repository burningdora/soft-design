using System.Collections.Immutable;

// IMMUTABLE МОДЕЛИ: итог работы билдера.
// Все свойства init-only (задаются только при создании), массивы ImmutableArray.
// Плюс: никто не изменит данные, от которых зависит код.
// Минус: любое изменение = создание нового объекта (лишняя память).
public sealed class Album
{
    public required string Title { get; init; }
}

public sealed class Photo
{
    public required string Name { get; init; }
    public required int Rating { get; init; }          // 0..5
    public required int Width { get; init; }
    public required int Height { get; init; }
    public OptionalAlbumId InAlbum { get; init; }      // по умолчанию: без альбома
}

// Плоская структура: никаких ссылок между объектами, только идентификаторы ("как в базе данных").
public sealed class Gallery
{
    public required ImmutableArray<Photo> Photos { get; init; }
    public required ImmutableArray<Album> Albums { get; init; }

    // Инкапсулировано, что в Id лежит индекс в массиве.
    // Перегрузки по типу Id: тип объекта указывать не нужно.
    public Photo Get(PhotoId id) => Photos[id.Value];
    public Album Get(AlbumId id) => Albums[id.Value];
    public Album? Get(OptionalAlbumId id) => id.HasValue ? Get(id.Id) : null;

    public string Describe(Photo p) =>
        $"{p.Name} {p.Width}x{p.Height}, рейтинг {p.Rating}, альбом: {Get(p.InAlbum)?.Title ?? "нет"}";
}
