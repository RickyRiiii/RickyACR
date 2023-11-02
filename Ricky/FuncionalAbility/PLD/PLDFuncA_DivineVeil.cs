using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;


namespace Ricky.FuncionalAbility.PLD
{
    public class PLDFuncA_DivineVeil
    {
        public Spell GetSpell()
        {
            return SpellsDefine.DivineVeil.GetSpell();
        }

        public bool IsUsable()
        {
            if (!SpellsDefine.DivineVeil.GetSpell().IsReady()) return false;
            if (Core.Me.CurrentHealthPercent < RickyOptions.Instance.HealthPercentOfDivineVeil)
            {
                return true;
            }
            return false;
        }
    }
}
