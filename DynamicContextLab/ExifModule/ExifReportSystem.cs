using DynamicContext;

namespace ExifModule;

// Операция из второго проекта: работает только с сущностями, у которых есть EXIF-свойства
public sealed class ExifReportSystem : ISystem
{
    public string Name => "EXIF-отчёт";
    public IReadOnlyList<IKey> Requires { get; } = new IKey[] { ExifKeys.Camera, ExifKeys.Iso };

    public void Update(Entity entity) =>
        Console.WriteLine($"  [EXIF] сущность #{entity.Id}: {entity.Get(ExifKeys.Camera)}, ISO {entity.Get(ExifKeys.Iso)}");
}
