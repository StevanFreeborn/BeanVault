global using BeanVault.Services.ProductService.Core.Exceptions;
global using BeanVault.Services.ProductService.Core.Interfaces;
global using BeanVault.Services.ProductService.Core.Models;
global using BeanVault.Services.ProductService.Infrastructure.Data.Mongo;
global using BeanVault.Services.ProductService.Infrastructure.Data.Mongo.Repositories;

global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;

global using MongoDB.Bson.Serialization;
global using MongoDB.Bson.Serialization.IdGenerators;
global using MongoDB.Driver;
global using MongoDB.Driver.Linq;