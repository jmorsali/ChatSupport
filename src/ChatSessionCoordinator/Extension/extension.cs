using ChatSessionCoordinator.AgentPool;
using ChatSessionCoordinator.Models.Entities;
using ChatSessionCoordinator.Models.Enums;

namespace ChatSessionCoordinator.Extension
{
    public static class Extension
    {
        public static bool IsActive(this AgentShifts? shift)
        {
            if (shift == null) return false;
            return shift switch
            {
                AgentShifts.From0_To8 => DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 8,
                AgentShifts.From8_To16 => DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 16,
                AgentShifts.From16_To24 => DateTime.Now.Hour >= 16 && DateTime.Now.Hour < 24,
                _ => throw new ArgumentOutOfRangeException(nameof(shift), shift, null)
            };
        }

        public static AgentShifts CurrentShift(this DateTime now)
        {
            return now.Hour switch
            {
                >= 0 and < 8 => AgentShifts.From0_To8,
                >= 8 and < 16 => AgentShifts.From8_To16,
                >= 16 and < 24 => AgentShifts.From16_To24,
                _ => throw new ArgumentOutOfRangeException(nameof(now), now, null)
            };
        }

       

        
        public static bool IsWorkingHour(this DateTime now)
        {
            return now.Hour is >= 8 and < 16;
        }
    }
}
