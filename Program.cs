using Api_Lanchonete_Sprint.Data;
using Api_Lanchonete_Sprint.Repositories;
using Api_Lanchonete_Sprint.Repositories.Interfaces;
using Api_Lanchonete_Sprint.Services;
using Api_Lanchonete_Sprint.Services.Interfaces;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;

using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// ==========================================
// 1. CONNECTION STRING
// ==========================================
var connectionString =
    builder.Configuration
    .GetConnectionString("ConexaoPadrao");


// ==========================================
// 2. DB CONTEXT
// ==========================================
builder.Services.AddDbContext<LanchoneteContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    )
);


// ==========================================
// 3. REPOSITORIES
// ==========================================
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IFornecedorRepository, FornecedorRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();


// ==========================================
// 4. SERVICES
// ==========================================
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IFornecedorService, FornecedorService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IAuthService, AuthService>();


// ==========================================
// 5. CORS
// ==========================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


// ==========================================
// 6. JWT AUTHENTICATION
// ==========================================
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer =
                    builder.Configuration["Jwt:Issuer"],

                ValidAudience =
                    builder.Configuration["Jwt:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["Jwt:Key"]
                        )
                    )
            };
    });


// ==========================================
// 7. CONTROLLERS + JSON
// ==========================================
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            ReferenceHandler.IgnoreCycles;
    });


// ==========================================
// 8. SWAGGER + JWT
// ==========================================
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Lanchonete",
        Version = "v1"
    });

    // CONFIGURAÇÃO JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
            @"JWT Authorization header usando Bearer.
              Exemplo: 'Bearer SEU_TOKEN'",

        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },

            new List<string>()
        }
    });
});


// ==========================================
// BUILD APP
// ==========================================
var app = builder.Build();


// ==========================================
// 9. ARQUIVOS ESTÁTICOS
// ==========================================
DefaultFilesOptions defaultFilesOptions =
    new DefaultFilesOptions();

defaultFilesOptions.DefaultFileNames.Clear();

defaultFilesOptions.DefaultFileNames.Add("login.html");

app.UseDefaultFiles(defaultFilesOptions);

app.UseStaticFiles();


// ==========================================
// 10. SWAGGER
// ==========================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}


// ==========================================
// 11. MIDDLEWARES
// ==========================================
app.UseHttpsRedirection();

app.UseCors("AllowAll");

// IMPORTANTE:
// Authentication ANTES de Authorization
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();