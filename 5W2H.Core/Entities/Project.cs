// File: _5W2H.Core/Entities/Project.cs

using System.Text.Json.Serialization;
using _5W2H.Core.Enums;

namespace _5W2H.Core.Entities;

public class Project : BaseEntity
{
    public Project(string title, int projectNumber, int userId, ProjectStatusEnum status, string originDate, string description)
    {
        Title = title;
        ProjectNumber = projectNumber;
        UserId = userId;
        Status = status;
        OriginDate = originDate;
        Actions = new List<Acao>();
        Description = description;
    }

    public string Title { get; private set; }
    public int ProjectNumber { get; private set; }
    public ProjectStatusEnum Status { get; private set; }
    public int UserId { get; private set; }
    public string OriginDate { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    
    public string Description { get; private set; }
    
    
    public virtual List<Acao> Actions { get; set; } // Lista privada de ações.
    
    public IEnumerable<int> ActionIds => Actions.Select(a => a.Id);

    public void AddAction(Acao acao)
    {
        if (acao == null)
            throw new ArgumentNullException(nameof(acao));

        // Atualize o ProjectId da ação
        acao.SetProjectId(this.Id);

        if (!Actions.Contains(acao))
        {
            Actions.Add(acao);
        }
    }

    public void RemoveAction(Acao acao)
    {
        Actions.Remove(acao);
    }
    
    public void Cancel()
    {
        if (Status == ProjectStatusEnum.InProgress || Status == ProjectStatusEnum.Suspended)
            Status = ProjectStatusEnum.Cancelled;
    }

    public void Start()
    {
        if (Status == ProjectStatusEnum.Created)
        {
            Status = ProjectStatusEnum.InProgress;
            StartedAt = DateTime.Now;
        }
    }

    public void Complete()
    {
        if (Status == ProjectStatusEnum.InProgress)
        {
            Status = ProjectStatusEnum.Completed;
            CompletedAt = DateTime.Now;
        }
    }

    public void Update(string title, int projectNumber, string originDate)
    {
        Title = title;
        ProjectNumber = projectNumber;
        OriginDate = originDate;
    }
    
}