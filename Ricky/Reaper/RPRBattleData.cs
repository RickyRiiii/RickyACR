using Common.Define;

namespace Ricky.Reaper;

public class RPRBattleData
{
    public static RPRBattleData Instance = new();

    public void Reset()
    {
        Instance = new RPRBattleData();
        SpellQueueGCD.Clear();
        SpellQueueoGCD.Clear();
    }
    public Queue<Spell> SpellQueueGCD = new();
    public Queue<Spell> SpellQueueoGCD = new();
}