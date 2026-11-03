using System.Diagnostics.CodeAnalysis;

namespace DynamicContext;

// СУЩНОСТЬ с ДИНАМИЧЕСКИМИ СВОЙСТВАМИ (аналог HttpContext.Items).
// Библиотека не знает, какие свойства будут нужны потребителям:
// данные лежат в словаре "Id ключа -> значение", а доступ идёт через типизированные ключи.
public sealed class Entity
{
    private readonly Dictionary<int, object?> _data = new();

    public int Id { get; }

    internal Entity(int id) => Id = id;          // создаёт только World

    // Add: как Dictionary.Add, повторное добавление = ошибка
    public void Add<T>(Key<T> key, T value)
    {
        Check(key);
        if (!_data.TryAdd(key.Id, value))
            throw new InvalidOperationException($"У сущности #{Id} свойство '{key.Name}' уже задано");
    }

    // Set: задать или перезаписать
    public void Set<T>(Key<T> key, T value)
    {
        Check(key);
        _data[key.Id] = value;
    }

    // Get: тип результата известен из типа ключа, явный каст не нужен
    public T Get<T>(Key<T> key)
    {
        Check(key);
        if (!_data.TryGetValue(key.Id, out var value))
            throw new KeyNotFoundException($"У сущности #{Id} нет свойства '{key.Name}'");
        return (T)value!;
    }

    public bool TryGet<T>(Key<T> key, [MaybeNullWhen(false)] out T value)
    {
        Check(key);
        if (_data.TryGetValue(key.Id, out var raw))
        {
            value = (T)raw!;
            return true;
        }
        value = default;
        return false;
    }

    public bool Has(IKey key) => _data.ContainsKey(key.Id);
    public bool Remove(IKey key) => _data.Remove(key.Id);

    // ИНТРОСПЕКЦИЯ: имена ключей берём из реестра
    public string Dump() => string.Join(", ",
        _data.OrderBy(kv => kv.Key)
             .Select(kv => $"{KeyRegistry.Global.Info(kv.Key).Name}={Format(kv.Value)}"));

    private static string Format(object? v) => v switch
    {
        null => "null",
        string s => s,
        System.Collections.IEnumerable e => "[" + string.Join(", ", e.Cast<object>()) + "]",
        _ => v.ToString() ?? "",
    };

    private static void Check(IKey key)
    {
        if (key.Id == 0)
            throw new ArgumentException("Ключ не зарегистрирован (default). Получите его через KeyRegistry.");
    }
}
