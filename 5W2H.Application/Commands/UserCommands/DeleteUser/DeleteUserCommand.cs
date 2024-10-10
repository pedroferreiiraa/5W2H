using _5W2H.Application.Models;
using _5W2H.Core.Entities;
using MediatR;

namespace _5W2H.Application.Commands.UserCommands.DeleteUser;

public class DeleteUserCommand : IRequest<ResultViewModel<int>>
{
    public int Id { get; set; } 
}