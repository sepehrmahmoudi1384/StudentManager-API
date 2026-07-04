using StudentManager.API.Data;
using StudentManager.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();
builder.AddStudentManagerDb();

var app = builder.Build();

app.MigrateDb();

// Adding endpoints
app.MapStudentEndPoints();

app.Run();
