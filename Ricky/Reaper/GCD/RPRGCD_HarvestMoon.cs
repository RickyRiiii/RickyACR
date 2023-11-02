using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Ricky.Reaper.GCD
{
    internal class RPRGCD_HarvestMoon : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.Gcd;
        public int Check()
        {
            if (Core.Me.ClassLevel < 82)
                return -1;
            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget())>
                    SettingMgr.GetSetting<GeneralSettings>().AttackRange*2)
                return 1;
            else 
                return -1;
        }

        public Spell GetSpell()
        {
            if (Core.Me.HasAura(AurasDefine.Soulsow))
                return SpellsDefine.HarvestMoon.GetSpell();
            else if(TargetHelper.GetNearbyEnemyCount(Core.Me,50,50) == 0)
                return SpellsDefine.Soulsow.GetSpell();
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
