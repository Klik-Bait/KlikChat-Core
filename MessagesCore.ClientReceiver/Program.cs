using MessagesCore.ClientReceiver.Hubs;
using MessagesCore.ClientReceiver.MassTransit;
using MessagesCore.ClientReceiver.Messaging.Publishers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IMessageReceiver, MessageReceiver>();
builder.Services.ConfigureMassTransit(builder.Configuration);
builder.Services.AddSignalR();

var app = builder.Build();

app.MapGet("/hc", () => "Healthy!");

app.MapHub<ChatHub>("/chatHub");

app.Run();