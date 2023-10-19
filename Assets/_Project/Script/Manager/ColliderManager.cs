using UnityEngine;

namespace Contra
{
      public class ColliderManager : MonoBehaviour
      {
            [SerializeField] private BoxCollider2D[] _boxCollider2D;
            [SerializeField] private Transform _transformPlayer;

            private void Start()
            {
                  _transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;
            }

            private void FixedUpdate()
            {
                  foreach (var collider in _boxCollider2D)
                  {
                        if (collider.tag == "Player") continue;

                        if (_transformPlayer.position.y > collider.transform.position.y + 1f)
                              collider.isTrigger = false;
                        else
                              collider.isTrigger = true;
                  }
            }
      }
}
