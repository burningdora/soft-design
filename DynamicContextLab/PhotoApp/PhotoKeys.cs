using DynamicContext;

// Ключи проекта PhotoApp. Библиотека DynamicContext про фото ничего не знает.
public static class PhotoKeys
{
    public static readonly Key<string> Name = KeyRegistry.Global.Register<string>("photo.name");
    public static readonly Key<int> Brightness = KeyRegistry.Global.Register<int>("photo.brightness");
    public static readonly Key<string> Filter = KeyRegistry.Global.Register<string>("photo.filter");
    public static readonly Key<int> Likes = KeyRegistry.Global.Register<int>("photo.likes");
    public static readonly Key<string[]> Tags = KeyRegistry.Global.Register<string[]>("photo.tags");

    static PhotoKeys() { }
}
