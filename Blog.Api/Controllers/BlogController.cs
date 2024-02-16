using Blog.Api.Data;
using Blog.Api.Models;
using Blog.Api.Services.Interfaces;
using Blog.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Blog.Api.Controllers
{
    [Route("v1")]
    [ApiController]    
    public class BlogController : ControllerBase
    {
        private readonly IDadosBlogService _dadosBlogService;
        public BlogController(IDadosBlogService dadosBlogService)
        {
            _dadosBlogService = dadosBlogService;
        }

        [HttpGet]
        [Route("dados")]
        [Authorize(Roles = "admin, usuario")]
        public async Task<IActionResult> GetAllAsync()
        {
            var dadosBlog = await _dadosBlogService.GetAll();
            return Ok(dadosBlog);
        }

        [HttpGet]
        [Route("dados/{id}")]
        [Authorize(Roles = "admin, usuario")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var dadoBlog = await _dadosBlogService.GetById(id);

            return dadoBlog == null ? NotFound() : Ok(dadoBlog);
        }

        [HttpPost]
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
                    Done = false
                };

                await _dadosBlogService.Post(postBlog);

                return Created(uri: $"v1/dados/{postBlog.Id}", postBlog);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("dados/{id}")]
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
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
