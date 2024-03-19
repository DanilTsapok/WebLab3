using WebApplication1.Services.ApiService;
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
            builder.Services.AddSingleton<IBooksServices, BooksService>(); //C���� ������ ������ ������ ���� �� ������ �����䳿 ��� ������ � ��'����� ��������������� addsingleton
            builder.Services.AddSingleton<IUserService, UserService>();//C���� ������ ������ ������ ���� �� ������ �����䳿 ��� ������ � ��'����� ��������������� addsingleton
            builder.Services.AddSingleton<ILoanSevice, LoanService>();//C���� ������ ������ ������ ���� �� ������ �����䳿 ��� ������ � ��'����� ��������������� addsingleton
            builder.Services.AddControllers();
           
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


            app.MapControllers();

            app.Run();
        }
    }
}