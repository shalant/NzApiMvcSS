using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            //fetch data from db- domain walks
            var walksDomain = await walkRepository.GetAllAsync();

            //convert domain walks to DTO walks
            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walksDomain);
            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("id:guid")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            // get walk domain object from db
            var walkDomain = await walkRepository.GetAsync(id);

            // convert domain object to DTO
            var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);

            // return res
            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.AddWalkRequest addWalkRequest)
        {
            // convert dto to domain object
            var walkDomain = new Models.Domain.Walk
            {
                Length = addWalkRequest.Length,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            };

            // pass domain ojbect to repo to persist
            walkDomain = await walkRepository.AddAsync(walkDomain);

            //convert domain obj back to dto
            var walkDTO = new Models.DTO.Walk
            {
                Id = walkDomain.Id,
                Length = walkDomain.Length,
                Name = walkDomain.Name,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId
            };

            //send dto res back to client
            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);
        }

    }

    
}
