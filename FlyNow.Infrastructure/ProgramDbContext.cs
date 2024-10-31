using Microsoft.EntityFrameworkCore;

namespace FlyNow.Infrastructure
{
	public class ProgramDbContext : DbContext
	{
		DbContextOptions<ProgramDbContext> options;

		public ProgramDbContext(DbContextOptions<ProgramDbContext> options) : base(options) { }

	}
}
