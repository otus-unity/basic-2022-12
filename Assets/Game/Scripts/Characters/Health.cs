using System;
using UnityEngine;

namespace Game.Scripts.Characters
{
    [Serializable]
    public class Health
    {
        public Action OnDeath;

        [SerializeField]
        private int _maximumHealth;

        [SerializeField]
        private int _currentHealth;

        public bool IsAlive => _currentHealth > 0;

        // TODO Type of damage
        public void TakeDamage(int damage)
        {
            if (!IsAlive)
                return;

            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        public void Heal()
        {
            _currentHealth = _maximumHealth;
        }

        private void Die()
        {
            Debug.Log("Health.Die: ");
            OnDeath?.Invoke();
        }
    }
}