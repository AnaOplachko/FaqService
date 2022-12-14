using FaqService.Application.Dtos;
using MediatR;

namespace FaqService.Application.Commands.User.Section.ReadAll;

/// <summary>
/// Запрос на получение пользователем всех категорий
/// </summary>
public class GetAllSectionsQuery : IRequest<List<SectionWithSubs>>
{
}