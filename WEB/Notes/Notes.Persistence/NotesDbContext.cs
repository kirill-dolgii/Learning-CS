using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;
using Notes.Persistence.EntityTypeConfigurations;

namespace Notes.Persistence;

public sealed class NotesDbContext : DbContext, INotesDbContext
{
	public DbSet<Note> Notes { get; set; }

	public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options) {}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new NotesConfiguration());
		base.OnModelCreating(modelBuilder);
	}
}