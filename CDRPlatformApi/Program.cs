
using CDRPlatform.AppServices.Interfaces;
using CDRPlatform.AppServices.Services;
using CDRPlatform.Data.Contexts;
using CDRPlatform.Data.Repositories;
using CDRPlatform.Domain.Interfaces.Repositories;
using CDRPlatform.Domain.Interfaces.Services;
using CDRPlatform.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace CDRPlatformApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAutoMapper(typeof(CDRPlatform.Domain.Mapping.CallDetailRecordProfile).Assembly);

            //services
            builder.Services.AddScoped<ICsvImportService, CsvImportService>();
            builder.Services.AddScoped<ICallDetailRecordService, CallDetailRecordService>();
            //reps
            builder.Services.AddScoped<ICallDetailRecordRepository, CallDetailRecordRepository>();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddControllers();
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
