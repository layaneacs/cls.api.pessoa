using cls.api.pessoa.core.interfaces;
using cls.api.pessoa.infra;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IDataService, FakeData>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
