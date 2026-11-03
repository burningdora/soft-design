namespace DynamicContext;

// ТИПИЗИРОВАННЫЙ КЛЮЧ: внутри int, а тип значения T зашит в тип ключа.
// Благодаря этому Get/Set знают тип значения на этапе компиляции, касты не нужны,
// а записать в ключ значение не того типа нельзя (ошибка компиляции).
//
// Конструктор internal: создать ключ можно ТОЛЬКО через KeyRegistry,
// из другого проекта подделать ключ нельзя.
public readonly struct Key<T> : IKey, IEquatable<Key<T>>
{
    public int Id { get; }                       // 0 = default(Key<T>), невалидный ключ

    internal Key(int id) => Id = id;

    public string Name => Id == 0 ? "<default>" : KeyRegistry.Global.Info(Id).Name;
    public Type ValueType => typeof(T);

    public bool Equals(Key<T> other) => Id == other.Id;
    public override bool Equals(object? obj) => obj is Key<T> k && Equals(k);
    public override int GetHashCode() => Id;
    public override string ToString() => Name;
}
