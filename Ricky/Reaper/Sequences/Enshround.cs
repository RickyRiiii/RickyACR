using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using System.Runtime.Intrinsics.X86;

namespace Ricky.Reaper.Sequences;

public class Enshround : ISlotSequence
{
    public Action CompeltedAction { get; set; }

    public int StartCheck()
    {
        if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
                SettingMgr.GetSetting<GeneralSettings>().AttackRange && TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) < 3) return -1;
        if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
            SettingMgr.GetSetting<GeneralSettings>().AttackRange * 2 && TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) >= 3) return -1;
        //如果处于附体状态
        if (Core.Me.HasAura(AurasDefine.Enshrouded))
            return 1;
        else
            return -1;
    }

    public int StopCheck(int index)
    {
        return -1;
    }

    public List<Action<Slot>> Sequence { get; } = new List<Action<Slot>>()
    {
        Step0,
    };

    public static Spell GetSpell()
    {
        //如果豆子大于1，则打普通连，否则打暴食
        if (Core.Get<IMemApiReaper>().LemureShroud > 1)
        {
            if (Qt.GetQt("AOE"))
            {
                var aoeCount = TargetHelper.GetEnemyCountInsideSector(Core.Me, Core.Me.GetCurrTarget(), 8, 180);
                if (aoeCount >= 3)
                {
                    return SpellsDefine.GrimReaping.GetSpell();
                }
            }
            if (Core.Me.HasAura(AurasDefine.EnhancedVoidReaping))
                return SpellsDefine.VoidReaping.GetSpell();
            else
                return SpellsDefine.CrossReaping.GetSpell();
        }
        else
            return SpellsDefine.Communio.GetSpell();
    }

    private static void Step0(Slot slot)
    {
        for(int i = 0; i < 4; i++)
        {
            slot.Add(GetSpell());
        }
    }
}
