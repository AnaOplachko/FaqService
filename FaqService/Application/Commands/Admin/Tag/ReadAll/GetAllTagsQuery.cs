using FaqService.Application.Models;
using MediatR;

namespace FaqService.Application.Commands.Admin.Tag.ReadAll;

/// <summary>
/// Запрос на получение всех тэгов
/// </summary>
public class GetAllTagsQuery : IRequest<List<TagModel>>
{
}