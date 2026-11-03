// КОНТЕКСТ: общие данные для всех шагов в одном объекте
class PhotoContext
{
    public Photo Photo { get; } = new();
    public bool IsDone { get; set; }
}
