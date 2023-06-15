global using System.ComponentModel.DataAnnotations;
global using System.Net;
global using System.Text.Json;
global using System.Text.RegularExpressions;

global using BeanVault.Services.AuthService.API.Dtos;
global using BeanVault.Services.AuthService.API.Middleware;
global using BeanVault.Services.AuthService.API.Validation;
global using BeanVault.Services.AuthService.Core.Exceptions;
global using BeanVault.Services.AuthService.Infrastructure.DependencyInjection;
global using BeanVault.Services.AuthService.Infrastructure.Interfaces;
global using BeanVault.Services.AuthService.Infrastructure.Models;

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Versioning;