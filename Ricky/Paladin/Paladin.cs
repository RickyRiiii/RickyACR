using CombatRoutine.View.JobView;
using CombatRoutine;
using Common.GUI;
using Common.Language;
using Common;
using ImGuiNET;
using Ricky.Reaper;
using Common.Define;
using Common.Helper;

namespace Ricky.Paladin;
public class PLDOverlay
{
    private bool isHorizontal;

    public void DrawGeneral(JobViewWindow jobViewWindow)
    {
        if (ImGui.CollapsingHeader("战斗设置"))
        {
            //ImGui.Button("两分钟爆发药");
            ImGuiHelper.LeftInputInt("开怪时间",
                        ref PLDSettings.Instance.Kaiguai, 0, 1000, 50);
            ImGui.Text("救人设置");
            ImGuiHelper.LeftInputInt("自动保护血量(百分比)",
                        ref RickyOptions.Instance.HealthPercentOfCover, 0, 90, 5);
            ImGuiHelper.LeftInputInt("深仁厚泽血量(百分比)",
                        ref PLDSettings.Instance.ClemencyPercent, 0, 90, 5);
            ImGuiHelper.LeftInputInt("安魂深厚血量(百分比)",
                ref PLDSettings.Instance.RCClemencyPercent, 0, 90, 5);
            ImGui.Text("四人本减伤设置");
            ImGuiHelper.LeftInputInt("30减血量(百分比)",
                        ref RickyOptions.Instance.HealthPercentOf30Reduce, 0, 100, 5);
            ImGuiHelper.LeftInputInt("亲疏血量(百分比)",
                        ref RickyOptions.Instance.HealthPercentOfArmsLength, 0, 100, 5);
            ImGuiHelper.LeftInputInt("铁壁血量(百分比)",
                        ref RickyOptions.Instance.HealthPercentOfRampart, 0, 100, 5);
            ImGuiHelper.LeftInputInt("血仇血量(百分比)",
                        ref RickyOptions.Instance.HealthPercentOfReprisal, 0, 100, 5);
            ImGuiHelper.LeftInputInt("壁垒血量(百分比)",
                        ref RickyOptions.Instance.HealthPercentOfBulwark, 0, 100, 5);
            ImGuiHelper.LeftInputInt("盾阵血量(百分比)",
                        ref RickyOptions.Instance.HealthPercentOfSheltron, 0, 100, 5);
            ImGuiHelper.LeftInputInt("幕帘血量(百分比)",
                        ref RickyOptions.Instance.HealthPercentOfDivineVeil, 0, 100, 5);
            ImGuiHelper.LeftInputInt("下踢血量(百分比)",
                        ref RickyOptions.Instance.HealthPercentOfLowBlow, 0, 100, 5);
            ImGuiHelper.LeftInputInt("无敌血量(百分比)",
                        ref RickyOptions.Instance.HealthPercentOfInvencible, 0, 100, 5);
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
            ImGui.Text($"承伤比例：{(decimal.Round(decimal.Parse(Calculate.Instance.DamageReduce().ToString()), 2))}");
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
            }
            else if (Core.Me.GetCurrTarget().IsDummy())
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
                        ImGui.Text($"减伤开开开");
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
        return PLDRotationEntry.JobViewWindow.GetQt(qtName);
    }

    /// 反转指定qt的值
    /// <returns>成功返回true，否则返回false</returns>
    public static bool ReverseQt(string qtName)
    {
        return PLDRotationEntry.JobViewWindow.ReverseQt(qtName);
    }

    /// 设置指定qt的值
    /// <returns>成功返回true，否则返回false</returns>
    public static bool SetQt(string qtName, bool qtValue)
    {
        return PLDRotationEntry.JobViewWindow.SetQt(qtName, qtValue);
    }

    /// 重置所有qt为默认值
    public static void Reset()
    {
        PLDRotationEntry.JobViewWindow.Reset();
    }

    /// 给指定qt设置新的默认值
    public static void NewDefault(string qtName, bool newDefault)
    {
        PLDRotationEntry.JobViewWindow.NewDefault(qtName, newDefault);
    }

    /// 将当前所有Qt状态记录为新的默认值，
    /// 通常用于战斗重置后qt还原到倒计时时间点的状态
    public static void SetDefaultFromNow()
    {
        PLDRotationEntry.JobViewWindow.SetDefaultFromNow();
    }

    /// 返回包含当前所有qt名字的数组
    public static string[] GetQtArray()
    {
        return PLDRotationEntry.JobViewWindow.GetQtArray();
    }
}