# Лабораторная: Pipeline обработки фото (C#)

Запуск: `dotnet run` в папке проекта.

## Где какой концепт

| Концепт | Файл |
|---|---|
| Абстракция | Core/PhotoOps.cs |
| Полиморфизм | Core/IPipelineStep.cs |
| Контекст | Core/PhotoContext.cs |
| Адаптер | Steps/BrightnessStep.cs, Steps/BlurStep.cs |
| Strategy | Core/Pipeline.cs (Add), Decorators/* (шаг передаётся параметром) |
| Интроспекция | Introspect() во всех шагах, Decorators/StepExtensions.cs, Program.cs (RemoveAll) |
| Data-Oriented | Core/Pipeline.cs (List Steps), Program.cs |
| Декоратор | Decorators/LoggingStep.cs, Decorators/PreviewStep.cs |
| Singleton | Steps/SepiaStep.cs, Steps/PrintStep.cs |
