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

        private IDatabase _db { get; set; }
        public DataContext(IDatabase db)
        {
            _db = db;
            CrearPersonas();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
        }

        public void CrearPersonas() {
            var personas = new List<Persona>()
            {
                new Persona(){ Id = 1, Nombre = "Jhon", Edad = 22, DNI = "42901989", Sexo = "Masculino" },
                new Persona(){ Id = 2, Nombre = "Tomy", Edad = 20, DNI = "42901459", Sexo = "Masculino" },
                new Persona(){ Id = 3, Nombre = "Jade", Edad = 24, DNI = "42456989", Sexo = "Femenino" },
                new Persona(){ Id = 4, Nombre = "Emily", Edad = 27, DNI = "42452989", Sexo = "Femenino" },
                new Persona(){ Id = 5, Nombre = "Alex", Edad = 18, DNI = "42789689", Sexo = "Femenino" },
            };

            // Convertir el array de objetos JSON a una cadena JSON
            var jsonString = JsonConvert.SerializeObject(personas);

            // Almacena la cadena JSON en Redis siempre y cuando no exista
            _db.StringSet("Personas", jsonString, when: When.NotExists);
        }
    }
}
