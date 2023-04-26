using Newtonsoft.Json;
using PlanCarrera.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanCarrera.DataAccess.Repositories
{
    public interface IPersonaRepository 
    { 
        List<Persona> GetAll();
    }
    public class PersonaRepository : IPersonaRepository
    {
        private DataContext dataContext { get; set; }
        public PersonaRepository(DataContext db) 
        { 
            dataContext = db;
        }

        public List<Persona> GetAll()
        {
            var json = dataContext._db.StringGet("Personas");
            var personas = JsonConvert.DeserializeObject<List<Persona>>(json);
            return JsonConvert.DeserializeObject<List<Persona>>(json);
        }
    }
}
