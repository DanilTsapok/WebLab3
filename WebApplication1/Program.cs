using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}
