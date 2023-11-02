using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Ricky.FuncionalAbility.PLD
{
    public class PLDFuncA_Sheltron
    {
        public Spell GetSpell()
        {
            return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.Sheltron.GetSpell().Id).GetSpell();
        }

        public bool IsUsable()
        {
            if (!SpellsDefine.Sheltron.GetSpell().IsReady()) return false;
            if (Core.Me.CurrentHealthPercent < RickyOptions.Instance.HealthPercentOfSheltron)
            {
                return true;
            }
            return false;
        }
    }
}
