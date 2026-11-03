// АДАПТЕР
class BlurStep : IPipelineStep
{
    readonly int _r;
    public BlurStep(int r) => _r = r;

    public void Execute(PhotoContext c) => PhotoOps.Blur(c.Photo, _r);
    public string Introspect() => $"Blur({_r})";
}
