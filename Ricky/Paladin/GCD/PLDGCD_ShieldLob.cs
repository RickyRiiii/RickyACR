using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using Ricky.Paladin;

namespace Ricky.Paladin.GCD
{
    internal class PLDGCD_ShieldLob : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.Gcd;

        public int Check()
        {
            if (Qt.GetQt("八人本不投盾"))
            {
                if (PartyHelper.CastableTanks.Count == 2) return -1;
            }
            if (!Core.Get<IMemApiMove>().IsMoving()) return -1;
            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
                SettingMgr.GetSetting<GeneralSettings>().AttackRange+3) return 1;
            return -1;
        }


        public void Build(Slot slot)
        {
            slot.Add(SpellsDefine.ShieldLob.GetSpell());
        }
    }
}
