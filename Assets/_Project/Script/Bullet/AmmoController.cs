using UnityEngine;

namespace Contra
{
      public class AmmoController : MonoBehaviour
      {
            [SerializeField] private float _ammoSpeed;
            private Camera _camera;

            public void Awake()
            {
                  _camera = Camera.main;
            }

            private void Update()
            {
                  transform.Translate(_ammoSpeed * Time.deltaTime * transform.right, Space.World);

                  DestroyControl();
            }

            private void DestroyControl()
            {
                  if (_camera == null) _camera = Camera.main;

                  Vector2 screenBounds = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.transform.position.z));

                  // E�er obje ekran�n d���na ��karsa, yok et
                  if (transform.position.x > screenBounds.x || transform.position.x < -screenBounds.x ||
                      transform.position.y > screenBounds.y || transform.position.y < -screenBounds.y)
                  {
                        Destroy(gameObject);
                  }
            }

            private void OnTriggerEnter2D(Collider2D collision)
            {
                  Debug.Log("AAA " + collision.tag);

                  if (collision.CompareTag("Enemy"))
                  {
                        collision.GetComponent<EnemyController>().TakeDamage(1);

                        Destroy(gameObject);
                  }
            }

            private void OnCollisionEnter2D(Collision2D collision)
            {
                  Debug.Log("BBB " + collision.transform.tag);

                  if (collision.collider.CompareTag("Enemy"))
                  {
                        collision.collider.GetComponent<EnemyController>().TakeDamage(1);

                        Destroy(gameObject);
                  }
            }
      }
}
