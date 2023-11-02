using CombatRoutine;
using CombatRoutine.Opener;
using Common.Define;
using Common;
using Common.Helper;
using CombatRoutine.Setting;
using System.Runtime.Intrinsics.X86;

namespace Ricky.Reaper;

public class Opener_RPR_80 : IOpener
{

    public int StartCheck()
    {
        if (Core.Get<IMemApiCondition>().IsBoundByDuty())
        {
            if (PartyHelper.NumMembers <= 4 && !Core.Me.GetCurrTarget().IsDummy() && !Core.Me.HasAura(2734))
            {
                return -1;
            }
        }
        if (!Core.Me.GetCurrTarget().IsBoss()) return -1;
        if (AI.Instance.BattleData.CurrBattleTimeInMs > 10000) return -1;
        if (!SpellsDefine.InnerRelease.IsReady()) return -1;
        return 1;
    }

    public int StopCheck(int index)
    {
        return -1;
    }

    public List<Action<Slot>> Sequence { get; set; } 

    public Action CompeltedAction { get; set; }

    public void InitCountDown(CountDownHandler countDownHandler)
    {
        //countDownHandler.AddPotionAction(1500);
        //if (SettingMgr.GetSetting<GeneralSettings>().UsePotion) countDownHandler.AddPotionAction(2000);
        if (!Core.Me.HasAura(AurasDefine.Soulsow))
            countDownHandler.AddAction(RPRSettings.Instance.Kaiguai + 5000, SpellsDefine.Soulsow, SpellTargetType.Target);
        countDownHandler.AddAction(RPRSettings.Instance.Kaiguai + 600, SpellsDefine.TrueNorth, SpellTargetType.Target);
        if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
                SettingMgr.GetSetting<GeneralSettings>().AttackRange * 3)
        {
            countDownHandler.AddAction(RPRSettings.Instance.Kaiguai, SpellsDefine.Harpe, SpellTargetType.Target);
            countDownHandler.AddAction(0, SpellsDefine.HellsIngress, SpellTargetType.Self);
        }
    }
}
