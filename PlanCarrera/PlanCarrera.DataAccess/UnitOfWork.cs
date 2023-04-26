using PlanCarrera.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanCarrera.DataAccess
{
    public interface IUnitOfWork
    { 
        IPersonaRepository PersonaRepository { get; set; }
    
    }
    public class UnitOfWork
    {
        public UnitOfWork(DataContext db, IPersonaRepository personaRepository) 
        { 

            PersonaRepository = personaRepository;
            db = db;
        }

        public IPersonaRepository PersonaRepository { get; set; }
        public DataContext Db { get; set; }
    }
}
