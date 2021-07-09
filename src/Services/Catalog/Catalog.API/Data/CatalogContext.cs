﻿using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            try
            {
                var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
                Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
                CatalogContextSeed.SeedData(Products);
            }
            catch(Exception ex)
            {
                Console.Write(ex);
            }
        }
        public IMongoCollection<Product> Products { get; }
    }
}
