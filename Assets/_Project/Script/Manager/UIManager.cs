using UnityEngine;
using UnityEngine.SceneManagement;

namespace Contra
{
      public class UIManager : MonoBehaviour
      {
            public static UIManager Instance;

            [Header("Header")]
            [SerializeField] private PanelLogin _panelLogin;
            [SerializeField] private PanelDead _panelPlayerDead;
            [SerializeField] private PanelWin _panelPlayerWin;
            [SerializeField] private PanelSetting _panelSetting;

            private void OnEnable()
            {
                  SceneManager.sceneLoaded += OnSceneLoaded;
            }

            private void OnDisable()
            {
                  SceneManager.sceneLoaded -= OnSceneLoaded;
            }

            private void Awake()
            {
                  if (Instance == null)
                  {
                        Instance = this;
                        DontDestroyOnLoad(gameObject);
                  }
                  else if (Instance != this)
                  {
                        Destroy(gameObject);
                  }
            }

            public void ShowPanelLogin()
            {
                  _panelLogin.OpenPanel();
            }

            public void ShowPanelDead()
            {
                  _panelPlayerDead.OpenPanel();
            }

            public void ShowPanelWin()
            {
                  _panelPlayerWin.OpenPanel();
            }

            public void ShowPanelSetting()
            {
                  _panelSetting.OpenPanel();
            }

            public void CloseAllPanel()
            {
                  _panelLogin.ClosePanel();
                  _panelPlayerDead.ClosePanel();
                  _panelPlayerWin.ClosePanel();
            }

            private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
            {
                  switch (arg0.name)
                  {
                        case "Login":
                              UIManager.Instance.ShowPanelLogin();
                              break;
                        case "GamePlay":
                              break;
                  }
            }
      }
}
