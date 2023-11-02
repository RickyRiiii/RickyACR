using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Ricky.FuncionalAbility.AffinityAbility
{
    public class FuncA_Reprisal
    {
        public Spell GetSpell()
        {
            return SpellsDefine.Reprisal.GetSpell();
        }

        public bool IsUsable()
        {
            if (!SpellsDefine.Reprisal.GetSpell().IsReady()) return false;
            if (Core.Me.CurrentHealthPercent < RickyOptions.Instance.HealthPercentOfReprisal)
            {
                return true;
            }
            return false;
        }
    }
}
