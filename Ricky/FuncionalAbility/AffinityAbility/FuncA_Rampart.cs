using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;


namespace Ricky.FuncionalAbility.AffinityAbility
{
    public class FuncA_Rampart
    {
        public Spell GetSpell()
        {
            return SpellsDefine.Rampart.GetSpell();
        }

        public bool IsUsable()
        {
            if (!SpellsDefine.Rampart.GetSpell().IsReady()) return false;
            if (Core.Me.CurrentHealthPercent < RickyOptions.Instance.HealthPercentOfRampart)
            {
                return true;
            }
            return false;
        }
    }
}
