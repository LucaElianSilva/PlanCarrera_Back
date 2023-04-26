using Autofac;
using Autofac.Extensions.DependencyInjection;
using PlanCarrera.Business.Services;
using PlanCarrera.DataAccess;
using PlanCarrera.DataAccess.Repositories;
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
            //Obtiene de la sección redis del appsettings el connectionString
            var connectionString = Configuration.GetSection("Redis:DataContext").Get<string>();

            // Se conecta al servidor de Redis generado
            var redis = ConnectionMultiplexer.Connect(connectionString);

            // Registra la base de datos en el contenedor de servicios
            services.AddSingleton<IConnectionMultiplexer>(redis);

            //Crea una base de datos en redis y Añade el datacontext desde el startup
            services.AddScoped(sp => new DataContext(redis.GetDatabase()));

            // Añade los servicios, repositorios y controladores al contenedor de servicios.
            services.AddControllers();
            services.AddScoped<IPersonaRepository, PersonaRepository>();
            services.AddScoped<IPersonaService, PersonaService>();

            // Añade los allows para la policy de cors
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder => {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    }
                );
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var builder = new ContainerBuilder();

            builder.Populate(services);

            IContainer ApplicationContainer = builder.Build();


            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseStatusCodePages();
            dataContext.CrearPersonas();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });

        }
    }
}
