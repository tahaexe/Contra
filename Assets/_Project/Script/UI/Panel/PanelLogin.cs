using DG.Tweening;
using UnityEngine;

namespace Contra
{
      public class PanelLogin : MonoBehaviour
      {
            [Header("Panel Setting")]
            [SerializeField] private RectTransform _content;

            public void OpenPanel()
            {
                  _content.gameObject.SetActive(true);

                  _content.DOAnchorPos(Vector2.zero, 1f);
            }

            public void ClosePanel()
            {
                  _content.DOAnchorPos(new Vector2(1920, 0), 1f).OnComplete(() =>
                  {
                        _content.gameObject.SetActive(false);
                  });
            }
      }
}
