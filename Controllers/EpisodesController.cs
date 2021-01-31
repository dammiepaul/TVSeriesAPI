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
using TVSeriesAPI.Models.DTOs;

namespace TVSeriesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public EpisodesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{pageIndex?}/{pageSize?}")]
        public async Task<IActionResult> GetAllEpisodesWithCharactersAndComments(int pageIndex = 0, int pageSize = 100)
        {
            try
            {
                var episodesList = await _unitOfWork.GetRepository<Episode>().GetPagedListAsync(    include: source => source.Include(episode => episode.EpisodeComments).Include(episode => episode.Characters),
                                                                                                    pageIndex: pageIndex,
                                                                                                    pageSize: pageSize      );

                if (episodesList.Items.Any())
                {
                    return Ok(new JsonMessage<IPagedList<Episode>>()
                    {
                        Success = true,
                        Results = new List<IPagedList<Episode>> { episodesList }
                    });
                }

                return NotFound(new JsonMessage<IPagedList<Episode>>()
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

        [HttpGet("sortedByReleaseDateWithCommentsCount/{pageIndex?}/{pageSize?}")]
        public async Task<IActionResult> GetAllEpisodesSortedByReleaseDateAscWithCommentsCount(int pageIndex = 0, int pageSize = 100)
        {
            try
            {
                var episodesList = await _unitOfWork.GetRepository<Episode>().GetPagedListAsync(
                                                                                                 selector: e => new EpisodeDto { Id = e.Id, Name = e.Name, EpisodeCode = e.EpisodeCode, ReleaseDate = e.ReleaseDate, NumberOfComments = e.EpisodeComments.Count, Created = e.Created, Modified = e.Modified },
                                                                                                 include: source => source.Include(episode => episode.EpisodeComments),
                                                                                                 orderBy: source => source.OrderBy(e => e.ReleaseDate),
                                                                                                 pageIndex: pageIndex,
                                                                                                 pageSize: pageSize
                                                                                               );

                if (episodesList.Items.Any())
                {
                    return Ok(new JsonMessage<IPagedList<EpisodeDto>>()
                    {
                        Success = true,
                        Results = new List<IPagedList<EpisodeDto>> { episodesList }
                    });
                }

                return NotFound(new JsonMessage<IPagedList<EpisodeDto>>()
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

        [HttpGet("ByIdWithCommentsCount/{episodeId}")]
        public async Task<IActionResult> GetEpisodeByIdWithCommentsCount(int episodeId)
        {
            try
            {
                var episodeData = await _unitOfWork.GetRepository<Episode>().GetFirstOrDefaultAsync(
                                                                                                 selector: e => new EpisodeDto { Id = e.Id, Name = e.Name, EpisodeCode = e.EpisodeCode, ReleaseDate = e.ReleaseDate, NumberOfComments = e.EpisodeComments.Count, Created = e.Created, Modified = e.Modified },
                                                                                                 include: source => source.Include(episode => episode.EpisodeComments),
                                                                                                 predicate: e => e.Id == episodeId
                                                                                               );

                if (episodeData is not null)
                {
                    return Ok(new JsonMessage<EpisodeDto>()
                    {
                        Success = true,
                        Results = new List<EpisodeDto> { episodeData }
                    });
                }

                return NotFound(new JsonMessage<EpisodeDto>()
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
