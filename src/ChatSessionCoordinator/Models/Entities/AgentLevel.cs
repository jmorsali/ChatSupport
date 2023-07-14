using ChatSessionCoordinator.Models.Enums;

namespace ChatSessionCoordinator.Models.Entities;

public abstract class AgentLevel
{
    public abstract AgentLevels Level { get; }
    public abstract double SeniorityMultipliers { get; }
}


public sealed class AgentJuniorLevel : AgentLevel
{
    public override AgentLevels Level => AgentLevels.Junior;
    public override double SeniorityMultipliers => 0.4;
}
public sealed class AgentMidLevel : AgentLevel
{
    public override AgentLevels Level => AgentLevels.MidLevel;
    public override double SeniorityMultipliers => 0.6;
}

public sealed class AgentSenior : AgentLevel
{
    public override AgentLevels Level => AgentLevels.Senior;
    public override double SeniorityMultipliers => 0.8;
}
public sealed class AgentTeamLead : AgentLevel
{
    public override AgentLevels Level => AgentLevels.TeamLead;
    public override double SeniorityMultipliers => 0.5;
}
