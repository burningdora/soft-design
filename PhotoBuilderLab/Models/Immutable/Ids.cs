// ИДЕНТИФИКАТОРЫ ОБЪЕКТОВ: обёртки над индексом в массиве.
// Type safety: AlbumId нельзя случайно передать туда, где нужен PhotoId.
public readonly record struct PhotoId(int Value);
public readonly record struct AlbumId(int Value);

// "Необязательный" AlbumId: у фото может не быть альбома.
// Хранится Value+1, поэтому default (нулевая структура) = "альбома нет".
public readonly record struct OptionalAlbumId
{
    private readonly int _valuePlusOne;

    public OptionalAlbumId(AlbumId id) => _valuePlusOne = id.Value + 1;

    public static OptionalAlbumId None => default;
    public bool HasValue => _valuePlusOne != 0;

    public AlbumId Id => HasValue
        ? new AlbumId(_valuePlusOne - 1)
        : throw new InvalidOperationException("Альбом не задан");
}
