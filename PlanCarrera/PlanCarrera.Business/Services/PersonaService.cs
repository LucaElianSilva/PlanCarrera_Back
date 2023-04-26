using PlanCarrera.Business.DTOs;
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

        public PersonaService(IPersonaRepository personaRepository) 
        {
            Repository = personaRepository;
        }

        public List<PersonaDTO> List() 
        {
            var list = new List<PersonaDTO>();
            var personas = Repository.GetAll();

            foreach (var persona in personas) 
            {
                var personaDTO = new PersonaDTO();
                personaDTO.Id = persona.Id;
                personaDTO.Nombre = persona.Nombre;
                personaDTO.Edad = persona.Edad;
                personaDTO.DNI = persona.DNI;
                personaDTO.Sexo = persona.Sexo;

                list.Add(personaDTO);
            }
            return  list;
        }
    }
}
