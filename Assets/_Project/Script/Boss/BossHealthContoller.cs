using System;
using UnityEngine;
using UnityEngine.UI;

namespace Contra
{
      public class BossHealthContoller : MonoBehaviour
      {
            [SerializeField] private Slider _slider;
            private Transform _player;

            private void Awake()
            {
                  _slider.wholeNumbers = true;
                  _slider.maxValue = 100;
                  _slider.value = 100;
            }

            private void Start()
            {
                  _player = GameObject.FindGameObjectWithTag("Player").transform;
            }

            private void Update()
            {
                  if (_player == null) return;

                  if (_player.position.x > 134)
                  {
                        _slider.gameObject.SetActive(true);
                  }
                  else
                  {
                        _slider.gameObject.SetActive(false);
                  }
            }

            private void OnEnable()
            {
                  EventManager.BossHealth += OnBossHealth;
            }

            private void OnDisable()
            {
                  EventManager.BossHealth -= OnBossHealth;
            }

            private void OnBossHealth(float health)
            {
                  _slider.value = health;
            }
      }
}
