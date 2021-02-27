using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SharedCode;

namespace AzureServiceBus.Data
{
    public class AzureServiceBusContext : DbContext
    {
        public AzureServiceBusContext (DbContextOptions<AzureServiceBusContext> options)
            : base(options)
        {
        }

        public DbSet<SharedCode.PersonData> PersonData { get; set; }
    }
}
