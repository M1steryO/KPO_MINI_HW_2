using System;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Zoo.Application.Interfaces.Services;
using Zoo.Application.Services;
using Zoo.Presentation.DTO;

namespace Zoo.Presentation.Controllers
{
    [ApiController]
    [Route("api/feedingSchedules")]
    public class FeedingScheduleController : ControllerBase
    {
        private readonly IFeedingOrganizationService _feeding;

        public FeedingScheduleController(IFeedingOrganizationService feeding)
        {
            _feeding = feeding;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FeedingScheduleDto>> GetAll()
        {
            var list = _feeding.GetAllSchedules();
            var dtos = list.Select(s => new FeedingScheduleDto(s.Id, s.AnimalId, s.FeedingTime, s.FoodType, s.Completed));
            return Ok(dtos);
        }

        [HttpPost]
        public ActionResult Schedule([FromBody] ScheduleFeedingDto input)
        {
            _feeding.ScheduleFeeding(input.AnimalId, input.Time, input.FoodType);
            return Accepted();
        }

        [HttpPost("execute")]
        public ActionResult Execute([FromQuery] TimeSpan now)
        {
            _feeding.ExecuteFeeding(now);
            return Ok();
        }
    }
}

