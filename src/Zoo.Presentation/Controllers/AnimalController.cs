using System;
using System.Runtime.InteropServices;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Zoo.Application.Interfaces;
using Zoo.Presentation.DTO;

namespace Zoo.Presentation.Controllers
{
    [ApiController]
    [Route("api/animals")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalRepository _animals;

        public AnimalController(IAnimalRepository animals)
        {
            _animals = animals;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AnimalDto>> GetAll()
        {
            var list = _animals.GetAll();
            var dtos = list.Select(a => new AnimalDto(a.Id, a.Species.Value, a.Name.Value, a.BirthDate, a.Gender, a.FavoriteFood, a.IsHealthy, a.EnclosureId));
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public ActionResult<AnimalDto> Get(Guid id)
        {
            try
            {
                var a = _animals.GetById(id);
                var dto = new AnimalDto(a.Id, a.Species.Value, a.Name.Value, a.BirthDate, a.Gender, a.FavoriteFood, a.IsHealthy, a.EnclosureId);
                return Ok(dto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult<AnimalDto> Create([FromBody] CreateAnimalDto input)
        {
            var entity = new Animal(
                new Species(input.Species),
                new AnimalName(input.Name),
                input.BirthDate,
                input.Gender,
                input.FavoriteFood);
            _animals.Add(entity);
            var dto = new AnimalDto(entity.Id, input.Species, input.Name, input.BirthDate, input.Gender, input.FavoriteFood, entity.IsHealthy, null);
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, dto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _animals.Remove(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}

