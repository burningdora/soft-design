// ИНТРОСПЕКЦИЯ: "снимает" декораторы, чтобы проверять тип настоящего шага
static class StepExtensions
{
    public static IPipelineStep Unwrap(this IPipelineStep s)
    {
        while (s is StepDecorator d) s = d.Inner;
        return s;
    }
}
