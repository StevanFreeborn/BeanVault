global using BeanVault.Services.CartService.Core.Interfaces;
global using BeanVault.Services.CartService.Core.Models;
global using BeanVault.Services.CartService.Infrastructure.Data.Mongo;
global using BeanVault.Services.CartService.Infrastructure.Data.Mongo.Repositories;

global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;

global using MongoDB.Bson.Serialization;
global using MongoDB.Bson.Serialization.IdGenerators;
global using MongoDB.Driver;
global using MongoDB.Driver.Linq;