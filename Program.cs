using MudBlazor.Services;
using Client_ui.Components;
using Microsoft.EntityFrameworkCore;
using System;
using Client_ui.Persistance;
using Client_ui.Service;
using Client_ui.Components.Enum;
using ApexCharts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Client_ui.Components.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Client_ui.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

builder.Services.AddAuthorizationCore();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddDbContext<WorkoutAppDbContext>(option =>
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddRazorComponents(options =>
    options.TemporaryRedirectionUrlValidityDuration =
        TimeSpan.FromMinutes(7));

builder.Services.AddAntiforgery();
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
// Dodaj MongoDB Driver
builder.Services.AddSingleton<IMongoDbService, MongoDbService>();

// Dodaj HttpClient dla Raspberry Pi
builder.Services.AddHttpClient<IRPComunnicationService, RPComunnicationService>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(30);
});
//APEX CHART
builder.Services.AddApexCharts();
builder.Services.AddHttpClient();
builder.Services.AddCors(opts =>
{
    opts.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddScoped<IWorkoutService, WorkoutService>();
builder.Services.AddScoped<IExerciseService, ExerciseService>();
builder.Services.AddScoped<IChartService, ChartService>();
builder.Services.AddHttpClient<IChatService, ChatService>(client => {
    client.BaseAddress = new Uri("https://localhost:7069/");
});
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<CustomAuthenticationService>();
builder.Services.AddScoped<ChatController>();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();  
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseAntiforgery();
app.UseCors("AllowAll");
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
