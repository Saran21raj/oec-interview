using MediatR;
using Microsoft.EntityFrameworkCore;
using RL.Backend.Exceptions;
using RL.Backend.Models;
using RL.Data;
using RL.Data.DataModels;

namespace RL.Backend.Commands.Handlers.Users
{
    public class AddUsersToProcedureCommandHandler : IRequestHandler<AddUsersToProcedureCommand, ApiResponse<int>>
    {
        private readonly RLContext _context;

        public AddUsersToProcedureCommandHandler(RLContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse<int>> Handle(AddUsersToProcedureCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var users = _context.UserMapping;

                if (users.Count() >= 1)
                {
                    int max = 0;
                   foreach(var u in users)
                    {
                        if (u.UserMappingId > max)
                        {
                            max = u.UserMappingId;
                        }
                    }
                    _context.UserMapping.Add(new UserMapping
                    {
                        UserMappingId = max+1,
                        PlanId = request.PlanId,
                        ProcedureId = request.ProcedureId,
                        UserId = request.UserId,
                        CreateDate = DateTime.Now.ToUniversalTime(),
                        UpdateDate = DateTime.Now.ToUniversalTime()
                    });
                    await _context.SaveChangesAsync();

                    return ApiResponse<int>.Succeed(max+1);
                }
                else
                {
                    _context.UserMapping.Add(new UserMapping
                    {
                        UserMappingId = 1,
                        PlanId = request.PlanId,
                        ProcedureId = request.ProcedureId,
                        UserId = request.UserId,
                        CreateDate = DateTime.Now.ToUniversalTime(),
                        UpdateDate = DateTime.Now.ToUniversalTime()
                    });



                    await _context.SaveChangesAsync();

                    return ApiResponse<int>.Succeed(1);
                }


             }
            catch (Exception e)
            {
                return ApiResponse<int>.Fail(e);
            }
        }
    }
}
