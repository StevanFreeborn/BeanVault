global using System.ComponentModel.DataAnnotations.Schema;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text;

global using BeanVault.Services.AuthService.Infrastructure.Data.Postgres;
global using BeanVault.Services.AuthService.Infrastructure.Data.Postgres.Repositories;
global using BeanVault.Services.AuthService.Infrastructure.Interfaces;
global using BeanVault.Services.AuthService.Infrastructure.Models;
global using BeanVault.Services.AuthService.Infrastructure.Services;

global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;