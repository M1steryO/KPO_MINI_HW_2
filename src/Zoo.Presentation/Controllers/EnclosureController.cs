using System;
using Domain.Entities;
using System.Runtime.InteropServices;
using Zoo.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Zoo.Presentation.DTO;
using Domain.ValueObjects;

namespace Zoo.Presentation.Controllers
{
    [ApiController]
    [Route("api/enclosures")]
    public class EnclosureController : ControllerBase
    {
        private readonly IEnclosureRepository _enclosures;

        public EnclosureController(IEnclosureRepository enclosures)
        {
            _enclosures = enclosures;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EnclosureDto>> GetAll()
        {
            var list = _enclosures.GetAll();
            var dtos = list.Select(e => new EnclosureDto(e.Id, e.Type, e.Size, e.CurrentCount, e.Capacity.Max));
            return Ok(dtos);
        }

        [HttpPost]
        public ActionResult<EnclosureDto> Create([FromBody] CreateEnclosureDto input)
        {
            var entity = new Enclosure(input.Type, input.Size, new Capacity(input.Capacity));
            _enclosures.Add(entity);
            var dto = new EnclosureDto(entity.Id, entity.Type, entity.Size, entity.CurrentCount, entity.Capacity.Max);
            return CreatedAtAction(nameof(GetAll), null, dto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _enclosures.Remove(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}

