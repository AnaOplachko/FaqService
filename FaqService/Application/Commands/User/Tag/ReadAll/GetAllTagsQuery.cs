using FaqService.Application.Models;
using MediatR;

namespace FaqService.Application.Commands.User.Tag.ReadAll;

/// <summary>
/// Запрос на получение пользователем всех тэгов
/// </summary>
public class GetAllTagsQuery : IRequest<List<TagModel>>
{
}