using ChatSessionCoordinator.Models.Enums;

namespace ChatSessionCoordinator.Models.Entities;

public class Team
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public List<Agent> Agents { get; } = new();


    public void Remove(Agent agent)
    {
        Agents.Remove(agent);
    }
    public void Add(Agent agent)
    {
        Agents.Add(agent);
    }
}

