using Supabase;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
