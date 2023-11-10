using RL.Data.DataModels.Common;

namespace RL.Data.DataModels
{
    public class  UserMapping: IChangeTrackable
    {
        public int UserMappingId { get; set; }
        public int UserId { get; set; }
        public int PlanId { get; set; }
        public int ProcedureId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
