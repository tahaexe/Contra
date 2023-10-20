using UnityEngine;

namespace Contra
{
      public class EnemyBullet : MonoBehaviour
      {
            [SerializeField] private int _damage = 1;
            [SerializeField] private float _speed = 10f;

            private Camera _camera;
            private Vector2 _direction;

            private void Start()
            {
                  var playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

                  _camera = Camera.main;

                  _direction = (playerTransform.position - transform.position).normalized;
            }

            private void Update()
            {
                  // Hareket et
                  transform.Translate(_speed * Time.deltaTime * _direction, Space.World);

                  DestroyControl();
            }

            private void DestroyControl()
            {
                  if (_camera == null) _camera = Camera.main;

                  Vector2 screenBounds = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.transform.position.z));

                  // Eðer obje ekranýn dýþýna çýkarsa, yok et
                  if (transform.position.x > screenBounds.x || transform.position.x < -screenBounds.x ||
                      transform.position.y > screenBounds.y || transform.position.y < -screenBounds.y)
                  {
                        Destroy(gameObject);
                  }
            }

            private void OnTriggerEnter2D(Collider2D collision)
            {
                  if (collision.CompareTag("Player"))
                  {
                        collision.GetComponent<Player>().TakeDamage(_damage);
                  }
            }

            private void OnCollisionEnter2D(Collision2D collision)
            {
                  if (collision.collider.CompareTag("Player"))
                  {
                        collision.collider.GetComponent<Player>().TakeDamage(_damage);
                  }
            }
      }
}
