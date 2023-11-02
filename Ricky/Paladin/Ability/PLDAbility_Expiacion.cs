using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using Ricky.Paladin;

namespace Ricky.Paladin.Ability
{
    internal class PLDAbility_Expiacions : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;

        public int Check()
        {
            if (Core.Me.ClassLevel >= 86 && SpellsDefine.Expiacion.GetSpell().Cooldown.TotalMilliseconds > 1000) return -1;
            if (Core.Me.ClassLevel >=30 && Core.Me.ClassLevel < 86 && SpellsDefine.SpiritsWithin.GetSpell().Cooldown.TotalMilliseconds > 1000) return -1;
            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) <=
                SettingMgr.GetSetting<GeneralSettings>().AttackRange) return 1;
            return -1;
        }

        public Spell GetSpell()
        {
            if (Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.SpiritsWithin.GetSpell().Id) == SpellsDefine.Expiacion)
                return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.SpiritsWithin.GetSpell().Id).GetSpell();
            return SpellsDefine.SpiritsWithin.GetSpell();
        }

        public void Build(Slot slot)
        {
            slot.Add(GetSpell());
        }
    }
}
