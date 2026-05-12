using Core.Application.Commands.Sos;

namespace Core.Application.Strategies;

public sealed class FloodSeverityPriorityStrategy : IPriorityScoreStrategy
{
    public int Calculate(CreateSosCommand command)
    {
        return 20;
    }
}

