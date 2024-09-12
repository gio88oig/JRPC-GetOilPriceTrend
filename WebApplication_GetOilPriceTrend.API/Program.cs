using WebApplication_GetOilPriceTrend.Business.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add JsonRpc services
builder.Services.AddJsonRpc();

// Configure Dependency Injection
await builder.Services.AddBusinessServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Configure JsonRpc
app.UseJsonRpc();

app.Run();
