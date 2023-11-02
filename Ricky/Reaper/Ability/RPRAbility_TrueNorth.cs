using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using Ricky.Reaper;

namespace Ricky.Reaper.Ability
{
    public class RPRAbility_TrueNorth : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;

        public int Check()
        {
            if (AI.Instance.GetGCDCooldown() < 600)
            {
                return -2;
            }

            if (!Qt.GetQt("自动真北"))
            {
                return -10;
            }

            if (!SpellsDefine.TrueNorth.IsReady())
            {
                return -9;
            }

            if (Core.Me.HasMyAura(AurasDefine.TrueNorth) || SpellsDefine.TrueNorth.RecentlyUsed())
            {
                return -8;
            }

            bool flag = Core.Get<IMemApiSpellCastSucces>().LastGcdSuccesTime + 2000 - TimeHelper.Now() < 2000;
            if (Core.Me.GetCurrTarget().HasPositional() && flag)
            {
                if (Core.Me.HasAura(AurasDefine.EnhancedGallows) && Core.Me.HasAura(2587) && !Core.Me.GetCurrTarget().IsBehind)
                {
                    return 1;
                }

                if (Core.Me.HasAura(AurasDefine.EnhancedGibbet) && Core.Me.HasAura(2587) && !Core.Me.GetCurrTarget().IsFlanking)
                {
                    return 1;
                }
            }

            return -1;
        }

        public void Build(Slot slot)
        {
            slot.Add(SpellsDefine.TrueNorth.GetSpell());
        }
    }
}