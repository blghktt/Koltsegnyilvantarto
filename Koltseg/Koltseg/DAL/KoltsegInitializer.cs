using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Koltseg.Models;

namespace Koltseg.DAL
{
    public class KoltsegInitializer : System.Data.Entity.DropCreateDatabaseAlways<KoltsegContext>
    {
        protected override void Seed(KoltsegContext context)
        {
            var cat = new List<Category>
            {
                new Category {ID = 1, Name = "Fizetés" },
                 new Category {ID = 2, Name="Könyvutalvány"},
                  new Category { ID = 3,Name = "Jutalom"},

                new Category {ID = 4, Name="Közüzemi számla"},
                new Category {ID = 5,Name = "Szabadidő" },


            };

            cat.ForEach(s => context.Categories.Add(s));
            context.SaveChanges();


            var user = new List<User>
            {
                new User {ID= 1, UserName = "Anya" , Password = "12345"},
                 new User {ID= 2,UserName = "Apa" , Password = "12345"},
                 new User {ID= 3,UserName = "Bagamér" , Password = "12345"},
                 new User {ID= 4,UserName = "Benedek" , Password = "12345"},
                 new User {ID= 5,UserName = "Balázs" , Password = "12345"},

            };

            user.ForEach(s => context.Users.Add(s));
            context.SaveChanges();

            var beveteliTetel = new List<IncomeItem>
            {
                new IncomeItem {ID = 1,Name = "Anya fizetés", CategoryID = 1, LastValue = 156000},
                 new IncomeItem {ID = 2,Name = "Apa fizetés", CategoryID = 1, LastValue = 156000},
                  new IncomeItem {ID = 3,Name = "Anya jutalom", CategoryID = 3, LastValue = 100000},
                   new IncomeItem {ID = 4,Name = "Apa jutalom", CategoryID = 3, LastValue = 100000},
                    new IncomeItem {ID = 5,Name = "Anya könyvutalvány", CategoryID = 2, LastValue = 15600},

            };

            beveteliTetel.ForEach(s => context.IncomeItems.Add(s));
            context.SaveChanges();


            var kiadasiTetel = new List<SpendingItem>
            {
               new SpendingItem {ID = 1,Name = "Internet", CategoryID = 4, LastValue = 3000},
                 new SpendingItem {ID = 2,Name = "Villany", CategoryID = 4, LastValue = 5000},
                  new SpendingItem {ID = 3,Name = "Nyaralás", CategoryID = 5, LastValue = 1000000},
                   new SpendingItem {ID = 4,Name = "Mozi", CategoryID = 5, LastValue = 10000},
                    new SpendingItem {ID = 5,Name = "Fűtés", CategoryID = 4, LastValue = 10000},

            };

            kiadasiTetel.ForEach(s => context.SpendingItems.Add(s));
            context.SaveChanges();


            var bevétel = new List<Income>
            {
               new Income {ID = 1,UserID = 1, IncomeItemID = 1, Value =156000 , CreatedTime = DateTime.Now},
                 new Income {ID = 2,UserID = 2, IncomeItemID = 4, Value =156000 , CreatedTime = DateTime.Now},
                  new Income {ID = 3,UserID = 1, IncomeItemID = 3, Value =100000 , CreatedTime = DateTime.Now},

            };

            bevétel.ForEach(s => context.Incomes.Add(s));
            context.SaveChanges();


            var kiadas = new List<Spending>
            {
               new Spending {ID = 1,UserID = 1, SpendingItemID = 1, Value =156000 , CreatedTime = DateTime.Now},
                 new Spending {ID = 2,UserID = 2, SpendingItemID = 2, Value =1560 , CreatedTime = DateTime.Now},
                  new Spending {ID = 3,UserID = 1, SpendingItemID = 3, Value =100000 , CreatedTime = DateTime.Now},

            };

            kiadas.ForEach(s => context.Spendings.Add(s));
            context.SaveChanges();

        }
    }
}