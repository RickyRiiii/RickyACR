using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using Ricky.FuncionalAbility.AffinityAbility;
using Ricky.Paladin;

namespace Ricky.Paladin.Ability
{
    internal class PLD_AutoReduce : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;
        public int Check()
        {
            if(Core.Me.ClassLevel < 8)
                return -1;
            if (Core.Me.ClassLevel < 21 && Calculate.Instance.DamageReduce() > 0.79f)
                return 1;
            return -1;
        }

        public Spell GetSpell()
        {
            if(Core.Me.ClassLevel < 35)
            {
                //if(new FuncA_ArmsLength().IsUsable())
            }
            
            return null;
        }

        public void Build(Slot slot)
        {
            slot.Add(GetSpell());
        }
    }
}
