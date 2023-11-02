using CombatRoutine;
using CombatRoutine.Opener;
using CombatRoutine.View.JobView;
using Common;
using Common.Define;
using Common.Language;
using System.ComponentModel;
using Ricky.Paladin;
using Ricky.Paladin.GCD;
using Ricky.Paladin.Ability;
using Ricky.Reaper.Ability;

namespace Ricky

{
    public class PLDRotationEntry : IRotationEntry
    {
        public static JobViewWindow JobViewWindow;
        private PLDOverlay _lazyOverlay = new PLDOverlay();
        public string OverlayTitle { get; } = "Ricky PLD";
        public string AuthorName { get; } = "Ricky";

        public Jobs TargetJob { get; } = Jobs.Paladin;

        public List<ISlotResolver> SlotResolvers = new()
        {
            //GCD安排
            new PLDGCD_ShieldLob(),
            new PLDGCD_Clemency(),
            new PLDGCD_Confiteor(),
            new PLDGCD_Atonement(),
            new PLDGCD_HolyMagic(),
            new PLDGCD_BaseCombo(),
            
            //能力技
            new PLD_AutoTForm(),
            new PLD_AutoCover(),
            new PLDAbility_Requiescat(),
            new PLDAbility_FightOrFlight(),
            new PLDAbility_CircleOfScorn(),
            new PLDAbility_Expiacions(),
            new PLDAbility_Intervene(),
            
        };


        public Rotation Build(string settingFolder)
        {
            PLDSettings.Build(settingFolder);
            return new Rotation(this, () => SlotResolvers)
                .AddOpener(GetOpener)
                .SetRotationEventHandler(new PLDRotationEventHandler())
                .AddSettingUIs()
                .AddSlotSequences()
                .AddTriggerAction()
                .AddCanUseHighPrioritySlotCheck(CanUseHighPrioritySlotCheck);
        }


        public bool BuildQt(out JobViewWindow jobViewWindow)
        {
            jobViewWindow = new JobViewWindow(PLDSettings.Instance.JobViewSave, PLDSettings.Instance.Save, OverlayTitle);
            JobViewWindow = jobViewWindow; // 这里设置一个静态变量.方便其他地方用
            jobViewWindow.AddTab("通用", _lazyOverlay.DrawGeneral);
            jobViewWindow.AddTab("时间轴", _lazyOverlay.DrawTimeLine);
            jobViewWindow.AddTab("DEV", _lazyOverlay.DrawDev);
            jobViewWindow.AddQt("爆发药", false);
            jobViewWindow.AddQt("存赎罪", true);
            jobViewWindow.AddQt("厄运拉怪", false);
            jobViewWindow.AddQt("AOE", true);
            jobViewWindow.AddQt("起手爆发", true);
            jobViewWindow.AddQt("四人本自动减伤", true);
            jobViewWindow.AddQt("自动团减", true);
            jobViewWindow.AddQt("自动保护", false);
            jobViewWindow.AddQt("自动干预", false);
            jobViewWindow.AddQt("没有战逃不打安魂", true);
            jobViewWindow.AddQt("远离不打战逃", true);
            jobViewWindow.AddQt("远离圣灵", true);
            jobViewWindow.AddQt("八人本不投盾", true);
            return true;
        }

        private IOpener? GetOpener(uint level)
        {
            if (level == 90)
            {
                return new Opener_PLD_90();
            }
            if (level < 90)
            {
                return null;
            }
            return null;
        }

        public int CanUseHighPrioritySlotCheck(SlotMode slotMode, Spell spell)
        {
            switch (slotMode)
            {
                case SlotMode.Gcd:
                    {
                        //检查技能是否需要咏唱
                        if (spell.CastTime.TotalSeconds > 0)
                        {
                            //检测移动
                            if (Core.Get<IMemApiMove>().IsMoving())
                            {
                                return -1;
                            }
                        }
                        return 1;
                    }
                case SlotMode.OffGcd:
                    {
                        //检查队列第一个是否可用
                        if (spell.Charges >= 1)
                        {
                            return 1;
                        }

                        return -1;
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(slotMode), slotMode, null);
            }
        }

    }
}