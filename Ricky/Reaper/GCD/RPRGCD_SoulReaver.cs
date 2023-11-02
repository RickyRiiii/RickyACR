using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Ricky.Reaper.GCD
{
    internal class RPRGCD_SoulReaver : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.Gcd;
        public int Check()
        {
            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
                SettingMgr.GetSetting<GeneralSettings>().AttackRange && TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) < 3) return -1;
            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
                SettingMgr.GetSetting<GeneralSettings>().AttackRange*2 && TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) >= 3) return -1;
            if (Core.Me.HasAura(AurasDefine.SoulReaver))
                return 1;
            else
                return -1;
        }


        public void Build(Slot slot)
        {
            var spell = RPRSpellHelper.GetSoulGCDSpell();
            if (spell == null) return;
            slot.Add(spell);
        }

    }
}
