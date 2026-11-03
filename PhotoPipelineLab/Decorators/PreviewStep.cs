// ДЕКОРАТОР 2 (тематический): показывает состояние фото до и после шага
class PreviewStep : StepDecorator
{
    public PreviewStep(IPipelineStep inner) : base(inner) { }

    public override void Execute(PhotoContext c)
    {
        Console.WriteLine($"  [до]    {c.Photo}");       // до
        Inner.Execute(c);
        Console.WriteLine($"  [после] {c.Photo}");       // после
    }
    public override string Introspect() => $"Preview[{Inner.Introspect()}]";
}
