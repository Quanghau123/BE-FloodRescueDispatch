using Core.Application.Commands.Sos;

namespace Core.Application.Strategies;

public sealed class HumanRiskPriorityStrategy : IPriorityScoreStrategy
{
    public int Calculate(CreateSosCommand command)
    {
        var score = 0;

        score += Math.Min(command.PeopleCount, 20) * 2;

        if (command.HasInjuredPeople)
            score += 40;

        if (command.HasChildren)
            score += 20;

        if (command.HasElderly)
            score += 20;

        return score;
    }
}

