// ===== ДЕМОНСТРАЦИЯ (основная функция) =====
Console.OutputEncoding = System.Text.Encoding.UTF8;

var filters = new Pipeline("Фильтры")                     // pipeline внутри pipeline
    .Add(SepiaStep.Instance)                              // Singleton
    .Add(new LoggingStep(new BlurStep(2)));               // Декоратор (до и после)

var pipeline = new Pipeline("Instagram")
    .Add(new BrightnessStep(20))
    .Add(filters)
    .Add(new PreviewStep(new BrightnessStep(5)))          // Декоратор (до и после)
    .Add(PrintStep.Instance);

Console.WriteLine(pipeline.Introspect());                 // Интроспекция
pipeline.Execute(new PhotoContext());

Console.WriteLine("\n-- убираем все BrightnessStep (даже обёрнутые) --");
pipeline.Steps.RemoveAll(s => s.Unwrap() is BrightnessStep);   // Data-Oriented + интроспекция
Console.WriteLine(pipeline.Introspect());
pipeline.Execute(new PhotoContext());

// Singleton: SepiaStep в двух разных pipeline - это один и тот же объект
var second = new Pipeline("Второй").Add(SepiaStep.Instance);
Console.WriteLine("\nSingleton (один объект в двух pipeline): " +
                  ReferenceEquals(filters.Steps[0], second.Steps[0]));
