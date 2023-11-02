using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using Ricky.Paladin;

namespace Ricky.Paladin.Ability
{
    internal class PLDAbility_Intervene : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;

        public int Check()
        {
            if (Core.Me.ClassLevel < 74) return -1;
            if (Core.Me.ClassLevel >= 74 && SpellsDefine.Intervene.GetSpell().Cooldown.TotalMilliseconds > 1000) return -1;
            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) <=
                SettingMgr.GetSetting<GeneralSettings>().AttackRange && Core.Me.HasAura(AurasDefine.FightOrFight)) return 1;
            return -1;
        }

        public Spell GetSpell()
        {
            return SpellsDefine.Intervene.GetSpell();
        }

        public void Build(Slot slot)
        {
            slot.Add(GetSpell());
        }
    }
}
