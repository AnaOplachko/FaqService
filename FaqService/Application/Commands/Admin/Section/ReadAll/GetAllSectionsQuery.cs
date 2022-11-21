using FaqService.Application.Models;
using MediatR;

namespace FaqService.Application.Commands.Admin.Section.ReadAll;

/// <summary>
/// Запрос на получение всех категорий
/// </summary>
public class GetAllSectionsQuery : IRequest<List<SectionWithSubs>>
{
}