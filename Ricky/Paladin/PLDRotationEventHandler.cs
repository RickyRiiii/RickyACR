using CombatRoutine;
using CombatRoutine.Setting;
using CombatRoutine.View;
using Common;
using Common.Define;
using Common.Helper;
using Ricky.FuncionalAbility.AffinityAbility;
using Ricky.Reaper;

namespace Ricky.Paladin;

public class PLDRotationEventHandler : IRotationEventHandler
{
    public void OnEnterRotation()
    {
        //LogHelper.Print("支持全等级，高难请使用2.5技速");
    }
    public void OnResetBattle()
    {
        PLDBattleData.Instance.Reset();
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
        
    }
}