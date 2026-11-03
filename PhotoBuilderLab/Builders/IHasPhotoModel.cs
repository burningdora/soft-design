// ИНТЕРФЕЙС: всё, что умеет отдавать настраиваемую модель фото.
// Его реализуют и PhotoBuilder, и GalleryScope, поэтому методы настройки
// пишутся ОДИН раз (в PhotoModelExtensions) и не дублируются в двух классах.
public interface IHasPhotoModel
{
    PhotoModel Model { get; }
}
