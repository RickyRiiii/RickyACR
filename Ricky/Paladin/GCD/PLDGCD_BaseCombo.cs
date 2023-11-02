using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Ricky.Paladin.GCD
{
    public class PLDGCD_BaseCombo : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.Gcd;

        private Spell GetSpell()
        {
            if (TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) < 2 || Core.Me.ClassLevel < 6 || !Qt.GetQt("AOE"))
            {
                if (Core.Get<IMemApiSpell>().GetComboTimeLeft().TotalMilliseconds < 3000)
                    return SpellsDefine.FastBlade.GetSpell();
                if (Core.Get<IMemApiSpell>().GetLastComboSpellId() == SpellsDefine.FastBlade && Core.Me.ClassLevel >= 4)
                    return SpellsDefine.RiotBlade.GetSpell();
                if (Core.Get<IMemApiSpell>().GetLastComboSpellId() == SpellsDefine.RiotBlade && Core.Me.ClassLevel >= 26)
                    if(Core.Me.ClassLevel >= 60)
                        return SpellsDefine.RoyalAuthority.GetSpell();
                    else
                        return SpellsDefine.RageofHalone.GetSpell();
                return SpellsDefine.FastBlade.GetSpell();
            }
            if (Qt.GetQt("AOE"))
            {
                var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5);
                if (aoeCount >= 2)
                {
                    if (Core.Get<IMemApiSpell>().GetComboTimeLeft().TotalMilliseconds < 3000)
                        return SpellsDefine.TotalEclipse.GetSpell();
                    if (Core.Get<IMemApiSpell>().GetLastComboSpellId() == SpellsDefine.TotalEclipse && Core.Me.ClassLevel >= 40)
                        return SpellsDefine.Prominance.GetSpell();
                    return SpellsDefine.TotalEclipse.GetSpell();
                }
            }
            return null;


        }
        public int Check()
        {
            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
                SettingMgr.GetSetting<GeneralSettings>().AttackRange && TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) < 2) return -1;
            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
                SettingMgr.GetSetting<GeneralSettings>().AttackRange * 2 && TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) >= 2) return -1;
            return 1;
        }

        public void Build(Slot slot)
        {
            var spell = GetSpell();
            if (spell == null) return;
            slot.Add(spell);
        }
    }
}
