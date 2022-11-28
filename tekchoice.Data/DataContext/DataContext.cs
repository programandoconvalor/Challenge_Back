using Microsoft.EntityFrameworkCore;
using tekchoice.Data.Models;

namespace tekchoice.Data.DataContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }
        // Add models that you use in API

        public virtual DbSet<CalculoModel>
           Calculo
        { get; set; }
    }
}
