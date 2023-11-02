using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Ricky.Reaper.GCD
{
    internal class RPRGCD_SoulSlice : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.Gcd;
        public int Check()
        {
            //未习得
            if (Core.Me.ClassLevel < 60) return -1;
            if (!SpellsDefine.SoulSlice.IsReady()) return -1;
            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
                SettingMgr.GetSetting<GeneralSettings>().AttackRange && TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) < 3) return -1;
            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
                SettingMgr.GetSetting<GeneralSettings>().AttackRange * 2 && TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) >= 3) return -1;
            if (Core.Get<IMemApiReaper>().SoulGauge <= 50)
                return 1;
            return -1;
        }

        private Spell GetSpell()
        {
            if (TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) < 3 || Core.Me.ClassLevel < 65)
                return SpellsDefine.SoulSlice.GetSpell();
            if (Qt.GetQt("AOE"))
            {
                var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5);
                if (aoeCount >= 3)
                {
                    return SpellsDefine.SoulScythe.GetSpell();
                }
            }
            return null;
        }

        public void Build(Slot slot)
        {
            var spell = GetSpell();
            if (spell == null) return;
            slot.Add(spell);
        }
    }
}
