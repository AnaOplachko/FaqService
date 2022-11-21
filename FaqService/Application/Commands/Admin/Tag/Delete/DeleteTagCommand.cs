using MediatR;

namespace FaqService.Application.Commands.Admin.Tag.Delete;

/// <summary>
/// Команда на удаление тэга
/// </summary>
public class DeleteTagCommand : IRequest
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; init; }
}