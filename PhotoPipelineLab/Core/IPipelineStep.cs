// ПОЛИМОРФИЗМ: все шаги вызываются одинаково, реализации разные
// ИНТРОСПЕКЦИЯ: Introspect() позволяет шагу рассказать о себе
interface IPipelineStep
{
    void Execute(PhotoContext context);
    string Introspect();
}
