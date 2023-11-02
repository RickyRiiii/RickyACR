using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Ricky.Reaper.GCD
{
    internal class RPRGCD_PlentifulHarvest : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.Gcd;
        public int Check()
        {
            if(Core.Me.HasAura(AurasDefine.ImmortalSacrifice) && SpellsDefine.PlentifulHarvest.IsReady())
                return 1;
            return -1;
        }

        public void Build(Slot slot)
        {
            slot.Add(SpellsDefine.PlentifulHarvest.GetSpell());
        }
    }
}
