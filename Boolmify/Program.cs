    using Boolmify;
    using Boolmify.Data;
    using Boolmify.Helper;
    using Boolmify.Interfaces;
    using Boolmify.Interfaces.ADminRepository;
    using Boolmify.Models;
    using Boolmify.Repository.AdminRepository;
    using Boolmify.Services;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    
    //Seed roles
  
    var builder = WebApplication.CreateBuilder(args);           

    // Add services to the container.
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSwaggerGen(option =>
    {
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        option.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                new string[]{}
            }
        });
    });
        
    builder.Services.AddControllers().AddNewtonsoftJson(option=>
    {
        option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        
    });
    builder.Configuration
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddUserSecrets<Program>(optional: true) // این خط مهمه
        .AddEnvironmentVariables();

    builder.Services.AddDbContext<ApplicationDBContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            
    });
    
    builder.Services.AddScoped<ITokenService , TokenService > ();
    builder.Services.AddScoped<IAdminUserService, AdminUserRepository>();
    builder.Services.AddScoped<IAdminBannerService, AdminBannerRepository>();
    builder.Services.AddScoped<IAdminCategoryService, AdminCategoryRepository>();
    builder.Services.AddScoped<IAdminCouponService, AdminCouponRepository>();
    builder.Services.AddScoped<IAdminFAQService, AdminFAQRepository>();
    builder.Services.AddScoped<IAdminOccasionService, AdminOccasionRepository>();
    builder.Services.AddScoped<IAdminOrderService, AdminOrderRepository>();
    builder.Services.AddScoped<IAdminProductService, AdminProductRepository>();
    builder.Services.AddScoped<IAdminTicketService, AdminTicketRepository>();
    
    
    builder.Services.AddIdentity<AppUser, IdentityRole<int>>(option =>
        {
            option.Password.RequireDigit = true;
            option.Password.RequireLowercase = true;
            option.Password.RequireUppercase = true;
            option.Password.RequireNonAlphanumeric = true;
            option.Password.RequiredLength = 8;
            option.Password.RequiredUniqueChars = 2;
        })
        .AddPasswordValidator<CustomPasswordValidation<AppUser>>()
        .AddEntityFrameworkStores<ApplicationDBContext>();
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme=
            options.DefaultChallengeScheme=
                options.DefaultForbidScheme=
                    options.DefaultScheme=
                        options.DefaultSignInScheme=
                            options.DefaultSignOutScheme= JwtBearerDefaults.AuthenticationScheme;
                    

    }).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigninKey"])),
            
                

        };
    });
    var app = builder.Build();

    // Seed roles & admin
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        await SeedData.SeedRolesAndAdminAsync(scope.ServiceProvider);
    }
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
