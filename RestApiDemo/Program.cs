using ActorRepositoryLib;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<IActorsRepository>(new ActorsRepository());

builder.Services.AddCors(opt =>
        opt.AddPolicy("AllowGetPut",
             builder => builder.AllowAnyOrigin().AllowAnyHeader().WithMethods("GET", "PUT")
             )
);

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
app.UseCors("AllowGetPut");

app.MapControllers();

app.Run();
