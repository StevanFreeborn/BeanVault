global using System.ComponentModel.DataAnnotations;
global using System.Net;
global using System.Text;
global using System.Text.Json;

global using BeanVault.Services.CouponService.API.Dtos;
global using BeanVault.Services.CouponService.API.Extensions;
global using BeanVault.Services.CouponService.API.Middleware;
global using BeanVault.Services.CouponService.API.Models;
global using BeanVault.Services.CouponService.Core.Exceptions;
global using BeanVault.Services.CouponService.Core.Interfaces;
global using BeanVault.Services.CouponService.Core.Models;
global using BeanVault.Services.CouponService.Infrastructure.DependencyInjection;

global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Versioning;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;
