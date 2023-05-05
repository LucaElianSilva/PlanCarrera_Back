using PlanCarrera.Business.DTOs;
using PlanCarrera.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanCarrera.Business.Mappers
{
    public class PersonaDTOMapper
    {
        public void MapToDTO(PersonaDTO toDTO, Persona fromEntity) 
        { 
            toDTO.Id = fromEntity.Id;
            toDTO.Nombre = fromEntity.Nombre;
            toDTO.DNI = fromEntity.DNI;
            toDTO.Edad = fromEntity.Edad;
            toDTO.Sexo = fromEntity.Sexo;
        }

        public List<PersonaDTO> MapToListDTO(List<Persona> listEntity) 
        {
            var listDTO = new List<PersonaDTO>();
            foreach (var item in listEntity) 
            {
                var personaDTO = new PersonaDTO();
                MapToDTO(personaDTO, item);
                listDTO.Add(personaDTO);
            }

            return listDTO;
        }
    }
}
