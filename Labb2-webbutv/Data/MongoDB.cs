//using Labb2_webbutv.Models;
//using MongoDB.Driver;

//namespace Labb2_webbutv.Data
//{
//    public class MongoDB
//    {
//        private IMongoDatabase _db;
//        private string connectionString;
//        public MongoDB(string database)
//        {
//            var client = new MongoClient();
//            _db = client.GetDatabase(database);
//        }

//        //READ ALL POKEMONS
//        public async Task<List<Pokemon>> GetAllPokemons(string table)
//        {
//            var collection = _db.GetCollection<Pokemon>(table);
//            return collection.AsQueryable().ToList();
//        }

//        //READ ALL ITEMS
//        public async Task<List<Item>> GetAllItems(string table)
//        {
//            var collection = _db.GetCollection<Item>(table);
//            return collection.AsQueryable().ToList();
//        }

//        //READ POKEMON BY ID
//        public async Task<Pokemon> GetPokemonByID(string table, Guid id)
//        {
//            var collection = _db.GetCollection<Pokemon>(table);
//            return collection.AsQueryable().ToList().Find(x => x.Id == id);
//        }

//        //READ ITEM BY ID
//        public async Task<Item> GetItemByID(string table, Guid id)
//        {
//            var collection = _db.GetCollection<Item>(table);
//            return collection.AsQueryable().ToList().Find(x => x.Id == id);
//        }

//        //CREATE POKEMON
//        public async Task<Pokemon> AddPokemon(string table, Pokemon pokemon)
//        {
//            var collection = _db.GetCollection<Pokemon>(table);
//            collection.InsertOneAsync(pokemon);
//            return pokemon;
//        }

//        //CREATE ITEM
//        public async Task<Item> AddItem(string table, Item item)
//        {
//            var collection = _db.GetCollection<Item>(table);
//            collection.InsertOneAsync(item);
//            return item;
//        }

//        //UPDATE POKEMON
//        public async Task<Pokemon> UpdatePokemon(string table, Pokemon pokemon)
//        {
//            var collection = _db.GetCollection<Pokemon>(table);
//            collection.ReplaceOne(x => x.Id == pokemon.Id, pokemon, new ReplaceOptions { IsUpsert = true });
//            return pokemon;
//        }

//        //UPDATE ITEM
//        public async Task<Item> UpdateItem(string table, Item item)
//        {
//            var collection = _db.GetCollection<Item>(table);
//            collection.ReplaceOne(x => x.Id == item.Id, item, new ReplaceOptions { IsUpsert = true });
//            return item;
//        }

//        //DELETE POKEMON
//        public async Task DeletePokemon(string table, Guid id)
//        {
//            var collection = _db.GetCollection<Pokemon>(table);
//            collection.DeleteOne(x => x.Id == id);
//        }
//        //DELETE ITEM
//        public async Task DeleteItem(string table, Guid id)
//        {
//            var collection = _db.GetCollection<Item>(table);
//            collection.DeleteOne(x => x.Id == id);
//        }
//    }
//}
