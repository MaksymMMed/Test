using Backend.DTOs;
using Backend.Helpers;

namespace Backend.Service
{
    public interface ITemplateService
    {
        Task<Result<bool>> CreateTemplate(CreateTemplateDto dto);
        Task<Result<bool>> DeleteTemplate(Guid id);
        Task<Result<List<GetTemplateDto>>> GetAllTemplates();
        Task<Result<GetTemplateDto>> GetTemplate(GetTemplateDto dto);
        Task<Result<bool>> UpdateTemplate(UpdateTemplateDto dto);
    }
}