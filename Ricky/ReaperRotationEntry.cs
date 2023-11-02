using CombatRoutine;
using CombatRoutine.Opener;
using CombatRoutine.View.JobView;
using Common;
using Common.Define;
using Common.Language;
using Ricky.Reaper;
using Ricky.Reaper.GCD;
using Ricky.Reaper.Ability;
using Ricky.Reaper.Sequences;
using System.ComponentModel;

namespace Ricky

{
    public class ReaperRotationEntry : IRotationEntry
    {
        public static JobViewWindow JobViewWindow;
        private RPROverlay _lazyOverlay = new RPROverlay();
        public string OverlayTitle { get; } = "Ricky RPR";
        public string AuthorName { get; } = "Ricky";

        public Jobs TargetJob { get; } = Jobs.Reaper;

        public List<ISlotResolver> SlotResolvers = new()
        {
            //GCD安排
            new RPRGCD_SoulReaver(),
            new RPRGCD_Harpe(),
            new RPRGCD_HarvestMoon(),
            new RPRGCD_DeathDesign(),
            new RPRGCD_Enshroud(),
            new RPRGCD_PlentifulHarvest(),
            new RPRGCD_SoulSlice(),
            new RPRGCD_BaseCombo(),



            //能力技
            new RPRAbility_ArcaneCircle(),
            new RPRAbility_Soul(),
            new RPRAbility_Enshroud(),
            new RPRAbility_Enshrouded(),
            new RPRAbility_TrueNorth(),
            new RPRAbility_ArcaneCrest(),
            new RPRAbility_SecondWind(),
            new RPRAbility_BloodBath(),
        };

        
        public Rotation Build(string settingFolder)
        {
            RPRSettings.Build(settingFolder);
            return new Rotation(this, () => SlotResolvers)
                .AddOpener(GetOpener)
                .SetRotationEventHandler(new RPRRotationEventHandler())
                .AddSettingUIs()
                .AddSlotSequences(new DoubleEnshroud())
                .AddTriggerAction();
        }


        public bool BuildQt(out JobViewWindow jobViewWindow)
        {
            jobViewWindow = new JobViewWindow(RPRSettings.Instance.JobViewSave, RPRSettings.Instance.Save, OverlayTitle);
            JobViewWindow = jobViewWindow; // 这里设置一个静态变量.方便其他地方用
            jobViewWindow.AddTab("通用", _lazyOverlay.DrawGeneral);
            jobViewWindow.AddTab("时间轴", _lazyOverlay.DrawTimeLine);
            jobViewWindow.AddTab("DEV", _lazyOverlay.DrawDev);
            jobViewWindow.AddQt("爆发药", false);
            jobViewWindow.AddQt("续烙印", true);
            jobViewWindow.AddQt("途中爆发", true);
            jobViewWindow.AddQt("AOE", true);
            jobViewWindow.AddQt("起手爆发", true);
            jobViewWindow.AddQt("自动真北", true);
            jobViewWindow.AddQt("自动白盾", true);

            jobViewWindow.AddHotkey("地狱入境", new HotKeyResolver_NormalSpell(SpellsDefine.HellsIngress, SpellTargetType.Self, false));
            jobViewWindow.AddHotkey("地狱出境", new HotKeyResolver_NormalSpell(SpellsDefine.HellsEgress, SpellTargetType.Self, false));
            jobViewWindow.AddHotkey("回退", new HotKeyResolver_NormalSpell(SpellsDefine.Regress, SpellTargetType.Self, false));
            jobViewWindow.AddHotkey("牵制", new HotKeyResolver_NormalSpell(SpellsDefine.Feint, SpellTargetType.Target, false));
            jobViewWindow.AddHotkey("LB", new HotKeyResolver_NormalSpell(24858u, SpellTargetType.Target, false));
            jobViewWindow.AddHotkey("防击退", new HotKeyResolver_NormalSpell(SpellsDefine.ArmsLength, SpellTargetType.Self, false));
            return true;
        }

        private IOpener? GetOpener(uint level)
        {
            if(level == 90)
            {
                return new Opener_RPR_90();
            }
            if(level < 90)
            {
                return new Opener_RPR_80();
            }
            return null;
        }
    }
}