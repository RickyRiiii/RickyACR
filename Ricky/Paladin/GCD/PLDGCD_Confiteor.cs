using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Ricky.Paladin.GCD
{
    public class PLDGCD_Confiteor : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.Gcd;

        private Spell GetSpell()
        {
            LogHelper.Print(Core.Get<IMemApiSpell>().GetLastComboSpellId().ToString());
            LogHelper.Print(Core.Me.LastSpellId.ToString());
            //等级为90级
            if (Core.Me.ClassLevel == 90)
            {
                if (Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.Confiteor.GetSpell().Id) == SpellsDefine.BladeOfTruth || Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.Confiteor.GetSpell().Id) == SpellsDefine.BladeOfFaith || Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.Confiteor.GetSpell().Id) == SpellsDefine.BladeOfValor)
                {
                    return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.Confiteor.GetSpell().Id).GetSpell();
                }
            }
            //如果自身有悔罪预备
            if(Core.Me.HasAura(AurasDefine.ConfiteorReady))
                return SpellsDefine.Confiteor.GetSpell();
            //如果周围目标数小于3或自身等级小于72(未习得圣环)或AOE的Qt未打开
            if (TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) < 3 || Core.Me.ClassLevel < 72 || !Qt.GetQt("AOE"))
            {
                //打圣灵
                return SpellsDefine.HolySpirit.GetSpell();
            }
            //如果AOE的Qt已打开
            if (Qt.GetQt("AOE"))
            {
                //技能范围内有3个以上敌人就打圣环
                var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5);
                if (aoeCount >= 3)
                {
                    return SpellsDefine.HolyCircle.GetSpell();
                }
            }
            //停手（调整位置打圣环）
            return null;
        }

        public int Check()
        {
            if (Core.Me.HasAura(AurasDefine.Requiescat)) return 1;
            return -1;
        }

        public void Build(Slot slot)
        {
            var spell = GetSpell();
            if (spell == null) return;
            slot.Add(spell);
        }
    }
}
