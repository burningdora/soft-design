// ДЕКОРАТОР 1: код до и после шага
class LoggingStep : StepDecorator
{
    public LoggingStep(IPipelineStep inner) : base(inner) { }

    public override void Execute(PhotoContext c)
    {
        Console.WriteLine($"  >> {Inner.Introspect()}");   // до
        Inner.Execute(c);
        Console.WriteLine($"  << {Inner.Introspect()}");   // после
    }
    public override string Introspect() => $"Logging[{Inner.Introspect()}]";
}
