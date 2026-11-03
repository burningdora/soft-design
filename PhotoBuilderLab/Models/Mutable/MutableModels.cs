// MUTABLE МОДЕЛИ: хранят настройки, пока идёт конфигурация.
// Намеренно НЕ инкапсулированы (публичные поля): любой внешний код может их настроить.
// Вся логика и правила вынесены в другие классы (расширения билдера).
// Специальные значения (NotSet, null) означают "поле не задано".
public class PhotoModel
{
    public const int NotSet = -1;

    public string? Name;
    public int Rating = NotSet;
    public int Width = NotSet;
    public int Height = NotSet;
    public OptionalAlbumId InAlbum;      // default = без альбома
    public string CreatedAt = "";        // "Файл:строка", где создали объект (для ошибок)
}

public class AlbumModel
{
    public string? Title;
    public string CreatedAt = "";
}

public class GalleryModel
{
    public List<PhotoModel> Photos = new();
    public List<AlbumModel> Albums = new();
}
