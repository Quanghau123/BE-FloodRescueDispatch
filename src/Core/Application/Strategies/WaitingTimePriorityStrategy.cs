using Core.Application.Commands.Sos;

namespace Core.Application.Strategies;

public sealed class WaitingTimePriorityStrategy : IPriorityScoreStrategy
{
    public int Calculate(CreateSosCommand command)
    {
        return 0;
    }
}

