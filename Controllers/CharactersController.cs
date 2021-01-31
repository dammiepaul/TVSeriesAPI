using Arch.EntityFrameworkCore.UnitOfWork;
using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TVSeriesAPI.Helpers;
using TVSeriesAPI.Models;

namespace TVSeriesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CharactersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{pageIndex?}/{pageSize?}")]
        public async Task<IActionResult> GetAllCharactersWithEpisodesAndLocations(int pageIndex = 0, int pageSize = 100)
        {
            try
            {
                var characterList = await _unitOfWork.GetRepository<Character>().GetPagedListAsync(
                                                                                                 include: source => source.Include(character => character.Location).Include(character => character.Episodes).ThenInclude(episodes => episodes.EpisodeComments),
                                                                                                 pageIndex: pageIndex,
                                                                                                 pageSize: pageSize
                                                                                               );

                if (characterList.Items.Any())
                {
                    return Ok(new JsonMessage<IPagedList<Character>>()
                    {
                        Success = true,
                        Results = new List<IPagedList<Character>> { characterList }
                    });
                }

                return NotFound(new JsonMessage<IPagedList<Character>>()
                {
                    Success = false,
                    ErrorMessage = "No record found"
                });

            }
            catch (Exception ex)
            {
                //log ex
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonMessage<IPagedList<string>>()
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpGet("{characterId}/episodes")]
        public async Task<IActionResult> GetAllEpisodesACharacterFeaturedIn(int characterId)
        {
            try
            {
                var characterData = await _unitOfWork.GetRepository<Character>().GetFirstOrDefaultAsync(
                                                                                                 include: source => source.Include(character => character.Location).Include(character => character.Episodes).ThenInclude(episodes => episodes.EpisodeComments),
                                                                                                 predicate: c => c.Id == characterId
                                                                                               );

                if (characterData is not null)
                {
                    return Ok(new JsonMessage<Character>()
                    {
                        Success = true,
                        Results = new List<Character> { characterData }
                    });
                }

                return NotFound(new JsonMessage<Character>()
                {
                    Success = false,
                    ErrorMessage = "No record found"
                });

            }
            catch (Exception ex)
            {
                //log ex
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonMessage<string>()
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
