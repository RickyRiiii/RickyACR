using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Ricky.FuncionalAbility.PLD
{
    public class PLDFuncA_PassageOfArms
    {
        public Spell GetSpell()
        {
            return SpellsDefine.PassageOfArms.GetSpell();
        }

        /*public bool IsUsable()
        {
            if (!SpellsDefine.PassageOfArms.GetSpell().IsReady()) return false;
            if (Core.Me.CurrentHealthPercent < RickyOptions.Instance.HealthPercentOfPassageOfArms)
            {
                return true;
            }
            return false;
        }*/
    }
}
