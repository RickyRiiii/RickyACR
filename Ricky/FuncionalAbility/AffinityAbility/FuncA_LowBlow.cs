using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Ricky.FuncionalAbility.AffinityAbility
{
    public class FuncA_LowBlow
    {
        public Spell GetSpell()
        {
            return SpellsDefine.LowBlow.GetSpell();
        }

        public bool IsUsable()
        {
            if (!SpellsDefine.LowBlow.GetSpell().IsReady()) return false;
            if (Core.Me.CurrentHealthPercent < RickyOptions.Instance.HealthPercentOfLowBlow)
            {
                return true;
            }
            return false;
        }
    }
}
