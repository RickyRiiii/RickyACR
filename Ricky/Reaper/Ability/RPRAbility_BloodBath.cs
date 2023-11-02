using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using Ricky.Reaper;

namespace Ricky.Reaper.Ability
{
    public class RPRAbility_BloodBath : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;
        public int Check()
        {
            //检查当前血量是否低于预期血量
            if (Core.Me.MaxHealth * (ulong)RPRSettings.Instance.BooldBathPercent / 100 < Core.Me.CurrentHealth) return -1;
            //检查自动浴血开关是否打开
            //if (!Qt.GetQt("自动浴血")) return -1;
            //检查浴血技能是否可用
            if (!SpellsDefine.Bloodbath.IsReady()) return -1;
            return 0;
        }


        public void Build(Slot slot)
        {
            slot.Add(SpellsDefine.Bloodbath.GetSpell());
        }

    }
}