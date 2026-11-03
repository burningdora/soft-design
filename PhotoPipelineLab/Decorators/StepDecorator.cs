// ДЕКОРАТОР (Wrapper): базовый класс. Хранит другой шаг и сам является шагом,
// поэтому декораторы можно вкладывать друг в друга.
abstract class StepDecorator : IPipelineStep
{
    public IPipelineStep Inner { get; }
    protected StepDecorator(IPipelineStep inner) => Inner = inner;

    public abstract void Execute(PhotoContext c);
    public abstract string Introspect();
}
