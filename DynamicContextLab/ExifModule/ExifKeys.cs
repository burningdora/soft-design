using DynamicContext;

namespace ExifModule;

// Ключи ВТОРОГО проекта-потребителя. Они регистрируются в том же глобальном реестре.
public static class ExifKeys
{
    public static readonly Key<string> Camera = KeyRegistry.Global.Register<string>("exif.camera");
    public static readonly Key<int> Iso = KeyRegistry.Global.Register<int>("exif.iso");

    // Явный статический конструктор: ключи регистрируются ровно при первом обращении к классу
    static ExifKeys() { }
}
