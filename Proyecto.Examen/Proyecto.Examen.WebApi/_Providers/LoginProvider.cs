using MongoDB.Driver;
using Proyecto.Examen.WebApi._Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Examen.WebApi._Providers
{
    public class LoginProvider
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

    }
}