// SINGLETON
class PrintStep : IPipelineStep
{
    public static readonly PrintStep Instance = new();
    PrintStep() { }

    public void Execute(PhotoContext c) => Console.WriteLine("  Фото: " + c.Photo);
    public string Introspect() => "Print";
}
