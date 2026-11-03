// Билдер одного альбома (у альбома одно свойство, интерфейс не нужен)
public class AlbumBuilder
{
    public AlbumModel Model { get; }
    public AlbumId Id { get; }

    public AlbumBuilder(AlbumModel model, AlbumId id)
    {
        Model = model;
        Id = id;
    }

    public AlbumBuilder SetTitle(string title)      // fluent: возвращает this
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Название альбома не может быть пустым", nameof(title));
        Model.Title = title;
        return this;
    }
}
