using System.Linq;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;

namespace Spells
{
    public class AkaliShadowSwipe : GameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var ap = owner.GetStats().AbilityPower.Total * 0.3f;
            var ad = owner.GetStats().AttackDamage.Total * 0.6f;
            var damage = 40 + spell.Level * 30 + ap + ad;
            foreach (var enemyTarget in ApiFunctionManager.GetUnitsInRange(owner, 300, true)
                .Where(x => x.Team == CustomConvert.GetEnemyTeam(owner.Team)))
            {
                enemyTarget.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL,
                    false);
                if(ApiFunctionManager.WillKill(target, damage))
                {
                    spell.LowerCooldown(2, spell.CurrentCooldown * 0.6f);
                }
            }
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {

        }

        public void OnUpdate(double diff)
        {
        }
    }
}
