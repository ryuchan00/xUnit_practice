using TrainingApp.External;
using TrainingApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddHttpClient<IPaymentGateway, PaymentGateway>();
builder.Services.AddSingleton(TimeProvider.System);
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

app.UseAuthorization();

app.MapControllers();

app.Run();

// WebApplicationFactory<Program> からこのアプリを起動できるようにするためのマーカー。
// (トップレベルステートメントで生成される Program クラスは既定では internal のため、
//  テストプロジェクトから参照できるように partial class を明示的に公開する)
public partial class Program;
