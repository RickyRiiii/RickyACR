using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using Ricky.Reaper;

namespace Ricky.Reaper.Ability
{
    internal class RPRAbility_Enshrouded : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;

        public int Check()
        {
            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
                SettingMgr.GetSetting<GeneralSettings>().AttackRange && TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) < 3) return -1;
            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
                SettingMgr.GetSetting<GeneralSettings>().AttackRange * 2 && TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) >= 3) return -1;
            if (Core.Get<IMemApiReaper>().VoidShroud >= 2)
                return 1;
            return -1;
        }

        private Spell GetSpell()
        {
            if (Qt.GetQt("AOE"))
            {
                var aoeCount = TargetHelper.GetEnemyCountInsideSector(Core.Me, Core.Me.GetCurrTarget(), 8, 180);
                if (aoeCount >= 3)
                {
                    return SpellsDefine.LemuresScythe.GetSpell();
                }
            }
            if(TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) < 3)
                return SpellsDefine.LemuresSlice.GetSpell();
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
