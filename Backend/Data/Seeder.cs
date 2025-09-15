namespace Backend.Data
{
    public static class Seeder
    {
        public static void Seed(AppDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
