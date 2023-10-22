using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Contra
{
      public class SceneFadeManager : MonoBehaviour
      {
            public static SceneFadeManager Instance;

            [SerializeField] private CanvasGroup _canvasGroup;

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

                  _canvasGroup.alpha = 0;
                  _canvasGroup.gameObject.SetActive(false);
            }

            private void OnEnable()
            {
                  SceneManager.sceneLoaded += OnSceneLoaded;
            }

            private void OnDisable()
            {
                  SceneManager.sceneLoaded -= OnSceneLoaded;
            }

            private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
            {
                  CloseSceneFade();
            }

            public void LoadScene(string sceneName)
            {
                  _canvasGroup.gameObject.SetActive(true);
                  _canvasGroup.DOFade(1, 1f).OnComplete(() =>
                  {
                        SceneManager.LoadScene(sceneName);
                  });
            }

            private void CloseSceneFade()
            {
                  _canvasGroup.DOFade(0, 1f).OnComplete(() =>
                  {
                        _canvasGroup.gameObject.SetActive(false);
                  });
            }
      }
}
