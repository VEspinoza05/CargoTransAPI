using CargoTransAPI.Repositories;
using Google.Cloud.Firestore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Inyect FirestoreDb
builder.Services.AddSingleton(provider =>
{
    string projectId = "cargotrans-473716";
    return FirestoreDb.Create(projectId);
});

// Inyect UserRepository
builder.Services.AddScoped<UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapControllers(); 

app.Run();
