global using System.ComponentModel.DataAnnotations;
global using System.Net;
global using System.Text;
global using System.Text.Json;

global using BeanVault.Services.ProductService.API.Dtos;
global using BeanVault.Services.ProductService.API.Middleware;
global using BeanVault.Services.ProductService.API.Models;
global using BeanVault.Services.ProductService.Core.Exceptions;
global using BeanVault.Services.ProductService.API.Extensions;
global using BeanVault.Services.ProductService.Core.Interfaces;
global using BeanVault.Services.ProductService.Core.Models;
global using BeanVault.Services.ProductService.Infrastructure.DependencyInjection;

global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Versioning;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;
