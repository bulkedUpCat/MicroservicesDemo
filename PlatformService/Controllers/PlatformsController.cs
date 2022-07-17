using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [ApiController]
    [Route("api/platforms")]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepository _repo;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepository repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlatformReadDTO>>> GetPlatforms()
        {
            var platforms = await _repo.GetALlAsync();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDTO>>(platforms));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public async Task<ActionResult<PlatformReadDTO>> GetPlatformById(int id)
        {
            var platform = await _repo.GetByIdAsync(id);

            return platform != null ? Ok(_mapper.Map<PlatformReadDTO>(platform)) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDTO>> CreatePlatform([FromBody]PlatformCreateDTO model)
        {
            var platform = _mapper.Map<Platform>(model);
            await _repo.CreateAsync(platform);
            await _repo.SaveChangesAsync();

            return CreatedAtRoute(nameof(GetPlatformById), new { id = platform.Id }, _mapper.Map<PlatformReadDTO>(platform));
        }
    }
}
