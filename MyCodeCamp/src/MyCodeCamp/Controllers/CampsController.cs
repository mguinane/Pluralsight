using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCodeCamp.Data;
using MyCodeCamp.Data.Entities;
using MyCodeCamp.Filters;
using MyCodeCamp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCodeCamp.Controllers
{
    [Route("api/[controller]")]
    [ValidateModel]
    public class CampsController : BaseController
    {
        private ILogger<CampsController> _logger;
        private ICampRepository _repository;
        private IMapper _mapper;

        public CampsController(ICampRepository repository, ILogger<CampsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var camps = _repository.GetAllCamps();

            return Ok(_mapper.Map<IEnumerable<CampModel>>(camps));
        }

        [HttpGet("{moniker}", Name = "CampGet")]
        public IActionResult Get(string moniker, bool includeSpeakers = false)
        {
            try
            {
                Camp camp = null;

                if (includeSpeakers)
                    camp = _repository.GetCampByMonikerWithSpeakers(moniker);
                else
                    camp = _repository.GetCampByMoniker(moniker);

                if (camp == null) return NotFound($"Camp {moniker} was not found.");

                return Ok(_mapper.Map<CampModel>(camp));
            }
            catch
            {
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize(Policy = "SuperUsers")]
        public async Task<IActionResult> Post([FromBody] CampModel model)
        {
            try
            {
                _logger.LogInformation("Creating a new Code Camp");

                var camp = _mapper.Map<Camp>(model);

                _repository.Add(camp);
                if (await _repository.SaveAllAsync())
                {
                    var newUri = Url.Link("CampGet", new { moniker = camp.Moniker });
                    return Created(newUri, _mapper.Map<CampModel>(camp));
                }
                else
                {
                    _logger.LogWarning("Could not save Camp to the database");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured while saving Camp: {ex.ToString()}");
            }
            return BadRequest();
        }

        [HttpPut("{moniker}")]
        public async Task<IActionResult> Put(string moniker, [FromBody] CampModel model)
        {
            try
            {
                var camp = _repository.GetCampByMoniker(moniker);
                if (camp == null) return NotFound($"Camp {moniker} was not found.");

                _mapper.Map(model, camp);

                if (await _repository.SaveAllAsync())
                {
                    return Ok(_mapper.Map<CampModel>(camp));
                }
            }
            catch (Exception)
            {

                throw;
            }
            return BadRequest("Could not update Camp");
        }

        [HttpDelete("{moniker}")]
        public async Task<IActionResult> Delete(string moniker)       
        {
            try
            {
                var camp = _repository.GetCampByMoniker(moniker);
                if (camp == null) return NotFound($"Camp {moniker} was not found.");

                _repository.Delete(camp);

                if (await _repository.SaveAllAsync())
                {
                    return Ok();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return BadRequest("Could not delete Camp");
        }
    }
}
