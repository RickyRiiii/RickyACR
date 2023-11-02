using CombatRoutine;
using CombatRoutine.Setting;
using CombatRoutine.View;
using Common;
using Common.Define;
using Common.Helper;
using Ricky.Reaper;

namespace Ricky.Reaper;

public class RPRRotationEventHandler : IRotationEventHandler
{
    public void OnEnterRotation()
    {
        LogHelper.Print("支持全等级，高难请使用2.5技速");
    }
    public void OnResetBattle()
    {
        RPRBattleData.Instance.Reset();
        if (SettingMgr.GetSetting<AutoResetSetting>().ResetButton)
        {
            RickyOptions.Instance.Reset();
        }
    }

    public Task OnPreCombat()
    {
        return Task.CompletedTask;
    }

    public Task OnNoTarget()
    {
        return Task.CompletedTask;
    }

    public void AfterSpell(Slot slot, Spell spell)
    {
    }

    public void OnBattleUpdate(int currTime)
    {
        if (Core.Me.HasAura(AurasDefine.SoulReaver) && AI.Instance.GetGCDCooldown() <= 2250)
        {
            if (Core.Me.HasAura(AurasDefine.EnhancedGallows))
            {
                MeleePosHelper.Draw(MeleePosHelper.Pos.Behind, AI.Instance.GetGCDCooldown() * 100 / 2500);
            }

            if (Core.Me.HasAura(AurasDefine.EnhancedGibbet))
            {
                MeleePosHelper.Draw(MeleePosHelper.Pos.Flank, AI.Instance.GetGCDCooldown() * 100 / 2500);
            }
        }
        else
        {
            MeleePosHelper.Clear();
        }
    }
}