using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using Ricky.Reaper;

namespace Ricky.Reaper.Ability
{
    internal class RPRAbility_Enshroud : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;

        public int Check()
        {
            if (Core.Me.ClassLevel < 80)
                return -1;
            if (Core.Get<IMemApiReaper>().ShroudGauge < 50) return -1;
            if (TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) >= 3)
                return 1;
            else
                //绿条50以上同时没有龙爪龙尾预备,且团辅还有50s以上转好
                if (Core.Get<IMemApiReaper>().ShroudGauge >= 50 && !Core.Me.HasAura(AurasDefine.SoulReaver) && SpellsDefine.ArcaneCircle.GetSpell().Cooldown.TotalMilliseconds > 50000)
                    return 1;
            return -1;
        }

        public void Build(Slot slot)
        {
            slot.Add(SpellsDefine.Enshroud.GetSpell());
        }
    }
}
