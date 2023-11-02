using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Ricky.FuncionalAbility.AffinityAbility
{
    public class FuncA_30Reduce
    {
        public Spell GetSpell()
        {
            if (Core.Me.CurrentJob == Jobs.Paladin)
                return SpellsDefine.Sentinel.GetSpell();
            if (Core.Me.CurrentJob == Jobs.Warrior)
                return SpellsDefine.Vengeance.GetSpell();
            if (Core.Me.CurrentJob == Jobs.DarkKnight)
                return SpellsDefine.ShadowWall.GetSpell();
            if (Core.Me.CurrentJob == Jobs.Gunbreaker)
                return SpellsDefine.Nebula.GetSpell();
            return null;
        }

        public bool IsUsable()
        {
            if (!SpellsDefine.Sentinel.GetSpell().IsReady() && !SpellsDefine.Vengeance.GetSpell().IsReady() && !SpellsDefine.ShadowWall.GetSpell().IsReady() && !SpellsDefine.Nebula.GetSpell().IsReady()) return false;
            if (Core.Me.CurrentHealthPercent < RickyOptions.Instance.HealthPercentOf30Reduce)
            {
                return true;
            }
            return false;
        }
    }
}
