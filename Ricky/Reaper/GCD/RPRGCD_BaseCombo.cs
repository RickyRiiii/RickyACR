using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Ricky.Reaper.GCD
{
    public class RPRGCD_BaseCombo : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.Gcd;

        private Spell GetSpell()
        {
            if (TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) < 3 || Core.Me.ClassLevel < 25)
            {
                if (Core.Get<IMemApiSpell>().GetComboTimeLeft().TotalMilliseconds < 3000)
                    return SpellsDefine.Slice.GetSpell();
                if (Core.Get<IMemApiSpell>().GetLastComboSpellId() == SpellsDefine.Slice && Core.Me.ClassLevel >= 5)
                    return SpellsDefine.WaxingSlice.GetSpell();
                if (Core.Get<IMemApiSpell>().GetLastComboSpellId() == SpellsDefine.WaxingSlice && Core.Me.ClassLevel >= 30)
                    return SpellsDefine.InfernalSlice.GetSpell();
                return SpellsDefine.Slice.GetSpell();
            }
            if (Qt.GetQt("AOE"))
            {
                var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5);
                if(aoeCount >= 3)
                {
                    if (Core.Get<IMemApiSpell>().GetComboTimeLeft().TotalMilliseconds < 3000)
                        return SpellsDefine.SpinningScythe.GetSpell();
                    if (Core.Get<IMemApiSpell>().GetLastComboSpellId() == SpellsDefine.SpinningScythe && Core.Me.ClassLevel >= 45)
                        return SpellsDefine.NightmareScythe.GetSpell();
                    return SpellsDefine.SpinningScythe.GetSpell();
                }
            }
            return null;


        }
        public int Check()
        {
            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
                SettingMgr.GetSetting<GeneralSettings>().AttackRange && TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) < 3) return -1;
            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
                SettingMgr.GetSetting<GeneralSettings>().AttackRange * 2 && TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) >= 3) return -1;
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
