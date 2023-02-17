using System.Collections;
using UnityEngine;

namespace Game.Scripts.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        public int Health;

        public IEnumerator Attack(Character attackedCharacter)
        {
            if (Health > 8)
            {
                _animator.SetTrigger("IsShooting");
            }
            else
            {
                _animator.SetBool("IsHitting", true);
            }

            yield return null;

            var animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            var duration = animatorStateInfo.length;

            yield return new WaitForSeconds(duration);

            if (Health <= 8)
            {
                _animator.SetBool("IsHitting", false);
            }

            attackedCharacter.Health -= 1;

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