using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Taboo.DAL;

namespace Taboo;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssemblyContaining<Program>();
        builder.Services.AddAutoMapper(typeof(Program));
        builder.Services.AddServices();
        builder.Services.AddDbContext<TabooDbContext>(opt =>
        {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("MSSql"));
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

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
    }
}
