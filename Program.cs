using CargoTransAPI.Repositories;
using Google.Cloud.Firestore;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

var builder = WebApplication.CreateBuilder(args);

// Intialize Firebase admin SDK
FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("C:\\GoogleAppCred\\cargotrans-473716-firebase-adminsdk-fbsvc-eb7fa84553.json")
});


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
builder.Services.AddScoped<ShipmentRepository>();

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
