using cls.api.pessoa.core.interfaces;
using cls.api.pessoa.infra;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IDataService, FakeData>();

var app = builder.Build();




app.MapGet("/", () =>
{
    var pessoas = app.Services.GetService<IDataService>();
    return pessoas?.GetAll();
});

app.Run();
