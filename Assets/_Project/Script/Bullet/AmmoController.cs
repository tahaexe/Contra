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
                        collision.GetComponent<Player>().TakeDamage(1);

                        Destroy(gameObject);
                  }
            }
      }
}
