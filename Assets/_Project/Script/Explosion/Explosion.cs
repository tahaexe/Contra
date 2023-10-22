using UnityEngine;

namespace Contra
{
      public class Explosion : MonoBehaviour
      {
            [SerializeField] private float _explosionTimer;

            private void Start()
            {
                  Destroy(gameObject, _explosionTimer);
            }
      }
}
