using CAPGEMINI_CROPDEAL.Data;
using CAPGEMINI_CROPDEAL.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CAPGEMINI_CROPDEAL.Services;
using CAPGEMINI_CROPDEAL.DTO;
using CAPGEMINI_CROPDEAL.Models;
using Microsoft.EntityFrameworkCore;
using CAPGEMINI_CROPDEAL.repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using CAPGEMINI_CROPDEAL.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<CropDealDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IUpdateRepository<>), typeof(GenericUpdateRepository<>));
builder.Services.AddScoped<IUpdateService<Farmer, FarmerDTO>>(provider =>
{
    var repo = provider.GetRequiredService<IUpdateRepository<Farmer>>();
    return new GenericUpdateService<Farmer, FarmerDTO>(repo, FarmerMapper.Map);
});

builder.Services.AddScoped<IUpdateService<Buyer, BuyerDTO>>(provider =>
{
    var repo = provider.GetRequiredService<IUpdateRepository<Buyer>>();
    return new GenericUpdateService<Buyer, BuyerDTO>(repo, BuyerMapper.Map);
});

builder.Services.AddScoped<IManipulateCropRepository, ManipulateCropRepository>();
builder.Services.AddScoped<IAddCropService, AddCropService>();
builder.Services.AddScoped<IUpdateCropService, UpdateCropService>();
builder.Services.AddScoped<ICropDeleteRepository, CropDeleteRepository>();
builder.Services.AddScoped<ICropDeleteService, CropDeleteService>();

builder.Services.AddSingleton<RabbitMQService>();
builder.Services.AddSingleton<CropEventPublisher>();

// CHANGED: register interfaces for easier mocking/testing
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICropSubscriptionService, CropSubscriptionService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IAdminService, AdminService>();

builder.Services.AddHostedService<CropNotificationConsumer>();

builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CropDealDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Token like: Bearer {your token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = { "Admin", "Farmer", "Buyer" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

app.Run();