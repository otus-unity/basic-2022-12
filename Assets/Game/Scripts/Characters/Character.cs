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

        public int Health;

        public bool IsAlive => Health > 0;


        public IEnumerator Attack(Character attackedCharacter)
        {
            var weaponAnimationName = WeaponHelpers.GetAnimationNameFor(_weapon.Type);
            _animator.SetTrigger(weaponAnimationName);

            yield return null;

            var animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            var duration = animatorStateInfo.length;

            yield return new WaitForSeconds(duration);

            attackedCharacter.Health -= _weapon.Damage;

            if (attackedCharacter.Health <= 0)
            {
                attackedCharacter.Die();
            }
        }

        private void Die()
        {
            Debug.Log($"{GetType().Name}.Die: {gameObject.name}");
        }
    }
}