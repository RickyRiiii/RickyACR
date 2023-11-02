using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using Ricky.FuncionalAbility.AffinityAbility;
using Ricky.Paladin;

namespace Ricky.Paladin.Ability
{
    internal class PLD_AutoTForm : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;

        public int Check()
        {
            if (FuncA_TankForm.Instance.IsUsable())
            {
                return 1;
            }
            return -1;
        }

        public Spell GetSpell()
        {
            return SpellsDefine.IronWill.GetSpell();
        }

        public void Build(Slot slot)
        {
            slot.Add(GetSpell());
        }
    }
}
