using Autofac;
using Autofac.Extensions.DependencyInjection;
using PlanCarrera.DataAccess;
using StackExchange.Redis;

namespace PlanCarrera.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Este metodo es llamado al correr el proyecto. Se utiliza para añadir servicios, controladores, contextos, etc.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //Obtiene la sección redis del appsettings
            var redisConfig = Configuration.GetSection("Redis");

            //Obtiene el connectionString
            var connectionString = redisConfig.GetConnectionString("DataContext");

            // Se conecta al servidor de Redis generado
            var redis = ConnectionMultiplexer.Connect(connectionString);

            // Crear una base de datos
            var database = redis.GetDatabase();

            // Añade el datacontext desde el startup
            services.AddScoped<DataContext>(sp => new DataContext(database));

            // Registra la base de datos en el contenedor de servicios
            services.AddSingleton(database);

            // Añade los servicios y controladores al contenedor de servicios.
            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var builder = new ContainerBuilder();

            builder.Populate(services);
            IContainer ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });
        }
    }
}
