using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Ricky.FuncionalAbility.AffinityAbility
{
    public class FuncA_TankForm
    {
        public static FuncA_TankForm Instance = new();
        public Spell GetSpell()
        {
            if (Core.Me.CurrentJob == Jobs.Paladin)
                return SpellsDefine.IronWill.GetSpell();
            if (Core.Me.CurrentJob == Jobs.Warrior)
                return SpellsDefine.Defiance.GetSpell();
            if (Core.Me.CurrentJob == Jobs.DarkKnight)
                return SpellsDefine.Grit.GetSpell();
            if (Core.Me.CurrentJob == Jobs.Gunbreaker)
                return SpellsDefine.RoyalGuard.GetSpell();
            return null;
        }

        public bool IsUsable()
        {
            if (!SpellsDefine.IronWill.GetSpell().IsReady() && !SpellsDefine.Defiance.GetSpell().IsReady() && !SpellsDefine.Grit.GetSpell().IsReady() && !SpellsDefine.RoyalGuard.GetSpell().IsReady()) return false;
            if (PartyHelper.CastableTanks.Count == 1 && !Core.Me.HasAura(AurasDefine.IronWill) && !Core.Me.HasAura(AurasDefine.Defiance) && !Core.Me.HasAura(AurasDefine.Grit) && !Core.Me.HasAura(AurasDefine.RoyalGuard))
            {
                return true;
            }
            return false;
        }
    }
}
