namespace Notes.Persistence;

public class DbInitializer
{
	public static async void Initialize(NotesDbContext context)
	{
		await context.Database.EnsureCreatedAsync();
	}
}