using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Ricky.Paladin.GCD
{
    public class PLDGCD_HolyMagic: ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.Gcd;

        private Spell GetSpell()
        {
            if (TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) < 2 || Core.Me.ClassLevel < 72 || !Qt.GetQt("AOE"))
            {
                return SpellsDefine.HolySpirit.GetSpell();
            }
            if (Qt.GetQt("AOE"))
            {
                var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5);
                if (aoeCount >= 2)
                {
                    return SpellsDefine.HolyCircle.GetSpell();
                }
            }
            return null;
        }

        public int Check()
        {
            if (Core.Me.ClassLevel < 64) return -1;
            if(Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
                SettingMgr.GetSetting<GeneralSettings>().AttackRange + 3 && Qt.GetQt("远离圣灵"))
            {
                if (Core.Me.HasAura(AurasDefine.DivineMight)) return 1;
                else if (!Core.Get<IMemApiMove>().IsMoving()) return 1;
                return -1;
            }
            if (TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) < 2) 
                if(Core.Me.HasAura(AurasDefine.SwordOath))
                    return -1;
            if (Core.Me.HasAura(AurasDefine.DivineMight)) return 1;
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
