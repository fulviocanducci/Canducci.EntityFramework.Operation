using ConsoleAppTest.Models;
using ConsoleAppTest.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Canducci.EntityFramework.Operation;
namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            DbContextOptionsBuilder<Ctx> builder = new DbContextOptionsBuilder<Ctx>();
            builder.UseSqlite("Data Source = ./db.sqlite");
            builder.LogTo(x => Console.WriteLine(x), minimumLevel: Microsoft.Extensions.Logging.LogLevel.Information);
            using (Ctx ctx = new Ctx(builder.Options))
            {
                //ctx.Database.EnsureCreated();
                //Summary summary1 = new Summary
                //{
                //    Count = 0,
                //    Name = "Sumary 1"
                //};
                //Summary summary2 = new Summary
                //{
                //    Count = 0,
                //    Name = "Sumary 2"
                //};
                //ctx.Summary.AddRange(new[] { summary1, summary2 });
                //ctx.SaveChanges();

                ctx.Increment<Summary>(x => x.Count, x => x.Id == 1);
                ctx.Decrement<Summary>(x => x.Count, x => x.Id == 2);
            }
            Console.WriteLine("Hello World!");
        }
    }
}
