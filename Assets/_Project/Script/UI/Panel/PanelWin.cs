using DG.Tweening;
using System;
using UnityEngine;

namespace Contra
{
      public class PanelWin : MonoBehaviour
      {
            [SerializeField] private RectTransform _content;

            private bool isPlayerWin = false;

            private void OnEnable()
            {
                  EventManager.PlayerWin += PlayerWin;
            }

            private void OnDisable()
            {
                  EventManager.PlayerWin -= PlayerWin;
            }

            private void PlayerWin()
            {
                  isPlayerWin = true;
            }

            private void Start()
            {
                  isPlayerWin = false;
            }

            private void Update()
            {
                  if (isPlayerWin == false) return;


                  if (Input.GetKeyDown(KeyCode.Return))
                  {
                        AudioManager.Instance.Play(AudioName.SFX_ButtonClick);

                        isPlayerWin = false;

                        ClosePanel();

                        SceneFadeManager.Instance.LoadScene("Login");
                  }
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
      }
}
