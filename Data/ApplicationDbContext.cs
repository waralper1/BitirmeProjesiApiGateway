using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BitirmeProjesiErp.Models;
using System.Threading.Tasks;

namespace BitirmeProjesiErp.Data
{ 
    public class ApplicationDbContext : IdentityDbContext
    {
        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
    }
}
