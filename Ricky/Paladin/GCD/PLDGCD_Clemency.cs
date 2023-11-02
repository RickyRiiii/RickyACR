using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Ricky.Paladin.GCD
{
    public class PLDGCD_Clemency : ISlotResolver
    {
        private static int Pm = 4;
        public SlotMode SlotMode { get; } = SlotMode.Gcd;

        void ISlotResolver.Build(Slot slot)
        {
            //slot中添加深厚，目标为Pm对应的目标
            slot.Add(new Spell(SpellsDefine.Clemency, skillTarget));
        }

        private static CharacterAgent skillTarget;
        int ISlotResolver.Check()
        {
            //如果有安魂
            if (Core.Me.HasAura(AurasDefine.Requiescat))
                //血量小于安魂深厚阈值
                skillTarget = PartyHelper.CastableAlliesWithin30
                    .Where(Pm => Pm.CurrentHealthPercent < PLDSettings.Instance.RCClemencyPercent * 0.01)
                    .OrderBy(Pm => Pm.CurrentHealthPercent)
                    .FirstOrDefault();
            //没有安魂
            if (!Core.Me.HasAura(AurasDefine.Requiescat))
                //血量小于读条深厚阈值
                skillTarget = PartyHelper.CastableAlliesWithin30
                    .Where(Pm => Pm.CurrentHealthPercent < PLDSettings.Instance.ClemencyPercent * 0.01)
                    .OrderBy(Pm => Pm.CurrentHealthPercent)
                    .FirstOrDefault();
            if(!skillTarget.IsNull()) return 1;
            return -1;
        }
    }
}
