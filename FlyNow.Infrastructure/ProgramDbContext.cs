using Microsoft.EntityFrameworkCore;

namespace FlyNow.Infrastructure
{
	public class ProgramDbContext : DbContext
	{
		private DbContextOptions<ProgramDbContext> options;

		public ProgramDbContext(DbContextOptions<ProgramDbContext> options) : base(options) { }

	}
}
