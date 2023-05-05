using PlanCarrera.Business.DTOs;
using PlanCarrera.Business.Mappers;
using PlanCarrera.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanCarrera.Business.Services
{
    public interface IPersonaService
    {
        List<PersonaDTO> List();
    }

    public class PersonaService: IPersonaService
    {
        private readonly IPersonaRepository Repository;
        private PersonaDTOMapper _mapper;

        public PersonaService(IPersonaRepository personaRepository) 
        {
            Repository = personaRepository;
            _mapper = new PersonaDTOMapper();  
        }

        public List<PersonaDTO> List() 
        {
            var personas = Repository.GetAll();
            return  _mapper.MapToListDTO(personas);
        }
    }
}
