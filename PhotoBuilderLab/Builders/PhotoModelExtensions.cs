// ИНКАПСУЛЯЦИЯ ПРАВИЛ в одном месте: extension-методы над интерфейсом IHasPhotoModel.
// Каждый метод:
//  - проверяет входное значение (контракт: базовая валидация на уровне метода),
//  - записывает его в mutable модель,
//  - возвращает тот же билдер (FLUENT), причём конкретного типа T.
public static class PhotoModelExtensions
{
    public static T SetName<T>(this T b, string name) where T : IHasPhotoModel
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Имя фото не может быть пустым", nameof(name));
        b.Model.Name = name;
        return b;
    }

    public static T SetRating<T>(this T b, int rating) where T : IHasPhotoModel
    {
        if (rating is < 0 or > 5)
            throw new ArgumentOutOfRangeException(nameof(rating), "Рейтинг должен быть от 0 до 5");
        b.Model.Rating = rating;
        return b;
    }

    // ВЫСОКОУРОВНЕВЫЙ метод: два связанных поля задаются разом, общее правило
    // (оба больше нуля) инкапсулировано здесь.
    public static T SetSize<T>(this T b, int width, int height) where T : IHasPhotoModel
    {
        if (width <= 0 || height <= 0)
            throw new ArgumentOutOfRangeException(nameof(width), "Ширина и высота должны быть больше 0");
        b.Model.Width = width;
        b.Model.Height = height;
        return b;
    }

    // Альбом задаётся через его идентификатор, а не через ссылку на объект
    public static T SetAlbum<T>(this T b, AlbumBuilder album) where T : IHasPhotoModel
    {
        b.Model.InAlbum = new OptionalAlbumId(album.Id);
        return b;
    }

    // ВЫСОКОУРОВНЕВЫЙ метод: скопировать настройки из другого билдера/scope.
    // Копируются только заданные поля (размер копируется парой).
    public static T CopyFrom<T>(this T target, IHasPhotoModel source) where T : IHasPhotoModel
    {
        var s = source.Model;
        var t = target.Model;

        if (s.Name != null) t.Name = s.Name;
        if (s.Rating != PhotoModel.NotSet) t.Rating = s.Rating;
        if (s.Width != PhotoModel.NotSet)
        {
            t.Width = s.Width;
            t.Height = s.Height;
        }
        if (s.InAlbum.HasValue) t.InAlbum = s.InAlbum;
        return target;
    }
}
