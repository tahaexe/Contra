using System;
using UnityEngine;

namespace Contra
{
      public class GameManager : MonoBehaviour
      {
            public static GameManager Instance;

            [SerializeField] private GameObject _powerBoxPrefab;

            private Transform _playerTransform;
            private float _archivedPositionX;

            private bool _isDead = false;
            private bool _isWin = false;

            private void OnEnable()
            {
                  EventManager.PlayerDead += PlayerDead;
                  EventManager.PlayerWin += PlayerWin;
            }

            private void OnDisable()
            {
                  EventManager.PlayerDead -= PlayerDead;
                  EventManager.PlayerWin -= PlayerWin;
            }

            private void Awake()
            {
                  if (Instance == null)
                  {
                        Instance = this;
                  }
                  else if (Instance != this)
                  {
                        Destroy(gameObject);
                  }
            }

            private void Start()
            {
                  _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
                  _archivedPositionX = 3;
                  _isDead = false;
                  _isWin = false;

                  AudioManager.Instance.Play(AudioName.Music_Level1);

                  UIManager.Instance.ShowPanelSetting();
            }

            private void Update()
            {
                  if (_isDead) return;

                  if (_playerTransform.position.x > _archivedPositionX)
                  {
                        _archivedPositionX += 25;
                        CreatePowerBox();
                  }
            }

            private void CreatePowerBox()
            {
                  Instantiate(_powerBoxPrefab, new Vector3(_playerTransform.position.x - 15, 4, 0), Quaternion.identity);
            }

            public void PlayerDead()
            {
                  if (_isDead) return;
                  _isDead = true;

                  Invoke(nameof(DelayRequestToOpenPlayerDead), 0.7f);
            }

            private void PlayerWin()
            {
                  if (_isWin) return;
                  _isWin = true;

                  Invoke(nameof(DelayRequestToOpenPlayerWin), 0.7f);
            }

            private void DelayRequestToOpenPlayerDead()
            {
                  UIManager.Instance.ShowPanelDead();
            }

            private void DelayRequestToOpenPlayerWin()
            {
                  UIManager.Instance.ShowPanelWin();
            }
      }
}
