using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Ricky.Paladin.GCD
{
    public class PLDGCD_Atonement : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.Gcd;

        private Spell GetSpell()
        {
            return SpellsDefine.Atonement.GetSpell();
        }

        public int Check()
        {
            if (TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) >= 2) return -1;
            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
                SettingMgr.GetSetting<GeneralSettings>().AttackRange && TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) < 2) return -1;
            if (Qt.GetQt("存赎罪"))
            {
                    if (Core.Get<IMemApiSpell>().GetLastComboSpellId() != SpellsDefine.RiotBlade && !Core.Me.HasAura(AurasDefine.FightOrFight)) return -1;
            }
            if (Core.Me.HasAura(AurasDefine.FightOrFight))
                if (Core.Me.HasAura(AurasDefine.DivineMight)) return -1;
            if (Core.Me.GetAuraStack(AurasDefine.SwordOath) >= 1) return 1;
            return -1;
        }

        public void Build(Slot slot)
        {
            var spell = GetSpell();
            if (spell == null) return;
            slot.Add(spell);
        }
    }
}
