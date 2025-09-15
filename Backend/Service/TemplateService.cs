using Backend.Data;
using Backend.DTOs;
using Backend.Entity;
using Backend.Helpers;
using iText.Html2pdf;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
namespace Backend.Service
{
    public class TemplateService : ITemplateService
    {
        private readonly AppDbContext _context;

        public TemplateService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<bool>> DeleteTemplate(Guid id)
        {
            try
            {
                var template = await _context.Templates
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (template == null)
                    return Result<bool>.Fail("Template not found");

                _context.Templates.Remove(template);
                await _context.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail($"{ex.InnerException} {ex.Message}");
            }
        }

        public async Task<Result<bool>> CreateTemplate(CreateTemplateDto dto)
        {
            try
            {
                var newTemplate = new Template
                {
                    Name = dto.Name,
                    Content = dto.Content,
                    Placeholders = Regex.Matches(dto.Content, @"\*\*(.+?)\*\*")
                        .Select(m => m.Value)
                        .ToList()
                };
                await _context.Templates.AddAsync(newTemplate);
                await _context.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail($"{ex.InnerException} {ex.Message}");
            }

        }
        public async Task<Result<bool>> UpdateTemplate(UpdateTemplateDto dto)
        {
            try
            {
                var oldTemplate = _context.Templates
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Id == dto.Id);

                if (oldTemplate == null)
                    return Result<bool>.Fail("Old template not found");

                var newTemplate = new Template
                {
                    Id = oldTemplate.Id,
                    Name = dto.Name,
                    Content = dto.Content,
                    Placeholders = Regex.Matches(dto.Content, @"\*\*(.+?)\*\*")
                        .Select(m => m.Value)
                        .ToList()
                };

                _context.Templates.Update(newTemplate);
                await _context.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail($"{ex.InnerException} {ex.Message}");
            }
        }

        public async Task<Result<List<GetTemplateDto>>> GetAllTemplates()
        {
            try
            {
                var templates = await _context.Templates
                    .AsNoTracking()
                    .Select(x => new GetTemplateDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Content = x.Content,
                        Placeholders = x.Placeholders

                    }).ToListAsync();
                return Result<List<GetTemplateDto>>.Success(templates);
            }
            catch (Exception ex)
            {
                return Result<List<GetTemplateDto>>.Fail($"{ex.InnerException} {ex.Message}")!;
            }
        }

        public async Task<Result<GetTemplateDto>> GetTemplate(Guid id)
        {
            try
            {
                var template = await _context.Templates
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (template == null)
                {
                    return Result<GetTemplateDto>.Fail("Template not found")!;
                }
                return Result<GetTemplateDto>.Success(new GetTemplateDto
                {
                    Id = template.Id,
                    Name = template.Name,
                    Content = template.Content,
                    Placeholders = template.Placeholders
                })!;
            }
            catch (Exception ex)
            {
                return Result<GetTemplateDto>.Fail($"{ex.InnerException} {ex.Message}")!;
            }
        }

        public async Task<Result<byte[]>> GeneratePdf(GeneratePdfDto dto)
        {
            var templateResult = await GetTemplate(dto.TemplateId);
            if (!templateResult.IsSuccess)
                return Result<byte[]>.Fail("Template not found")!;

            var template = templateResult.Value;

            foreach (KeyValuePair<string,string> placeholder in dto.PlaceholderValues)
            {
                template.Content = template.Content.Replace(placeholder.Key, placeholder.Value);
            }

            byte[] pdfBytes;
            using (var memoryStream = new MemoryStream())
            {
                HtmlConverter.ConvertToPdf(template.Content, memoryStream);
                pdfBytes = memoryStream.ToArray();
            }

            return Result<byte[]>.Success(pdfBytes);
        }
    }   
}
