using CombatRoutine.View.JobView;
using CombatRoutine;
using Common.GUI;
using Common.Language;
using Common;
using ImGuiNET;
using Ricky.Reaper;
using Common.Define;
using Common.Helper;

namespace Ricky.Reaper;
public class RPROverlay
{
    private bool isHorizontal;

    public void DrawGeneral(JobViewWindow jobViewWindow)
    {
        if (ImGui.CollapsingHeader("战斗设置"))
        {
            //ImGui.Button("两分钟爆发药");
            ImGui.Text("浴血内丹设置");
            ImGuiHelper.LeftInputInt("自动浴血血量(百分比)",
                        ref RPRSettings.Instance.BooldBathPercent, 5, 90, 5);
            ImGuiHelper.LeftInputInt("自动内丹血量(百分比)",
                ref RPRSettings.Instance.SecondWindPercent, 5, 90, 5);
        }

        if (ImGui.CollapsingHeader("插入技能状态"))
        {
            if (ImGui.Button("清除队列"))
            {
                AI.Instance.BattleData.HighPrioritySlots_OffGCD.Clear();
                AI.Instance.BattleData.HighPrioritySlots_GCD.Clear();
            }

            ImGui.SameLine();
            if (ImGui.Button("清除一个"))
            {
                AI.Instance.BattleData.HighPrioritySlots_OffGCD.Dequeue();
                AI.Instance.BattleData.HighPrioritySlots_GCD.Dequeue();
            }

            ImGui.Text("-------能力技-------");
            if (AI.Instance.BattleData.HighPrioritySlots_OffGCD.Count > 0)
                foreach (var spell in AI.Instance.BattleData.HighPrioritySlots_OffGCD)
                    ImGui.Text(spell.Name);
            ImGui.Text("-------GCD-------");
            if (AI.Instance.BattleData.HighPrioritySlots_GCD.Count > 0)
                foreach (var spell in AI.Instance.BattleData.HighPrioritySlots_GCD)
                    ImGui.Text(spell.Name);
        }

    }

    public void DrawTimeLine(JobViewWindow jobViewWindow)
    {
        var currTriggerline = AI.Instance.TriggerlineData.CurrTriggerLine;
        var notice = "无";
        if (currTriggerline != null) notice = $"[{currTriggerline.Author}]{currTriggerline.Name}";

        ImGui.Text(notice);
        if (currTriggerline != null)
        {
            ImGui.Text("导出变量:".Loc());
            ImGui.Indent();
            foreach (var v in currTriggerline.ExposedVars)
            {
                var oldValue = AI.Instance.ExposedVars.GetValueOrDefault(v);
                ImGuiHelper.LeftInputInt(v, ref oldValue);
                AI.Instance.ExposedVars[v] = oldValue;
            }

            ImGui.Unindent();
        }
    }

    public void DrawDev(JobViewWindow jobViewWindow)
    {
        if (ImGui.TreeNode("循环"))
        {
            ImGui.Text($"爆发药：{Qt.GetQt("爆发药")}");
            ImGui.Text($"gcd是否可用：{AI.Instance.CanUseGCD()}");
            ImGui.Text($"gcd剩余时间：{AI.Instance.GetGCDCooldown()}");
            ImGui.Text($"gcd总时间：{AI.Instance.GetGCDDuration()}");
            ImGui.TreePop();
        }


        if (ImGui.TreeNode("技能释放"))
        {
            ImGui.Text($"上个技能：{Core.Get<IMemApiSpellCastSucces>().LastSpell}");
            ImGui.Text($"上个GCD：{Core.Get<IMemApiSpellCastSucces>().LastGcd}");
            ImGui.Text($"上个能力技：{Core.Get<IMemApiSpellCastSucces>().LastAbility}");
            ImGui.Text($"距离神秘环还有：{SpellsDefine.ArcaneCircle.GetSpell().Cooldown.TotalMilliseconds}");
            ImGui.TreePop();
        }

        if (ImGui.TreeNode("小队"))
        {
            ImGui.Text($"小队人数：{PartyHelper.CastableParty.Count}");
            ImGui.Text($"小队坦克数量：{PartyHelper.CastableTanks.Count}");
            ImGui.TreePop();
        }

        if (ImGui.TreeNode("目标"))
        {
            ImGui.Text($"目标名称：{Core.Me.GetCurrTarget().Name}");
            ImGui.Text($"目标该读条已进行：{Core.Me.GetCurrTarget().CurrentCastTime}");
            ImGui.Text($"目标该读条总时间：{Core.Me.GetCurrTarget().TotalCastTime}");
            if (Core.Me.GetCurrTarget().IsBoss())
            {
                ImGui.Text($"目标类型：Boss");
            }else if (Core.Me.GetCurrTarget().IsDummy())
            {
                ImGui.Text($"目标类型：木人");
            }
            else
            {
                ImGui.Text($"目标类型：非Boss");
            }
            if (!Core.Me.GetCurrTarget().IsNull() && Core.Me.GetCurrTarget().IsCasting)
            {
                if (Core.Me.GetCurrTarget().CastingSpellId.GetSpell().IsBossAoe())
                {
                    ImGui.Text($"AOE要来了");
                    if (Core.Me.GetCurrTarget().TotalCastTime - Core.Me.GetCurrTarget().CurrentCastTime < 5.0)
                    {
                        ImGui.Text($"白盾开开开");
                    }
                }
            }
            ImGui.TreePop();
        }
    }
}

public static class Qt
{
    /// 获取指定名称qt的bool值
    public static bool GetQt(string qtName)
    {
        return ReaperRotationEntry.JobViewWindow.GetQt(qtName);
    }

    /// 反转指定qt的值
    /// <returns>成功返回true，否则返回false</returns>
    public static bool ReverseQt(string qtName)
    {
        return ReaperRotationEntry.JobViewWindow.ReverseQt(qtName);
    }

    /// 设置指定qt的值
    /// <returns>成功返回true，否则返回false</returns>
    public static bool SetQt(string qtName, bool qtValue)
    {
        return ReaperRotationEntry.JobViewWindow.SetQt(qtName, qtValue);
    }

    /// 重置所有qt为默认值
    public static void Reset()
    {
        ReaperRotationEntry.JobViewWindow.Reset();
    }

    /// 给指定qt设置新的默认值
    public static void NewDefault(string qtName, bool newDefault)
    {
        ReaperRotationEntry.JobViewWindow.NewDefault(qtName, newDefault);
    }

    /// 将当前所有Qt状态记录为新的默认值，
    /// 通常用于战斗重置后qt还原到倒计时时间点的状态
    public static void SetDefaultFromNow()
    {
        ReaperRotationEntry.JobViewWindow.SetDefaultFromNow();
    }

    /// 返回包含当前所有qt名字的数组
    public static string[] GetQtArray()
    {
        return ReaperRotationEntry.JobViewWindow.GetQtArray();
    }
}