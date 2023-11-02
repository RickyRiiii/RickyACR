using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using Ricky.Reaper;

namespace Ricky.Reaper.Ability
{
    internal class RPRAbility_Soul : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;

        public int Check()
        {
            if (Core.Me.HasAura(AurasDefine.Enshrouded))
                return -1;
            if(Core.Me.HasAura(AurasDefine.SoulReaver))
                return -1;
            if (Core.Get<IMemApiReaper>().SoulGauge == 100)
                return 1;
            if (!Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.DeathsDesign, 5000))
                return -1;
            if (SpellsDefine.Gluttony.GetSpell().Cooldown.TotalMilliseconds > 600 && SpellsDefine.Gluttony.GetSpell().Cooldown.TotalMilliseconds < 12500)
                return -1;
            if (Core.Get<IMemApiReaper>().SoulGauge >= 50 && Core.Get<IMemApiReaper>().ShroudGauge < 80)
                return 1;
            return -1;
        }

        public void Build(Slot slot)
        {
            var spell = GetSpell();
            if (spell == null) return;
            slot.Add(spell);
        }

        public Spell GetSpell()
        {
            if(Core.Me.ClassLevel >= 76)
            {
                if (SpellsDefine.Gluttony.GetSpell().Cooldown.TotalMilliseconds <= 600)
                    return SpellsDefine.Gluttony.GetSpell();
            }
            if (Qt.GetQt("AOE"))
            {
                var aoeCount = TargetHelper.GetEnemyCountInsideSector(Core.Me, Core.Me.GetCurrTarget(), 8, 180);
                if (aoeCount >= 3)
                {
                    return SpellsDefine.GrimSwathe.GetSpell();
                }
            }
            if(Core.Me.HasAura(AurasDefine.EnhancedGallows))
                return SpellsDefine.UnveiledGallows.GetSpell();
            if (Core.Me.HasAura(AurasDefine.EnhancedGibbet))
                return SpellsDefine.UnveiledGibbet.GetSpell();
            return SpellsDefine.BloodStalk.GetSpell();
        }
    }
}
