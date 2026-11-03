using System.Collections.Immutable;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// ===== 1. Инициализация в ОДИН этап (immutable, без билдера) =====
Console.WriteLine("=== 1. Один этап: все данные сразу, потом менять нельзя ===");
var manual = new Gallery
{
    Albums = ImmutableArray.Create(new Album { Title = "Горы" }),
    Photos = ImmutableArray.Create(new Photo
    {
        Name = "Вершина", Rating = 5, Width = 1920, Height = 1080,
        InAlbum = new OptionalAlbumId(new AlbumId(0)),
    }),
};
Console.WriteLine("  " + manual.Describe(manual.Photos[0]));

// ===== 2. ПОЭТАПНАЯ инициализация через билдер =====
Console.WriteLine("\n=== 2. Билдер: настраиваем по частям ===");
var g = new GalleryBuilder();
var sea = g.AddAlbum().SetTitle("Море");
var city = g.AddAlbum().SetTitle("Город");

// Scope: общие настройки для нескольких фото
var seaScope = g.Scope();
seaScope.SetAlbum(sea).SetRating(4).SetSize(1920, 1080);
var beach = seaScope.AddPhoto().SetName("Пляж").SetRating(5);   // переопределили рейтинг
seaScope.AddPhoto().SetName("Закат");                           // всё из scope
seaScope.AddPhoto().SetName("Яхты").SetSize(3840, 2160);        // переопределили размер

// Делегат конфигурации + fluent
g.AddPhoto(p => p.SetName("Метро").SetRating(3).SetSize(1280, 720).SetAlbum(city));

// Scope тоже можно настроить делегатом
g.Scope(s =>
{
    s.SetAlbum(city).SetRating(3).SetSize(1280, 720);
    s.AddPhoto().SetName("Мост");
    s.AddPhoto().SetName("Площадь");
});

// Фото без альбома
g.AddPhoto().SetName("Без альбома").SetRating(2).SetSize(800, 600);

// Высокоуровневый метод: скопировать настройки из другого билдера
g.AddPhoto().CopyFrom(beach).SetName("Пляж (копия)");

var gallery = g.Build();
foreach (var p in gallery.Photos)
    Console.WriteLine("  " + gallery.Describe(p));

// ===== 3. Запрос по плоским данным (группировка через идентификаторы) =====
Console.WriteLine("\n=== 3. Группировка фото по альбомам ===");
foreach (var group in gallery.Photos.GroupBy(p => p.InAlbum))
    Console.WriteLine($"  {gallery.Get(group.Key)?.Title ?? "Без альбома"}: " +
                      string.Join(", ", group.Select(p => p.Name)));

// ===== 4. Валидация: ВСЕ ошибки сразу, со строкой кода =====
Console.WriteLine("\n=== 4. Валидация при Build() ===");
var bad = new GalleryBuilder();
bad.AddAlbum();                                          // нет названия
bad.AddPhoto().SetName("Без размера").SetRating(3);      // нет размера
bad.AddPhoto().SetSize(100, 100).SetRating(1);           // нет имени
try
{
    bad.Build();
}
catch (GalleryValidationException e)
{
    Console.WriteLine(e.Message);
}

// ===== 5. Валидация на уровне метода =====
Console.WriteLine("\n=== 5. Проверка значения в методе ===");
try
{
    new GalleryBuilder().AddPhoto().SetRating(10);
}
catch (ArgumentOutOfRangeException e)
{
    Console.WriteLine("  " + e.Message);
}

// ===== 6. Mutable модель меняется, immutable результат нет =====
Console.WriteLine("\n=== 6. Mutable vs Immutable ===");
g.Model.Photos[0].Name = "Изменено в модели";     // можно: модель mutable и открыта
Console.WriteLine("  mutable модель:  " + g.Model.Photos[0].Name);
Console.WriteLine("  готовая Gallery: " + gallery.Photos[0].Name);
// gallery.Photos[0].Name = "x";   // не скомпилируется: свойство init-only
