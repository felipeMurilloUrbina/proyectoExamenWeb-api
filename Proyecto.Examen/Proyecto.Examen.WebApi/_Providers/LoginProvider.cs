using MongoDB.Driver;
using Proyecto.Examen.WebApi._Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Examen.WebApi._Providers
{
    public class LoginProvider: IDisposable
    {
        public MongoClient _client = null;
        string _connectionString = "";
        public IMongoDatabase _database { get; set; }
        public IMongoDatabase Database
        {
            get
            {
                return this._database = _database == null ? this.MongoClient.GetDatabase("users") : _database;
            }
        }
        public MongoClient MongoClient
        { get
            {
                if(string.IsNullOrEmpty(_connectionString))
                    return this._client = _client ==null ? new MongoClient() : _client;
                else
                    return this._client = _client == null ? new MongoClient(_connectionString) : _client;

            }
        }
        public LoginProvider()
        {
            //this._connectionString = "mongodb://felip.murillo.u@gmail.com:Master20145.@cluster0-shard-00-00-3kvuy.mongodb.net:27017,cluster0-shard-00-01-3kvuy.mongodb.net:27017,cluster0-shard-00-02-3kvuy.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true";
        }
        public LoginProvider(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public void Insert(LoginUser loginUser)
        {
            try
            {
                var collection = this.Database.GetCollection<LoginUser>("Logins");
                collection.InsertOne(loginUser);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public ICollection<LoginUser> Get(string userId=null)
        {
            var logins=this.Database.GetCollection<LoginUser>("Logins");
            if (string.IsNullOrEmpty(userId))
                return logins.AsQueryable().ToList();
            else
                return logins.AsQueryable().Where(l => l.UserId.Equals(userId)).ToList();
        }

        public void Dispose()
        {
            this._database = null;

        }
    }
}