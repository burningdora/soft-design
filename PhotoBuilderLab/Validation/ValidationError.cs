// УЛУЧШЕННАЯ ВАЛИДАЦИЯ: ошибка знает, в какой строке кода создан объект.
public record ValidationError(string Location, string Message)
{
    public override string ToString() => $"{Location}: {Message}";
}

// Содержит ВСЕ найденные ошибки, а не только первую.
public class GalleryValidationException : Exception
{
    public IReadOnlyList<ValidationError> Errors { get; }

    public GalleryValidationException(IReadOnlyList<ValidationError> errors)
        : base($"Найдено ошибок: {errors.Count}\n" + string.Join("\n", errors))
    {
        Errors = errors;
    }
}
