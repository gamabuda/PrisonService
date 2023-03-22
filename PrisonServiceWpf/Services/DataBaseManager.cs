using MongoDB.Driver;
using PrisonService.Data;
using PrisonService.Data.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonServiceWpf.Services
{
    public static class DataBaseManager
    {
        public static Employee Employee { get; set; }
        static DataBaseManager()
        {
            _host = "localhost:27017";
            _client = new MongoClient($"mongodb://{_host}");
            _db = _client.GetDatabase("maintest1");
        }

        private static string _host;
        private static MongoClient _client;
        private static IMongoDatabase _db;

        public static List<Prisoner> GetPrisoners() 
        {
            return (List<Prisoner>)_db.GetCollection<Prisoner>("prisoner").AsQueryable<Prisoner>().ToList();
        }

        public static List<Prison> GetPrisons()
        {
            return _db.GetCollection<Prison>("prison").AsQueryable<Prison>().ToList();
        }

        public static List<Adress> GetAdresses()
        {
            return _db.GetCollection<Adress>("adress").AsQueryable<Adress>().ToList(); 
        }

        public static List<Employee> GetEmployees()
        {
            return _db.GetCollection<Employee>("employee").AsQueryable<Employee>().ToList();
        }

        public static bool TryAddPrisoner(Prisoner prisoner)
        {
           try
            {
                var colPrisoner = _db.GetCollection<Prisoner>("prisoner");
                var q = _db.GetCollection<Prisoner>("prisoner").AsQueryable<Prisoner>().ToList();

                if(q.Contains(prisoner))
                {
                    colPrisoner.ReplaceOne(null, prisoner);
                }
                else
                {
                    colPrisoner.InsertOne(prisoner);
                }
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool TryRemovePrisoner(Prisoner prisoner)
        {
            try
            {
                var colPrisoner = _db.GetCollection<Prisoner>("prisoner");
                colPrisoner.DeleteOne(a => a.Id == prisoner.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
