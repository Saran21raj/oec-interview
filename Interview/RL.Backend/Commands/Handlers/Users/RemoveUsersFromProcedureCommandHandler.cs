using MediatR;
using Microsoft.EntityFrameworkCore;
using RL.Backend.Exceptions;
using RL.Backend.Models;
using RL.Data;
using RL.Data.DataModels;

namespace RL.Backend.Commands.Handlers.Users
{
    public class RemoveUsersFromProcedureCommandHandler : IRequestHandler<RemoveUsersFromProcedureCommand, ApiResponse<int>>
    {
        private readonly RLContext _context;

        public RemoveUsersFromProcedureCommandHandler(RLContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse<int>> Handle(RemoveUsersFromProcedureCommand request, CancellationToken cancellationToken)
        {
            try
            {
                    var user = _context.UserMapping.Where(p => p.UserMappingId == request.UserMappingId).FirstOrDefault();

                    _context.UserMapping.Remove(user);

                    await _context.SaveChangesAsync();

                    return ApiResponse<int>.Succeed(request.UserMappingId);
         
         

            }
            catch (Exception e)
            {
                return ApiResponse<int>.Fail(e);
            }
        }
    }
}
