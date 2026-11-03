// АБСТРАКЦИЯ: чистая логика с именами, ничего не знает о контексте и pipeline
static class PhotoOps
{
    public static void Brighten(Photo p, int d) => p.Brightness += d;
    public static void Blur(Photo p, int r) => p.BlurRadius += r;
    public static void SetFilter(Photo p, string f) => p.Filter = f;
}
