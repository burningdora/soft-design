// Модель данных: фото
class Photo
{
    public int Brightness, BlurRadius;
    public string Filter = "none";
    public override string ToString() => $"яркость={Brightness}, размытие={BlurRadius}, фильтр={Filter}";
}
