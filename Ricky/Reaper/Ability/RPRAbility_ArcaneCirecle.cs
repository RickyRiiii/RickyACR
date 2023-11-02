using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using Ricky.Reaper;

namespace Ricky.Reaper.Ability
{
    internal class RPRAbility_ArcaneCircle : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;

        public int Check()
        {
            if (Core.Me.ClassLevel < 72) return -1;
            if (SpellsDefine.ArcaneCircle.GetSpell().Cooldown.TotalMilliseconds > 1000)
                return -1;
            if (TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) >= 3)
                if (Qt.GetQt("途中爆发"))
                    return 1;
                else
                    return -1;
            if(Core.Me.GetCurrTarget().CurrentHealthPercent < 0.25 && !Core.Me.GetCurrTarget().IsBoss())
                return -1;
            return 1;
        }

        public void Build(Slot slot)
        {
            slot.Add(SpellsDefine.ArcaneCircle.GetSpell());
        }
    }
}
