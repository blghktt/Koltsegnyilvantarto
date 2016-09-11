using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Koltseg.Models;

namespace Koltseg.DAL
{
    public class KoltsegContext : DbContext
    {
        public KoltsegContext() : base("Koltseg")
        {
        }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<IncomeItem> IncomeItems { get; set; }
        public DbSet<Spending> Spendings { get; set; }
        public DbSet<SpendingItem> SpendingItems { get; set; }
        public DbSet<User> Users { get; set; }

    }
}