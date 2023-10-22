using UnityEngine;

namespace Contra
{
      public class LoginMenu : MonoBehaviour
      {
            public static LoginMenu Instance;

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
                  AudioManager.Instance.Play(AudioName.Music_Intro);
            }

            private void Update()
            {
                  if (Input.GetKeyDown(KeyCode.Return))
                  {
                        AudioManager.Instance.Play(AudioName.SFX_ButtonClick);

                        SceneFadeManager.Instance.LoadScene("GamePlay");

                        UIManager.Instance.CloseAllPanel();
                  }
            }
      }
}
