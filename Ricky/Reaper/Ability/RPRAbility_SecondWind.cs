using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using Ricky.Reaper;

namespace Ricky.Reaper.Ability
{
    public class RPRAbility_SecondWind : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;
        public int Check()
        {
            //检测自身血量是否低于设定血量
            if (Core.Me.MaxHealth * (ulong)RPRSettings.Instance.SecondWindPercent / 100 < Core.Me.CurrentHealth) return -1;
            //检查自动内丹开关是否打开
            //if (!Qt.GetQt("自动内丹")) return -1;
            //检查内丹技能是否可用
            if (!SpellsDefine.SecondWind.IsReady()) return -1;
            return 0;
        }


        public void Build(Slot slot)
        {
            slot.Add(SpellsDefine.SecondWind.GetSpell());
        }

    }
}