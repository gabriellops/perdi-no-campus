using PerdiNoCampus.API.Repositories;
using PerdiNoCampus.API.Repositories.Interfaces;
using PerdiNoCampus.API.Services;
using PerdiNoCampus.API.Services.Interfaces;
using System.Text.Json.Serialization;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddScoped<Client>(sp =>
{
    var url = builder.Configuration["SupabaseUrl"];
    var key = builder.Configuration["SupabaseKey"];
    var options = new SupabaseOptions
    {
        AutoConnectRealtime = true
    };

    return new Client(url, key, options);
});

builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemService, ItemService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend",
        p => p
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());
});

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("Frontend");

app.MapControllers();

app.Run();
