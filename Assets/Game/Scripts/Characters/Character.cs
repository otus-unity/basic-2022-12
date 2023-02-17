using System.Collections;
using Game.Scripts.Helpers;
using UnityEngine;

namespace Game.Scripts.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        public Weapon _weapon;

        [SerializeField]
        public Health _health;

        public bool IsAlive => _health.IsAlive;


        public IEnumerator Attack(Character attackedCharacter)
        {
            var weaponAnimationName = WeaponHelpers.GetAnimationNameFor(_weapon.Type);
            _animator.SetTrigger(weaponAnimationName);

            yield return null;

            var animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            var duration = animatorStateInfo.length;

            yield return new WaitForSeconds(duration);

            attackedCharacter._health.TakeDamage(_weapon.Damage);
        }
    }
}