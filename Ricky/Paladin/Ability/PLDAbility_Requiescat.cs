using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using Ricky.Paladin;

namespace Ricky.Paladin.Ability
{
    internal class PLDAbility_Requiescat : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;

        public int Check()
        {
            if (Qt.GetQt("没有战逃不打安魂"))
            {
                if (!Core.Me.HasAura(AurasDefine.FightOrFight)) return -1;
            }
            if (SpellsDefine.Requiescat.GetSpell().Cooldown.TotalMilliseconds > 1200) return -1;
            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) <=
                SettingMgr.GetSetting<GeneralSettings>().AttackRange) return 1;
            return -1;
        }


        public void Build(Slot slot)
        {
            slot.Add(SpellsDefine.Requiescat.GetSpell());
        }
    }
}
