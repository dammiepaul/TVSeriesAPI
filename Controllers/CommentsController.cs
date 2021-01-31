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
    public class CommentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet("sortedByCreatedDateDesc/{pageIndex?}/{pageSize?}")]
        public async Task<IActionResult> GetAllCommentsSortedByCreatedDateDesc(int pageIndex = 0, int pageSize = 100)
        {
            try
            {
                var commentList = await _unitOfWork.GetRepository<Comment>().GetPagedListAsync(
                                                                                                 selector: c => new CommentDto { Id = c.Id, CommentText = c.CommentText, IpAddressLocation = c.IpAddressLocation, Created = c.Created, Modified = c.Modified, EpisodeId = c.EpisodeId, EpisodeName = c.Episode.Name },
                                                                                                 include: source => source.Include(comment => comment.Episode),
                                                                                                 orderBy: source => source.OrderByDescending(c => c.Created),
                                                                                                 pageIndex: pageIndex,
                                                                                                 pageSize: pageSize
                                                                                               );

                if (commentList.Items.Any())
                {
                    return Ok(new JsonMessage<IPagedList<CommentDto>>()
                    {
                        Success = true,
                        Results = new List<IPagedList<CommentDto>> { commentList }
                    });
                }

                return NotFound(new JsonMessage<IPagedList<CommentDto>>()
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

        [HttpPost("AddToEpisode/{episodeId}")]
        public async Task<IActionResult> AddNewCommentToAnEpisode(int episodeId, CommentForCreationDto newComment)
        {
            try
            {
                var episodeData = await _unitOfWork.GetRepository<Episode>().GetFirstOrDefaultAsync(
                                                                                                 selector: e => new EpisodeDto { Id = e.Id, Name = e.Name, EpisodeCode = e.EpisodeCode, ReleaseDate = e.ReleaseDate, NumberOfComments = e.EpisodeComments.Count, Created = e.Created, Modified = e.Modified },
                                                                                                 include: source => source.Include(episode => episode.EpisodeComments),
                                                                                                 predicate: e => e.Id == episodeId
                                                                                               );

                if (episodeData is null)
                {
                    return NotFound(new JsonMessage<EpisodeDto>()
                    {
                        Success = false,
                        ErrorMessage = "Episode record not found"
                    });
                }


                var commentEntity = new Comment { EpisodeId = episodeId, IpAddressLocation = newComment.IpAddressLocation, CommentText = newComment.CommentText, Created = DateTime.Now, Modified = DateTime.Now };

                var commentRepo = _unitOfWork.GetRepository<Comment>();
                commentRepo.Insert(commentEntity);
                await _unitOfWork.SaveChangesAsync();

                return Ok(new JsonMessage<bool>()
                {
                    Success = true
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
    }
}
