using DynamicContext;
using ExifModule;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var world = new World();

// ----- Сущности: у каждой свой набор свойств -----
var beach = world.CreateEntity();
beach.Add(PhotoKeys.Name, "Пляж");
beach.Add(PhotoKeys.Brightness, 50);
beach.Add(PhotoKeys.Filter, "none");
beach.Add(PhotoKeys.Likes, 3);
beach.Add(ExifKeys.Camera, "Canon R6");      // свойства из ДРУГОГО проекта
beach.Add(ExifKeys.Iso, 200);

var sunset = world.CreateEntity();
sunset.Add(PhotoKeys.Name, "Закат");
sunset.Add(PhotoKeys.Brightness, 30);
sunset.Add(PhotoKeys.Filter, "none");

var album = world.CreateEntity();             // не фото: нет яркости, фильтра и лайков
album.Add(PhotoKeys.Name, "Лето");
album.Add(PhotoKeys.Tags, new[] { "море", "отпуск" });

// ----- Операции, заданные потребителем -----
world.AddSystem("Яркость +10", e => e.Set(PhotoKeys.Brightness, e.Get(PhotoKeys.Brightness) + 10), PhotoKeys.Brightness);
world.AddSystem("Лайк", e => e.Set(PhotoKeys.Likes, e.Get(PhotoKeys.Likes) + 1), PhotoKeys.Likes);
world.AddSystem("Сепия", e => e.Set(PhotoKeys.Filter, "sepia"), PhotoKeys.Filter);
world.AddSystem(new ExifReportSystem());      // система из другого проекта
world.AddSystem(new PrintSystem());

Console.WriteLine("=== 1. Запуск систем ===");
world.Update();

Console.WriteLine("\n=== 2. Реестр ключей (метаданные) ===");
foreach (var info in KeyRegistry.Global.All())
    Console.WriteLine($"  {info.Id}: {info.Name} ({info.ValueType.Name})");

Console.WriteLine("\n=== 3. Конфликт ключей между проектами ===");
try
{
    KeyRegistry.Global.Register<int>("exif.camera");      // имя занято проектом ExifModule
}
catch (KeyAlreadyRegisteredException e)
{
    Console.WriteLine("  " + e.Message);
}

Console.WriteLine("\n=== 4. У сущности нет свойства ===");
try
{
    album.Get(PhotoKeys.Brightness);
}
catch (KeyNotFoundException e)
{
    Console.WriteLine("  " + e.Message);
}

// Ошибки КОМПИЛЯЦИИ (раскомментируй любую строку, чтобы проверить):
// beach.Set(PhotoKeys.Brightness, "ярко");     // ключ Key<int>, а значение string
// int n = beach.Get(PhotoKeys.Name);           // Get вернёт string, а не int
// var fake = new Key<string>(1);               // конструктор internal: подделать ключ нельзя
