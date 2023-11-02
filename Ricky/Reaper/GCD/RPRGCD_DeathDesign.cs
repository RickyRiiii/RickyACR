using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;


namespace Ricky.Reaper.GCD
{
    internal class RPRGCD_DeathDesign : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.Gcd;
        
        private Spell GetSpell()
        {
            if (TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) < 3 || Core.Me.ClassLevel < 35)
                return SpellsDefine.ShadowOfDeath.GetSpell();
            if (Qt.GetQt("AOE"))
            {
                var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5);
                if (aoeCount >= 3) 
                    return SpellsDefine.WhorlOfDeath.GetSpell();
            }
            
            return null;
        }

        public int Check()
        {
            if (!Qt.GetQt("续烙印")) return -1;
            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
                SettingMgr.GetSetting<GeneralSettings>().AttackRange) return -1;
            if (Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.DeathsDesign, 3000)) return -1;
            return 1;
        }

        public void Build(Slot slot)
        {
            slot.Add(GetSpell());
        }
    }
}
