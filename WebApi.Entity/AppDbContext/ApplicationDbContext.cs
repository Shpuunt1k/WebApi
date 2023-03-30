using WebApi.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Entity.AppDbContext;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Theme> Themes { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
	public DbSet<Key> Keys { get; set; }
	public DbSet<Progress> Progresses{ get; set; }

protected override void OnModelCreating(ModelBuilder builder)
	{

		builder.Entity<Progress>()
			.HasOne(p => p.User)
			.WithOne(p => p.Progress)
			.OnDelete(DeleteBehavior.Cascade);

		builder.Entity<Message>(m =>
		{
			m.HasOne(m => m.Theme)
			.WithMany(t => t.Messages)
			.OnDelete(DeleteBehavior.Cascade); // При удалении темы - удаляются все сообщения

			m.HasOne(m => m.Author)
			.WithMany()
			.OnDelete(DeleteBehavior.SetNull); // При удалении автора - автор сообщения становится NULL

			m.Property("CreatedDate").HasDefaultValueSql("NOW()");
		});

		builder.Entity<Theme>(t =>
		{
			t.HasMany(t => t.Messages)
			.WithOne(m => m.Theme);

			t.HasOne(t => t.Author)
			.WithMany()
			.OnDelete(DeleteBehavior.SetNull); // При удалении автора - автор темы становится NULL

			t.Property("CreatedDate").HasDefaultValueSql("NOW()");
		});

		builder.Entity<User>(u =>
		{
			u.HasMany(u => u.Messages)
			.WithOne(m => m.Author)
			.OnDelete(DeleteBehavior.NoAction); // При удалении сообщения - с пользователем ничего не происходит

			u.HasMany(u => u.Themes)
			.WithOne(t => t.Author)
			.OnDelete(DeleteBehavior.NoAction); // При удалении темы - с пользователем ничего не происходит
		});

		//// Связь МНОГИЕ ко МНОГИМ через вспомогательный класс ThemeSection (таблицу ThemeSections) между Theme и Section
		//builder.Entity<ThemeSection>(ts =>
		//{
		//	ts.HasOne(ts => ts.Theme)
		//	.WithMany(ts => ts.Sections)
		//	.OnDelete(DeleteBehavior.Cascade); // При удалении темы - удаляется вспомогательная связь в таблице ThemeSections

		//	ts.HasOne(ts => ts.Section)
		//	.WithMany(ts => ts.Themes)
		//	.OnDelete(DeleteBehavior.Cascade);  // При удалении секции (раздела) - удаляется вспомогательная связь в таблице ThemeSections
		//});

		builder.Entity<Category>()
			.HasMany(c => c.Sections)
			.WithOne(s => s.Category)
			.OnDelete(DeleteBehavior.SetNull); // При удалении раздела - разделId у категории становится NULL

		base.OnModelCreating(builder);
	}
}
