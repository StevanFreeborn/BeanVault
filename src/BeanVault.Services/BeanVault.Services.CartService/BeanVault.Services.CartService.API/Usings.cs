global using System.ComponentModel.DataAnnotations;
global using System.Net;
global using System.Text;
global using System.Text.Json;

global using BeanVault.Services.CartService.API.Dtos;
global using BeanVault.Services.CartService.API.Extensions;
global using BeanVault.Services.CartService.API.Middleware;
global using BeanVault.Services.CartService.API.Models;
global using BeanVault.Services.CartService.Core.Interfaces;
global using BeanVault.Services.CartService.Core.Models;
global using BeanVault.Services.CartService.Infrastructure.DependencyInjection;

global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Versioning;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;

global using System.IdentityModel.Tokens.Jwt;
global using BeanVault.Services.CartService.Core.Exceptions;