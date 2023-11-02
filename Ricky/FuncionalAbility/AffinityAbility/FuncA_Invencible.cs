using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;


namespace Ricky.FuncionalAbility.AffinityAbility
{
    public class FuncA_Invencible
    {
        public Spell GetSpell()
        {
            if (Core.Me.CurrentJob == Jobs.Paladin)
                return SpellsDefine.HallowedGround.GetSpell();
            if (Core.Me.CurrentJob == Jobs.Warrior)
                return SpellsDefine.Holmgang.GetSpell();
            if (Core.Me.CurrentJob == Jobs.DarkKnight)
                return SpellsDefine.LivingDead.GetSpell();
            if (Core.Me.CurrentJob == Jobs.Gunbreaker)
                return SpellsDefine.Superbolide.GetSpell();
            return null;
        }

        public bool IsUsable()
        {
            if (!SpellsDefine.HallowedGround.GetSpell().IsReady() && !SpellsDefine.Holmgang.GetSpell().IsReady() && !SpellsDefine.LivingDead.GetSpell().IsReady() && !SpellsDefine.Superbolide.GetSpell().IsReady()) return false;
            if (Core.Me.CurrentHealthPercent < RickyOptions.Instance.HealthPercentOfInvencible)
            {
                return true;
            }
            return false;
        }
    }
}
