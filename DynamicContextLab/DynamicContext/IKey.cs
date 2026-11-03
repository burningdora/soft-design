namespace DynamicContext;

// Нетипизированный вид ключа: нужен там, где тип значения не важен
// (например, "есть ли у сущности такое свойство?").
public interface IKey
{
    int Id { get; }
    string Name { get; }
    Type ValueType { get; }
}
