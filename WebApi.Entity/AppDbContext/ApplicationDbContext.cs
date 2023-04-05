using WebApi.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Entity.AppDbContext;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Like> Likes { get; set; }
    public DbSet<User> Users { get; set; }

protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.Entity<Like>(l =>
		{
			l.HasOne(l => l.Author)
			.WithMany()
			.OnDelete(DeleteBehavior.Cascade); // При удалении автора - автор сообщения становится NULL

		});

		builder.Entity<User>(u =>
		{
			u.HasMany(u => u.Likes)
			.WithOne(m => m.Author)
			.OnDelete(DeleteBehavior.NoAction); // При удалении сообщения - с пользователем ничего не происходит

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
		base.OnModelCreating(builder);
	}
}
