using Backend.DTOs;
using Backend.Service;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("API")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        private readonly ITemplateService _service;

        public TemplatesController(ITemplateService service)
        {
            _service = service;
        }

        [HttpGet("templates")]
        public async Task<IActionResult> GetAllTemplates()
        {
            var result = await _service.GetAllTemplates();

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("template/{id}")]
        public async Task<IActionResult> GetTemplate(Guid id)
        {
            var result = await _service.GetTemplate(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost("template")]
        public async Task<IActionResult> CreateTemplate([FromBody] CreateTemplateDto dto)
        {
            var result = await _service.CreateTemplate(dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpPut("template")]
        public async Task<IActionResult> UpdateTemplate([FromBody] UpdateTemplateDto dto)
        {
            var result = await _service.UpdateTemplate(dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpDelete("template/{id}")]
        public async Task<IActionResult> DeleteTemplate(Guid id)
        {
            var result = await _service.DeleteTemplate(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpPost("template/pdf")]
        public async Task<IActionResult> GeneratePdf([FromBody] GeneratePdfDto dto)
        {
            try
            {
                var result = await _service.GeneratePdf(dto);
                if (!result.IsSuccess)
                {
                    return BadRequest(result.Error);
                }
                return File(result.Value, "application/pdf", "template.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500,$"{ex.Message} {ex.InnerException}");
            }
        }
    }
}
