using System.Runtime.CompilerServices;

namespace DynamicContext;

public sealed record KeyInfo(int Id, string Name, Type ValueType, string RegisteredAt);

public class KeyAlreadyRegisteredException : Exception
{
    public KeyAlreadyRegisteredException(string name, KeyInfo existing, Type attempted)
        : base($"Ключ '{name}' уже зарегистрирован ({existing.RegisteredAt}, тип {existing.ValueType.Name}). " +
               $"Попытка повторной регистрации с типом {attempted.Name}.")
    {
    }
}

// ЦЕНТРАЛИЗОВАННЫЙ РЕЕСТР КЛЮЧЕЙ (паттерн Registry + Singleton).
// Все проекты, использующие библиотеку, получают ключи только отсюда.
// Если имя уже занято (любым типом, любым проектом), будет исключение,
// поэтому два проекта не смогут записать несовместимые данные под одним ключом.
public sealed class KeyRegistry
{
    public static KeyRegistry Global { get; } = new();
    private KeyRegistry() { }

    private readonly object _lock = new();                       // реестр общий, защищаем от гонок потоков
    private readonly List<KeyInfo> _infos = new();               // индекс = Id - 1
    private readonly Dictionary<string, int> _byName = new();

    // Id начинаются с 1, чтобы default(Key<T>) (Id = 0) нельзя было спутать с настоящим ключом.
    public Key<T> Register<T>(
        string name,
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Имя ключа не может быть пустым", nameof(name));

        lock (_lock)
        {
            if (_byName.TryGetValue(name, out var existingId))
                throw new KeyAlreadyRegisteredException(name, _infos[existingId - 1], typeof(T));

            int id = _infos.Count + 1;
            _infos.Add(new KeyInfo(id, name, typeof(T), $"{Path.GetFileName(file)}:{line}"));
            _byName.Add(name, id);
            return new Key<T>(id);
        }
    }

    // Метаданные ключа по числу (для интроспекции и отладки)
    public KeyInfo Info(int id)
    {
        lock (_lock)
        {
            if (id < 1 || id > _infos.Count)
                throw new ArgumentOutOfRangeException(nameof(id), $"Ключа с Id {id} нет в реестре");
            return _infos[id - 1];
        }
    }

    public IReadOnlyList<KeyInfo> All()
    {
        lock (_lock) return _infos.ToArray();
    }
}
