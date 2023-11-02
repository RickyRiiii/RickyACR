using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using Ricky.Paladin;

namespace Ricky.Paladin.Ability
{
    internal class PLDAbility_CircleOfScorn : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;

        public int Check()
        {
            if (Core.Me.ClassLevel < 50) return -1;
            if (Core.Me.ClassLevel >= 50 && SpellsDefine.CircleofScorn.GetSpell().Cooldown.TotalMilliseconds > 1000) return -1;
            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
                SettingMgr.GetSetting<GeneralSettings>().AttackRange * 2 && TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) >= 2) return -1;
            if(TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5) >= 3) return 1;
            if (Core.Me.GetCurrTarget().IsBoss()) return 1;
            return -1;
        }

        public Spell GetSpell()
        {
            return SpellsDefine.CircleofScorn.GetSpell();
        }

        public void Build(Slot slot)
        {
            slot.Add(GetSpell());
        }
    }
}
