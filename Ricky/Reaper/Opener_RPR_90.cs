using CombatRoutine;
using CombatRoutine.Opener;
using Common.Define;
using Common;
using Common.Helper;
using CombatRoutine.Setting;
using ImGuiNET;

namespace Ricky.Reaper;

public class Opener_RPR_90 : IOpener
{
    public uint Level { get; } = 90;

    public int StartCheck()
    {
        if (!Qt.GetQt("起手爆发")) return -1;
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

    public List<Action<Slot>> Sequence { get; } = new()
    {
        Step0,
        Step1,
        Step2,
        Step3,
        Step4,
        Step5,
    };

    public Action CompeltedAction { get; set; }


    private static void Step0(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.ShadowOfDeath, SpellTargetType.Target)); //烙印
        if (Qt.GetQt("爆发药")) slot.Add(Spell.CreatePotion());
    }


    private static void Step1(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.SoulSlice, SpellTargetType.Target)); //切割
        slot.Add(new Spell(SpellsDefine.ArcaneCircle, SpellTargetType.Self)); //团辅
        slot.Add(new Spell(SpellsDefine.Gluttony, SpellTargetType.Target)); //暴食
    }


    private static void Step2(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.Gibbet, SpellTargetType.Target)); //龙尾大回旋
        //slot.Add(new Spell(SpellsDefine.ArcaneCrest, SpellTargetType.Target)); //神秘纹
    }


    private static void Step3(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.Gallows, SpellTargetType.Target)); //龙牙龙爪
    }


    private static void Step4(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.PlentifulHarvest, SpellTargetType.Target)); //丰收
        slot.Add(new Spell(SpellsDefine.Enshroud, SpellTargetType.Target)); //附体
    }

    private static void Step5(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.VoidReaping, SpellTargetType.Target));//红1
    }

    public void InitCountDown(CountDownHandler countDownHandler)
    {
        //countDownHandler.AddPotionAction(1500);
        //if (SettingMgr.GetSetting<GeneralSettings>().UsePotion) countDownHandler.AddPotionAction(2000);
        if(!Core.Me.HasAura(AurasDefine.Soulsow))
            countDownHandler.AddAction(RPRSettings.Instance.Kaiguai + 5000, SpellsDefine.Soulsow, SpellTargetType.Target);
        countDownHandler.AddAction(RPRSettings.Instance.Kaiguai + 600, SpellsDefine.TrueNorth, SpellTargetType.Target);
        if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
                SettingMgr.GetSetting<GeneralSettings>().AttackRange*3)
        {
            countDownHandler.AddAction(RPRSettings.Instance.Kaiguai, SpellsDefine.Harpe, SpellTargetType.Target);
            countDownHandler.AddAction(0, SpellsDefine.HellsIngress, SpellTargetType.Self);
        }
    }
}