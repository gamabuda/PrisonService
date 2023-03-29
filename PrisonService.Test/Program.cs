using MongoDB.Driver;
using PrisonService.Data;
using PrisonService.Data.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonService.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ls = GenereatorStub.Prisoners;

            MongoClient client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("maintestlast");

            db.CreateCollection("adress");  // создаем коллекцию "adresses"
            var colAdress = db.GetCollection<Adress>("adress");
            colAdress.InsertMany(GenereatorStub.Adresses);

            db.CreateCollection("prison");  // создаем коллекцию "prison"
            var colPrisons = db.GetCollection<Prison>("prison");
            colPrisons.InsertMany(GenereatorStub.Prisons);

            db.CreateCollection("prisoner");  // создаем коллекцию "prisoner"
            var colPrisoner = db.GetCollection<Prisoner>("prisoner");
            colPrisoner.InsertMany(GenereatorStub.Prisoners);

            db.CreateCollection("employee");  // создаем коллекцию "employee"
            var colEmployee = db.GetCollection<Employee>("employee");
            colEmployee.InsertMany(GenereatorStub.Employees);

            Console.WriteLine("Complete");
            Console.ReadLine();
        }
    }
}
