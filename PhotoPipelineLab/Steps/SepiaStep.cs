// SINGLETON: шаг без параметров, достаточно одного объекта на всю программу.
// Вместо new SepiaStep() используем SepiaStep.Instance
class SepiaStep : IPipelineStep
{
    public static readonly SepiaStep Instance = new();
    SepiaStep() { }

    public void Execute(PhotoContext c) => PhotoOps.SetFilter(c.Photo, "sepia");
    public string Introspect() => "Sepia";
}
