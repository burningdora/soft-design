// Билдер одного фото. Хранит mutable модель и идентификатор будущего объекта.
// Методы настройки (SetName, SetRating, ...) берутся из PhotoModelExtensions.
public class PhotoBuilder : IHasPhotoModel
{
    public PhotoModel Model { get; }
    public PhotoId Id { get; }

    public PhotoBuilder(PhotoModel model, PhotoId id)
    {
        Model = model;
        Id = id;
    }
}
