using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using Ricky.FuncionalAbility.AffinityAbility;
using Ricky.FuncionalAbility.PLD;
using Ricky.Paladin;

namespace Ricky.Paladin.Ability
{
    internal class PLD_AutoCover : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;

        public int Check()
        {
            if (!Qt.GetQt("自动保护")) return -1;
            if (PLDFuncA_Cover.Instance.IsUsable())
            {
                return 1;
            }
            return -1;
        }

        public Spell GetSpell()
        {
            return PLDFuncA_Cover.Instance.GetSpell();
        }

        public void Build(Slot slot)
        {
            slot.Add(GetSpell());
        }
    }
}
