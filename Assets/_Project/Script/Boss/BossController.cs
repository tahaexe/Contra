using UnityEngine;

namespace Contra
{
      public class BossController : MonoBehaviour
      {
            [SerializeField] private float _maxHealth;
            [SerializeField] private float _currentHealth;

            [SerializeField] private Transform _explosion;

            private void Start()
            {
                  _currentHealth = _maxHealth;
            }

            public void TakeDamage(float damage)
            {
                  _currentHealth -= damage;

                  EventManager.OnBossHealth(_currentHealth);

                  AudioManager.Instance.Play(AudioName.SFX_Boss_TakeHit);

                  if (_currentHealth <= 0)
                  {
                        AudioManager.Instance.Play(AudioName.SFX_Boss_Defeated);

                        EventManager.OnPlayerWin();

                        _explosion.gameObject.SetActive(true);
                  }
            }
      }
}
