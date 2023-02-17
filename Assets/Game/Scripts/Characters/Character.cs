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
        private Health _health;

        [SerializeField]
        private float _speed = 2;

        [SerializeField]
        private Transform _meleeAttackerAnchor;

        public Transform MeleeAttackerAnchor => _meleeAttackerAnchor;

        public bool IsAlive => _health.IsAlive;


        private void Start()
        {
            _health.OnDeath += OnDeath;
        }

        private void OnDestroy()
        {
            _health.OnDeath -= OnDeath;
        }

        private void OnDeath()
        {
            Debug.Log("Character.OnDeath: ");

            _animator.SetTrigger("IsDead");
        }

        public IEnumerator Attack(Character attackedCharacter)
        {
            Vector3 originalPosition = transform.position;

            if (_weapon.Type == WeaponType.BaseballBat)
            {
                _animator.SetFloat("Speed", _speed);

                // Move to the target
                float distance;
                var step = _speed * Time.deltaTime;
                do
                {
                    distance = Vector3.Distance(transform.position, attackedCharacter.MeleeAttackerAnchor.position);
                    transform.position = Vector3.MoveTowards(transform.position, attackedCharacter.MeleeAttackerAnchor.position, step);
                    yield return null;
                } while (distance > CharacterHelper.MeleeAttackDistanceThreshold);

                _animator.SetFloat("Speed", 0);
            }

            var weaponAnimationName = WeaponHelpers.GetAnimationNameFor(_weapon.Type);
            _animator.SetTrigger(weaponAnimationName);

            yield return null;

            var animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            var duration = animatorStateInfo.length;

            yield return new WaitForSeconds(duration);

            // jTODO create coroutine with animations for taking damage and death
            attackedCharacter.TakeDamage(_weapon.Damage);


            if (_weapon.Type == WeaponType.BaseballBat)
            {
                // Turn to original position
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -1 * transform.localScale.z);

                _animator.SetFloat("Speed", _speed);

                // Move back to original position
                float distance;
                var step = _speed * Time.deltaTime;
                do
                {
                    distance = Vector3.Distance(transform.position, originalPosition);
                    transform.position = Vector3.MoveTowards(transform.position, originalPosition, step);
                    yield return null;
                } while (distance > CharacterHelper.MeleeAttackDistanceThreshold);

                _animator.SetFloat("Speed", 0);

                // Turn to opponents
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -1 * transform.localScale.z);

                yield return null;
            }
        }

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }
    }
}