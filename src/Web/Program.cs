using Core.Application.Interfaces.Notifications;
using Core.Application.Services;
using Core.Application.Strategies;
using Infrastructure;
using Web.Mapping;
using Web.Realtime;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddAutoMapper(typeof(ApiMappingProfile).Assembly);

builder.Services.AddSignalR();

builder.Services.AddScoped<SosService>();
builder.Services.AddScoped<DispatchService>();
builder.Services.AddScoped<RescueTeamService>();
builder.Services.AddScoped<FloodAlertService>();
builder.Services.AddScoped<FloodZoneService>();
builder.Services.AddScoped<ShelterService>();

builder.Services.AddScoped<PriorityScoreCalculator>();
builder.Services.AddScoped<IPriorityScoreStrategy, FloodSeverityPriorityStrategy>();
builder.Services.AddScoped<IPriorityScoreStrategy, HumanRiskPriorityStrategy>();
builder.Services.AddScoped<IPriorityScoreStrategy, WaitingTimePriorityStrategy>();

builder.Services.AddScoped<INotificationService, SignalRNotificationService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultCors", policy =>
    {
        var origins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
            ?? Array.Empty<string>();

        policy.WithOrigins(origins)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("DefaultCors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<DispatchHub>("/hubs/dispatch");
app.MapHub<CitizenAlertHub>("/hubs/citizen-alerts");
app.MapHub<RescueTeamHub>("/hubs/rescue-teams");

app.Run();
