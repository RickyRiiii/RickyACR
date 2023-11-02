using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using System.Runtime.InteropServices;

namespace Ricky.Reaper
{
    internal class RPRSpellHelper
    {
        public static Spell GetSoulGCDSpell()//龙尾龙爪
        {
            if (Core.Me.HasAura(AurasDefine.SoulReaver))
            {
                if (Qt.GetQt("AOE"))
                {
                    var aoeCount = TargetHelper.GetEnemyCountInsideSector(Core.Me, Core.Me.GetCurrTarget(), 8, 180);
                    if (aoeCount >= 3)
                    {
                        return SpellsDefine.Guillotine.GetSpell();
                    }
                }
                if (TargetHelper.GetNearbyEnemyCount(Core.Me, 20, 20) < 3)
                {
                    if (Core.Me.HasAura(AurasDefine.EnhancedGallows))
                        return SpellsDefine.Gallows.GetSpell();
                    else if (Core.Me.HasAura(AurasDefine.EnhancedGibbet))
                        return SpellsDefine.Gibbet.GetSpell();
                    if (Core.Me.GetCurrTarget().IsBehind)
                        return SpellsDefine.Gallows.GetSpell();
                    else
                        return SpellsDefine.Gibbet.GetSpell();
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}
