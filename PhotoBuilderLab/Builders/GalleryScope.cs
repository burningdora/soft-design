using System.Runtime.CompilerServices;

// ПАТТЕРН SCOPE: общие настройки для группы фото.
// Модель scope хранит настройки по умолчанию; каждое фото, созданное через scope,
// сначала получает копию этих настроек, потом их можно переопределить.
public class GalleryScope : IHasPhotoModel
{
    private readonly GalleryBuilder _gallery;

    public PhotoModel Model { get; } = new();    // общие настройки

    public GalleryScope(GalleryBuilder gallery) => _gallery = gallery;

    public PhotoBuilder AddPhoto(
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
    {
        var builder = _gallery.AddPhoto(file, line);   // место создания пробрасываем дальше
        builder.CopyFrom(this);                         // применяем общие настройки
        return builder;
    }
}
