using MediatR;
using RL.Backend.Models;
namespace RL.Backend.Commands
{
    public class RemoveUsersFromProcedureCommand : IRequest<ApiResponse<int>>
    {
        public int UserMappingId { get; set; }
    }
}
