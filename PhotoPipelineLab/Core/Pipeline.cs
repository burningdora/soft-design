// DATA-ORIENTED + STRATEGY: шаги хранятся как данные (List) и передаются объектами.
// Pipeline сам является шагом, поэтому его можно вложить в другой Pipeline.
class Pipeline : IPipelineStep
{
    readonly string _name;
    public List<IPipelineStep> Steps { get; } = new();

    public Pipeline(string name) => _name = name;

    public Pipeline Add(IPipelineStep step)
    {
        Steps.Add(step);
        return this;
    }

    public void Execute(PhotoContext c) => Steps.ForEach(s => s.Execute(c));

    public string Introspect() =>
        $"{_name}: " + string.Join(" -> ", Steps.Select(s => s.Introspect()));
}
