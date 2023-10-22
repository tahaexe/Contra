using UnityEngine;
using DG.Tweening;
using TMPro;

namespace Contra
{
      public class PanelSetting : MonoBehaviour
      {
            [SerializeField] private RectTransform _content;
            [SerializeField] private RectTransform _panel;
            [SerializeField] private TextMeshProUGUI _textMute;

            private bool isPanelOpen = false;

            private void Update()
            {
                  if (isPanelOpen == false) return;

                  if (Input.GetKeyDown(KeyCode.P))
                  {
                        AudioManager.Instance.Play(AudioName.SFX_ButtonClick);

                        OnClickOpenSettingArea();
                  }

                  if (Input.GetKeyDown(KeyCode.R))
                  {
                        AudioManager.Instance.Play(AudioName.SFX_ButtonClick);

                        OnClickButtonRestart();
                  }

                  if (Input.GetKeyDown(KeyCode.M))
                  {
                        AudioManager.Instance.Play(AudioName.SFX_ButtonClick);

                        OnClickButtonMute();
                  }

                  if (Input.GetKeyDown(KeyCode.C))
                  {
                        AudioManager.Instance.Play(AudioName.SFX_ButtonClick);

                        OnClickButtonClose();
                  }
            }

            public void OpenPanel()
            {
                  _content.gameObject.SetActive(true);

                  isPanelOpen = true;

                  _content.GetComponent<CanvasGroup>().DOFade(1, 1f);
            }

            public void ClosePanel()
            {
                  isPanelOpen = false;

                  _content.GetComponent<CanvasGroup>().DOFade(0, 1f).OnComplete(() =>
                  {
                        _content.gameObject.SetActive(false);
                  });
            }

            public void OnClickOpenSettingArea()
            {
                  AudioManager.Instance.Play(AudioName.SFX_ButtonClick);

                  _panel.gameObject.SetActive(true);

                  Time.timeScale = 0;
            }

            public void OnClickButtonClose()
            {
                  AudioManager.Instance.Play(AudioName.SFX_ButtonClick);

                  _panel.gameObject.SetActive(false);

                  Time.timeScale = 1;
            }

            public void OnClickButtonMute()
            {
                  AudioManager.Instance.Play(AudioName.SFX_ButtonClick);

                  AudioListener.pause = !AudioListener.pause;

                  _textMute.text = AudioListener.pause ? "U<color=\"green\">M</color>ute" : "<color=\"green\">M</color>ute";
            }

            public void OnClickButtonRestart()
            {
                  ClosePanel();

                  OnClickButtonClose();

                  SceneFadeManager.Instance.LoadScene("GamePlay");
            }
      }
}
