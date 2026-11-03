// АДАПТЕР: берёт нужное из контекста и вызывает чистую логику PhotoOps
class BrightnessStep : IPipelineStep
{
    readonly int _d;
    public BrightnessStep(int d) => _d = d;

    public void Execute(PhotoContext c) => PhotoOps.Brighten(c.Photo, _d);
    public string Introspect() => $"Brightness({_d})";
}
