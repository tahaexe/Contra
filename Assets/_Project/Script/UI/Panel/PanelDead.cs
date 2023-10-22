using UnityEngine;
using DG.Tweening;
using System;

namespace Contra
{
      public class PanelDead : MonoBehaviour
      {
            [SerializeField] private RectTransform _content;

            private bool isPlayerDead = false;

            private void Start()
            {
                  isPlayerDead = false;
            }

            private void OnEnable()
            {
                  EventManager.PlayerDead += PlayerDead;
            }

            private void OnDisable()
            {
                  EventManager.PlayerDead -= PlayerDead;
            }

            private void PlayerDead()
            {
                  isPlayerDead = true;
            }

            public void OpenPanel()
            {
                  _content.gameObject.SetActive(true);

                  _content.GetComponent<CanvasGroup>().DOFade(1, 1f);
            }

            public void ClosePanel()
            {
                  _content.GetComponent<CanvasGroup>().DOFade(0, 1f).OnComplete(() =>
                  {
                        _content.gameObject.SetActive(false);
                  });
            }

            private void Update()
            {
                  if (isPlayerDead == false) return;

                  if (Input.GetKeyDown(KeyCode.Return))
                  {
                        OnClickRestart();
                  }
            }

            public void OnClickRestart()
            {
                  if (isPlayerDead == false) return;
                  isPlayerDead = false;

                  AudioManager.Instance.Play(AudioName.SFX_ButtonClick);

                  ClosePanel();

                  SceneFadeManager.Instance.LoadScene("GamePlay");
            }
      }
}
