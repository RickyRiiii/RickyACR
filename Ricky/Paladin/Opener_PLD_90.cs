using CombatRoutine;
using CombatRoutine.Opener;
using Common.Define;
using Common;
using Common.Helper;
using CombatRoutine.Setting;
using ImGuiNET;

namespace Ricky.Paladin;

public class Opener_PLD_90 : IOpener
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
    };

    public Action CompeltedAction { get; set; }


    private static void Step0(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.FastBlade, SpellTargetType.Target));//1
        if(Qt.GetQt("自动干预"))
            slot.Add(new Spell(SpellsDefine.Intervention, SpellTargetType.Pm2));//干预
    }


    private static void Step1(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.RiotBlade, SpellTargetType.Target)); //2
        if (Qt.GetQt("爆发药")) slot.Add(Spell.CreatePotion());
    }


    private static void Step2(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.RoyalAuthority, SpellTargetType.Target)); //3
        slot.Add(new Spell(SpellsDefine.FightorFlight, SpellTargetType.Self));//战逃
        slot.Add(new Spell(SpellsDefine.Requiescat, SpellTargetType.Target));//安魂
    }


    private static void Step3(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.GoringBlade, SpellTargetType.Target)); //沥血
    }

    public void InitCountDown(CountDownHandler countDownHandler)
    {
        //countDownHandler.AddPotionAction(1500);
        //if (SettingMgr.GetSetting<GeneralSettings>().UsePotion) countDownHandler.AddPotionAction(2000);
        countDownHandler.AddAction(PLDSettings.Instance.Kaiguai + 5000, SpellsDefine.Sprint, SpellTargetType.Self);
        countDownHandler.AddAction(PLDSettings.Instance.Kaiguai + 1500, SpellsDefine.HolySpirit, SpellTargetType.Target);
    }
}