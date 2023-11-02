using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Ricky.FuncionalAbility.AffinityAbility
{
    public class FuncA_ArmsLength
    {
        public Spell GetSpell()
        {
            return SpellsDefine.ArmsLength.GetSpell();
        }

        public bool IsUsable()
        {
            if (!SpellsDefine.ArmsLength.GetSpell().IsReady()) return false;
            if (Core.Me.CurrentHealthPercent < RickyOptions.Instance.HealthPercentOfArmsLength)
            {
                return true;
            }
            return false;
        }
    }
}
