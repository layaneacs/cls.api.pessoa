using cls.api.pessoa.core.interfaces;
using cls.api.pessoa.infra;
using cls.api.pessoa.infra.Configurations;

var builder = WebApplication.CreateBuilder(args);

var mongoSettings = builder.Configuration.GetSection("MongoSettings");

builder.Services.Configure<MongoSettings>(mongoSettings);
builder.Services.AddSingleton<IDataService, DbMongoData>();


builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
