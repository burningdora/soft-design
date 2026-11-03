namespace DynamicContext;

// ОПЕРАЦИЯ над сущностями (паттерн Strategy): пишется потребителем библиотеки.
// Requires: какие свойства должны быть у сущности, чтобы система к ней применилась.
public interface ISystem
{
    string Name { get; }
    IReadOnlyList<IKey> Requires { get; }
    void Update(Entity entity);
}

// Мир хранит сущности и системы. Он не знает ни про фото, ни про EXIF:
// просто для каждой системы вызывает Update у тех сущностей, где есть все нужные свойства.
public sealed class World
{
    private readonly List<Entity> _entities = new();
    private readonly List<ISystem> _systems = new();

    public IReadOnlyList<Entity> Entities => _entities;

    public Entity CreateEntity()
    {
        var entity = new Entity(_entities.Count);
        _entities.Add(entity);
        return entity;
    }

    public void AddSystem(ISystem system) => _systems.Add(system);

    // Короткая форма: операция передаётся лямбдой
    public void AddSystem(string name, Action<Entity> update, params IKey[] requires) =>
        _systems.Add(new DelegateSystem(name, update, requires));

    public void Update()
    {
        foreach (var system in _systems)
            foreach (var entity in _entities)
                if (system.Requires.All(entity.Has))
                    system.Update(entity);
    }

    private sealed class DelegateSystem : ISystem
    {
        private readonly Action<Entity> _update;
        public string Name { get; }
        public IReadOnlyList<IKey> Requires { get; }

        public DelegateSystem(string name, Action<Entity> update, IKey[] requires)
        {
            Name = name;
            _update = update;
            Requires = requires;
        }

        public void Update(Entity entity) => _update(entity);
    }
}
