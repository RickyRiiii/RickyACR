using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Ricky.Reaper.Sequences;

/// <summary>
/// 绝Op6开场
/// </summary>
public class DoubleEnshroud : ISlotSequence
{
    public Action CompeltedAction { get; set; }

    public int StartCheck()
    {
        //蓝条小于50不打
        if (Core.Get<IMemApiReaper>().ShroudGauge < 50)
        {
            return -1;
        }
        //团辅差6s以上转好不打
        if (SpellsDefine.ArcaneCircle.GetSpell().Cooldown.TotalMilliseconds > 5000)
        {
            return -1;
        }
        //超过近战距离不打
        if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) > SettingMgr.GetSetting<GeneralSettings>().AttackRange)
        {
            return -1;
        }
        //90级以下不打
        if (Core.Me.ClassLevel < 90)
        {
            return -1;
        }
        //附近怪物数量多于3不打
        if (TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) >= 3) return -1;

        return 1;
    }

    public int StopCheck(int index)
    {
        return -1;
    }

    public List<Action<Slot>> Sequence { get; } = new List<Action<Slot>>()
    {
        Step0,
        Step1,
        Step2,
        Step3,
    };

    private static void Step0(Slot slot)
    {
        //先开附体
        slot.Add(new Spell(SpellsDefine.Enshroud, SpellTargetType.Target));
        slot.Add(new Spell(SpellsDefine.ShadowOfDeath, SpellTargetType.Target));
        if (Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.DeathsDesign, 3000) && Core.Me.HasAura(AurasDefine.Soulsow))
        {
            slot.Add(new Spell(SpellsDefine.HarvestMoon, SpellTargetType.Target));
        }
        else
            slot.Add(new Spell(SpellsDefine.ShadowOfDeath, SpellTargetType.Target));
    }

    private static void Step1(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.ArcaneCircle, SpellTargetType.Target));

        slot.Add(new Spell(SpellsDefine.CrossReaping, SpellTargetType.Target));
        slot.Add(new Spell(SpellsDefine.VoidReaping, SpellTargetType.Target));
        slot.Add(new Spell(SpellsDefine.LemuresSlice, SpellTargetType.Target));
        slot.Add(new Spell(SpellsDefine.CrossReaping, SpellTargetType.Target));
        slot.Add(new Spell(SpellsDefine.VoidReaping, SpellTargetType.Target));
        slot.Add(new Spell(SpellsDefine.LemuresSlice, SpellTargetType.Target));
        slot.Add(new Spell(SpellsDefine.Communio, SpellTargetType.Target));
        //if (FraOptions.Instance.UsePotion) slot.Add(Spell.CreatePotion());
    }

    private static void Step2(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.PlentifulHarvest, SpellTargetType.Target));
        slot.Add(new Spell(SpellsDefine.Enshroud, SpellTargetType.Self));
    }

    private static void Step3(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.CrossReaping, SpellTargetType.Target));
        slot.Add(new Spell(SpellsDefine.VoidReaping, SpellTargetType.Target));
        slot.Add(new Spell(SpellsDefine.LemuresSlice, SpellTargetType.Target));
        slot.Add(new Spell(SpellsDefine.CrossReaping, SpellTargetType.Target));
        slot.Add(new Spell(SpellsDefine.VoidReaping, SpellTargetType.Target));
        slot.Add(new Spell(SpellsDefine.LemuresSlice, SpellTargetType.Target));
        slot.Add(new Spell(SpellsDefine.Communio, SpellTargetType.Target));
    }

}