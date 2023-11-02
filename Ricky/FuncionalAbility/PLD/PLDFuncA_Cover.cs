using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using Ricky;
using Ricky.Paladin;
using System.ComponentModel;

namespace Ricky.FuncionalAbility.PLD
{
    public class PLDFuncA_Cover
    {
        public static PLDFuncA_Cover Instance = new();
        public Spell GetSpell()
        {
            return new Spell(SpellsDefine.Cover, skillTarget);
        }

        CharacterAgent skillTarget;
        public bool IsUsable()
        {
            if (!SpellsDefine.Cover.GetSpell().IsReady()) return false;
            skillTarget = PartyHelper.CastableAlliesWithin30
                    .Where(Pm => Pm.CurrentHealthPercent < RickyOptions.Instance.HealthPercentOfCover * 0.01)
                    .OrderBy(Pm => Pm.CurrentHealthPercent)
                    .FirstOrDefault();
            if(!skillTarget.IsNull()) return true;
            return false;
        }
    }
}