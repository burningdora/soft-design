using DynamicContext;

// Операция в виде класса (вторая форма записи системы): печатает все свойства сущности
public sealed class PrintSystem : ISystem
{
    public string Name => "Печать";
    public IReadOnlyList<IKey> Requires { get; } = new IKey[] { PhotoKeys.Name };

    public void Update(Entity entity) => Console.WriteLine($"  #{entity.Id} {entity.Dump()}");
}
