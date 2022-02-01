namespace MusicEF
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new SampleContextFactory().CreateDbContext(args))
            {
            }
        }
    }
}
