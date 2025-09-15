using Backend.Data;
using Backend.DTOs;
using Backend.Entity;
using Backend.Helpers;
using Microsoft.EntityFrameworkCore;

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
                    Content = dto.Content
                };
                await _context.Templates.AddAsync(newTemplate);
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
                    Content = dto.Content
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
                        Content = x.Content
                    }).ToListAsync();
                return Result<List<GetTemplateDto>>.Success(templates);
            }
            catch (Exception ex)
            {
                return Result<List<GetTemplateDto>>.Fail($"{ex.InnerException} {ex.Message}")!;
            }
        }

        public async Task<Result<GetTemplateDto>> GetTemplate(GetTemplateDto dto)
        {
            try
            {
                var template = await _context.Templates
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == dto.Id);

                if (template == null)
                {
                    return Result<GetTemplateDto>.Fail("Template not found")!;
                }
                return Result<GetTemplateDto>.Success(new GetTemplateDto
                {
                    Id = template.Id,
                    Name = template.Name,
                    Content = template.Content
                })!;
            }
            catch (Exception ex)
            {
                return Result<GetTemplateDto>.Fail($"{ex.InnerException} {ex.Message}")!;
            }
        }
    }
}
