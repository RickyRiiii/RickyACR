using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;


namespace Ricky.FuncionalAbility.PLD
{
    public class PLDFuncA_Bulwark
    {
        public Spell GetSpell()
        {
            return SpellsDefine.Bulwark.GetSpell();
        }

        public bool IsUsable()
        {
            if (!SpellsDefine.Bulwark.GetSpell().IsReady()) return false;
            if (Core.Me.CurrentHealthPercent < RickyOptions.Instance.HealthPercentOfBulwark)
            {
                return true;
            }
            return false;
        }
    }
}
