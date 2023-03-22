using MongoDB.Driver;
using PrisonService.Data;
using PrisonService.Data.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonServiceWpf.Services
{
    public static class DataBaseManager
    {
        static DataBaseManager()
        {
            _host = "localhost:27017";
            _client = new MongoClient($"mongodb://{_host}");
            _db = _client.GetDatabase("PrisonServiceTest");

            GenereateCollections();
        }

        private static string _host;
        private static MongoClient _client;
        private static IMongoDatabase _db;

        private  static void GenereateCollections()
        {
            var collections =  _db.ListCollectionsAsync();

            if (collections != null)
                return;

            _db.CreateCollection("Prisoner");
            _db.GetCollection<Employee>("Employee"); 
            _db.GetCollection<Prison>("Prison"); 
            _db.GetCollection<Adress>("Adress");
            _db.GetCollection<State>("State");
            _db.GetCollection<Sex>("Sex");

            GenereateItems();
        }
        private async static void GenereateItems()
        {
            var collections = await _db.ListCollectionsAsync();

            if (collections == null)
                return;

            var item =  _db.GetCollection<Prisoner>("Prisoner");
            item.InsertMany(GenereatorStub.Prisoners);
        }
        private static void UpdateHost(string host)
        {
            _host = host;
            _client = new MongoClient($"mongodb://{_host}");
        }
    }
}
