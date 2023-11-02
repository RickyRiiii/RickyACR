using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using Ricky.Paladin;

namespace Ricky.Paladin.Ability
{
    internal class PLDAbility_FightOrFlight : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;

        public int Check()
        {
            if (SpellsDefine.FightorFlight.GetSpell().Cooldown.TotalMilliseconds > 1200) return -1;
            if (Qt.GetQt("远离不打战逃"))
            {
                if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
                SettingMgr.GetSetting<GeneralSettings>().AttackRange + 1) return -1;
            }
            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) <=
                SettingMgr.GetSetting<GeneralSettings>().AttackRange + 1) return 1;
            return -1;
        }

        
        public void Build(Slot slot)
        {
            slot.Add(SpellsDefine.FightorFlight.GetSpell()) ;
        }
    }
}
