using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Data
{
    public class APIContext : DbContext
    {
        public APIContext (DbContextOptions<APIContext> options)
            : base(options)
        {
        }

        public DbSet<API.Models.Room> Room { get; set; } = default!;
        public DbSet<API.Models.Reservations> Reservation { get; set; } = default!;
    }
}
