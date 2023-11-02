using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using Ricky.Reaper;

namespace Ricky.Reaper.Ability
{
    internal class RPRAbility_ArcaneCrest : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;

        public int Check()
        {
            if (SpellsDefine.ArcaneCrest.GetSpell().Cooldown.TotalMilliseconds > 600)
            {
                return -1;
            }
            
            if (AI.Instance.GetGCDCooldown() < 600)
            {
                return -2;
            }

            if (!Qt.GetQt("自动白盾"))
            {
                return -3;
            }

            if (!Core.Me.GetCurrTarget().IsNull() && Core.Me.GetCurrTarget().IsCasting)
            {
                if (Core.Me.GetCurrTarget().CastingSpellId.GetSpell().IsBossAoe())
                {
                    if(Core.Me.GetCurrTarget().TotalCastTime - Core.Me.GetCurrTarget().CurrentCastTime < 3.0)
                    {
                        return 1;
                    }      
                }
            }
            return -1;
        }

        public void Build(Slot slot)
        {
            slot.Add(SpellsDefine.ArcaneCrest.GetSpell());
        }
    }
}
