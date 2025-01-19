using Microsoft.EntityFrameworkCore;
using GameTicketing.Database;
using GameTicketing.Services.Abstractions;
using GameTicketing.Services.Implementations;

namespace GameTicketing;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services
            .AddDbContext<
                TicketingDatabaseContext>(o =>
                o.UseSqlite("Datasource=../GameTickets.db;")) 
            .AddScoped<IUserService, UserService>();
        builder.Services.AddControllers();
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


        app.MapControllers();

        app.Run();
    }
}