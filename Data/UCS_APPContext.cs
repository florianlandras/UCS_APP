using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UCS_APP.Models;

namespace UCS_APP.Data
{
    public class UCS_APPContext : DbContext
    {
        public UCS_APPContext (DbContextOptions<UCS_APPContext> options)
            : base(options)
        {
        }

        public DbSet<UCS_APP.Models.Album> Album { get; set; } = default!;

        public DbSet<UCS_APP.Models.Photo>? Photo { get; set; }
    }
}
