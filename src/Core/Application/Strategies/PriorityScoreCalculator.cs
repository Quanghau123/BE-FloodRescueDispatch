using Core.Application.Commands.Sos;

namespace Core.Application.Strategies;

public sealed class PriorityScoreCalculator
{
    private readonly IEnumerable<IPriorityScoreStrategy> _strategies;

    public PriorityScoreCalculator(IEnumerable<IPriorityScoreStrategy> strategies)
    {
        _strategies = strategies;
    }

    public int Calculate(CreateSosCommand command)
    {
        return _strategies.Sum(x => x.Calculate(command));
    }
}

