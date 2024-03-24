using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApplication1.Services.ApiService;
using WebApplication1.Services.AuthService;
using WebApplication1.Services.AuthServices;
using WebApplication1.Services.BookService;
using WebApplication1.Services.LoanService;
using WebApplication1.Services.UserService;



namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<IApiClient, ApiClient>();
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IBooksServices, BooksService>(); //Cервіс зберігає єдиний список книг та методів взаємодії для роботи з об'єктом використовується addsingleton
            builder.Services.AddSingleton<IUserService, UserService>();//Cервіс зберігає єдиний список книг та методів взаємодії для роботи з об'єктом використовується addsingleton
            builder.Services.AddSingleton<ILoanSevice, LoanService>();//Cервіс зберігає єдиний список книг та методів взаємодії для роботи з об'єктом використовується addsingleton
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddControllers();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT")["Key"]))
                };
            });
           
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {

            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Books", Version = "v1" });

            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter JWT Bearer token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            };
                options.AddSecurityDefinition("Bearer", securityScheme);

            var securityRequirement = new OpenApiSecurityRequirement
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
                };
                options.AddSecurityRequirement(securityRequirement);
        }
            
                
                
                );

            var app = builder.Build();

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
            var test = BCrypt.Net.BCrypt.EnhancedHashPassword("123");
            Console.WriteLine(test);
            app.Run();
        }
    }
}
