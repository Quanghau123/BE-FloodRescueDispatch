using Core.Application.Commands.Sos;

namespace Core.Application.Strategies;

public interface IPriorityScoreStrategy
{
    int Calculate(CreateSosCommand command);
}

