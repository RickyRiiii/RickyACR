using CombatRoutine;
using CombatRoutine.Opener;
using Common.Define;
using Common;
using Common.Helper;
using CombatRoutine.Setting;
using ImGuiNET;
using System.Security.AccessControl;

namespace Ricky
{
    public class Calculate
    {
        public static Calculate Instance = new();

        public List<uint> Reduce30 = new()
        {
            AurasDefine.Sentinel,
            AurasDefine.Vengeance,
            AurasDefine.ShadowWall,
            AurasDefine.Nebula,

        };

        public List<uint> Reduce20 = new()
        {
            AurasDefine.Rampart,
            77u,//壁垒
        };

        public List<uint> Reduce15 = new()
        {
            AurasDefine.HeartofStone,AurasDefine.HeartOfCorundum,AurasDefine.ClarityOfCorundum,//绝枪减
            AurasDefine.Sheltron,AurasDefine.HolySheltron, AurasDefine.KnightsResolve,//剑盾减
        };

        public List<uint> Reduce10 = new()
        {
            AurasDefine.Oblation,//献奉
            AurasDefine.RawIntuition,AurasDefine.Bloodwhetting,AurasDefine.StemTheFlow,AurasDefine.NascentGlint,//战士减
            AurasDefine.Troubadour,AurasDefine.ShieldSamba,2177u,1951u,//远敏减
            AurasDefine.Kerachole,AurasDefine.Taurochole,AurasDefine.Holos,//贤者减
            299u,//野战治疗阵
            AurasDefine.DesperateMeasures,//怒涛之计
            2037u,//节制
            AurasDefine.Exaltation,//耀升
            2020u,//干预
        };
        public float DamageReduce()
        {
            float DamageTake = 1.0f;
            var AuraQurey30 = from AuraId in Reduce30
                            where Core.Me.HasAura(AuraId)  //按照条件过滤
                            orderby AuraId
                            select AuraId;
            foreach (var AuraId in AuraQurey30)
            {
                DamageTake *= 0.7f;  //修改数值
            }

            var AuraQurey20 = from AuraId in Reduce20
                            where Core.Me.HasAura(AuraId)  //按照条件过滤
                            orderby AuraId
                            select AuraId;
            foreach (var AuraId in AuraQurey20)
            {
                DamageTake *= 0.8f;  //修改数值
            }

            var AuraQurey15 = from AuraId in Reduce15
                              where Core.Me.HasAura(AuraId)  //按照条件过滤
                              orderby AuraId
                              select AuraId;
            foreach (var AuraId in AuraQurey15)
            {
                DamageTake *= 0.85f;  //修改数值
            }

            var AuraQurey10 = from AuraId in Reduce10
                              where Core.Me.HasAura(AuraId)  //按照条件过滤
                              orderby AuraId
                              select AuraId;
            foreach (var AuraId in AuraQurey10)
            {
                DamageTake *= 0.90f;  //修改数值
            }

            if (Core.Me.HasAura(AurasDefine.Camouflage))//伪装
                if (TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5) >= 3)
                    DamageTake *= 0.78f;
                else
                    DamageTake *= 0.9f;
            
            if(Core.Me.GetCurrTarget().HasAura(AurasDefine.Reprisal))//血仇
                DamageTake *= 0.9f;

            if (Core.Me.GetCurrTarget().HasAura(9u))//亲疏
                DamageTake *= 0.8f;

            if (Core.Me.HasAura(196u) || Core.Me.HasAura(863u) || Core.Me.HasAura(864u) || Core.Me.HasAura(1931))//3段TLB
                DamageTake *= 0.2f;

            if (Core.Me.HasAura(195u))//2段TLB
                DamageTake *= 0.6f;

            if (Core.Me.HasAura(194u))//1段TLB
                DamageTake *= 0.8f;
            
            /*if (Core.Me.HasAura())
            {
                PartyHelper.CastableTanks
            }*/

            return DamageTake;

        }
    }
}
    