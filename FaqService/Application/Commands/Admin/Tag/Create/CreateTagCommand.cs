using MediatR;

namespace FaqService.Application.Commands.Admin.Tag.Create;

/// <summary>
/// Команда на создание тэга
/// </summary>
public class CreateTagCommand : IRequest<Dtos.Tag>
{
    /// <summary>
    /// Название тэга
    /// </summary>
    public string Name { get; init; } = null!;
}