using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Ricky.Reaper.GCD
{
    internal class RPRGCD_Harpe : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.Gcd;
        public int Check()
        {
            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
                    SettingMgr.GetSetting<GeneralSettings>().AttackRange*2) 
            {
                if (Qt.GetQt("AOE"))
                {
                    var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 10, 10);
                    if (aoeCount >= 3)
                        return -1;
                }
                if (Core.Get<IMemApiMove>().IsMoving() && !Core.Me.HasAura(AurasDefine.EnhancedHarpe))
                    return -1;
                else
                    return 1;
            }
            return -1;
        }

        public void Build(Slot slot)
        {
            if(SpellsDefine.Communio.GetSpell().IsReady() && Core.Get<IMemApiReaper>().LemureShroud == 1)
                slot.Add(SpellsDefine.Communio.GetSpell());
            else
                slot.Add(SpellsDefine.Harpe.GetSpell());
        }
    }
}
