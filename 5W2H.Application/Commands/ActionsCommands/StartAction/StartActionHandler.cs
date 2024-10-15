using _5W2H.Application.Models;
using _5W2H.Core.Repositories;
using MediatR;
using Action = _5W2H.Core.Entities.Action;

namespace _5W2H.Application.Commands.ActionsCommands.StartAction;

public class StartActionHandler : IRequestHandler<StartActionCommand, ResultViewModel<Action>>
{
    private readonly IActionRepository _actionRepository;

    public StartActionHandler(IActionRepository actionRepository)
    {
        _actionRepository = actionRepository;
    }
    
    public async Task<ResultViewModel<Action>> Handle(StartActionCommand request, CancellationToken cancellationToken)
    {
        var existingAction = await _actionRepository.GetByIdAsync(request.Id);

        if (existingAction == null)
        {
            
            return ResultViewModel<Action>.Error("Projeto não encontrado.");
        }
        
        existingAction.Start();

        await _actionRepository.SaveChangesAsync();

        return ResultViewModel<Action>.Success(existingAction);
    }
}