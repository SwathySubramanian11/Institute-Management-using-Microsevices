using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace InstituteGateways
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddJsonFile("Ocelot.json", optional: false, reloadOnChange: true);
            builder.Services.AddOcelot(builder.Configuration);
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");
            app.UseOcelot();
            app.Run();
        }
    }
}
