using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PlanCarrera.Domain.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanCarrera.DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<Persona> Persona { get; set; }

        public IDatabase _db { get; set; }
        public DataContext(IDatabase db)
        {
            _db = db;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura las entidades
            base.OnModelCreating(modelBuilder);
        }

        public void CrearPersonas() 
        {
            var personas = new List<Persona>()
            {
                new Persona(){ Id = 1, Nombre = "Jhon Keneddy", Edad = 22, DNI = "42901989", Sexo = "Masculino" },
                new Persona(){ Id = 2, Nombre = "Tomy Shelby", Edad = 20, DNI = "42901459", Sexo = "Masculino" },
                new Persona(){ Id = 3, Nombre = "Jade Classius", Edad = 24, DNI = "42456989", Sexo = "Femenino" },
                new Persona(){ Id = 4, Nombre = "Emily Blunt", Edad = 27, DNI = "42452989", Sexo = "Femenino" },
                new Persona(){ Id = 5, Nombre = "Alex Mount", Edad = 18, DNI = "42789689", Sexo = "Femenino" },
            };

            // Convertir el array de objetos JSON a una cadena JSON
            var jsonString = JsonConvert.SerializeObject(personas);

            // Almacena la cadena JSON en Redis siempre y cuando no exista
            var redisValue = _db.StringGet("Personas");
            if (redisValue.IsNullOrEmpty)
            {
                _db.StringSet("Personas", jsonString);
            }
        }
    }
}
