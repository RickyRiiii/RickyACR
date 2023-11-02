using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using Ricky.Paladin;

namespace Ricky.Paladin.Ability
{
    internal class PLDAbility_GoringBlade : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.Gcd;

        public int Check()
        {
            if (Core.Me.ClassLevel < 54) return -1;
            if (Core.Me.ClassLevel >= 54 && SpellsDefine.GoringBlade.GetSpell().Cooldown.TotalMilliseconds > 1000) return -1;
            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) > SettingMgr.GetSetting<GeneralSettings>().AttackRange) return -1;
            if(!Core.Me.HasAura(AurasDefine.FightOrFight)) return -1;
            if (TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) > 2) return -1;
            if (Core.Me.GetCurrTarget().IsBoss()) return 1;
            return 1;
        }

        public Spell GetSpell()
        {
            return SpellsDefine.GoringBlade.GetSpell();
        }

        public void Build(Slot slot)
        {
            slot.Add(GetSpell());
        }
    }
}
