using Blog.Api.Data;
using Blog.Api.Models;
using Blog.Api.Services;
using Blog.Api.Services.Interfaces;
using Blog.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Api.Controllers
{
    [Route("v1")]
    [ApiController]    
    public class BlogController : ControllerBase
    {
        private readonly IDadosBlogService _dadosBlogService;
        private readonly IHubContext<MessageHub, IMessageHubClient> _messageHub;
        public BlogController(IDadosBlogService dadosBlogService, IHubContext<MessageHub, IMessageHubClient> messageHub)
        {
            _dadosBlogService = dadosBlogService;
            _messageHub = messageHub;
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [Route("dados")]
        [Authorize(Roles = "admin, usuario")]
        public async Task<IActionResult> GetAllAsync()
        {
            var dadosBlog = await _dadosBlogService.GetAll();
            return Ok(dadosBlog);
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [Route("dados/{id}")]
        [Authorize(Roles = "admin, usuario")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var dadoBlog = await _dadosBlogService.GetById(id);

            return dadoBlog == null ? NotFound() : Ok(dadoBlog);
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [Route("dados")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PostAsync([FromBody] CreatePostViewModel post)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var postBlog = new DadosBlog
                {
                    NomeEditor = post.NomeEditor,
                    Texto = post.Texto,
                    Done = true
                };

                await _dadosBlogService.Post(postBlog);

                await _messageHub.Clients.All.SendMessageToHub($"Nome Editor: {postBlog.NomeEditor}, Texto: {postBlog.Texto}");

                return Created(uri: $"v1/dados/{postBlog.Id}", postBlog);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("dados/{id}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutAsync(
            [FromBody] CreatePostViewModel model,
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var dadosBlog = await _dadosBlogService.GetById(id);

            if (dadosBlog == null)
                return NotFound();

            try
            {
                dadosBlog.NomeEditor = model.NomeEditor;
                dadosBlog.Texto = model.Texto;

                await _dadosBlogService.Put(dadosBlog);

                return Ok(dadosBlog);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpDelete("dados/{id}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var dadosBlog = await _dadosBlogService.GetById(id);

            try
            {
                await _dadosBlogService.Delete(dadosBlog);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
