using UnityEngine;

namespace Contra
{
      public class PowerBox : MonoBehaviour
      {
            [Header("Setting")]
            [SerializeField] private float _moveSpeed = 3f;
            [SerializeField] private float amplitude = 1.0f;
            [SerializeField] private float speed = 1.0f;
            [SerializeField] private Vector3 startPosition;

            [Header("Component")]
            [SerializeField] private Transform _mainTransform;
            [SerializeField] private Transform _boxTransform;

            [SerializeField] private GameObject[] _ammo;

            private Camera _camera;

            private void Start()
            {
                  startPosition = _boxTransform.position;

                  _camera = Camera.main;
            }

            private void Update()
            {
                  transform.Translate(_moveSpeed * Time.deltaTime * Vector3.right);

                  float newY = startPosition.y + amplitude * Mathf.Sin(speed * Time.time);
                  Vector3 newPosition = new Vector3(_boxTransform.position.x, newY, _boxTransform.position.z);
                  _boxTransform.position = newPosition;
            }

            private void DestroyControl()
            {
                  if (_camera == null) _camera = Camera.main;

                  Vector2 screenBounds = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.transform.position.z));

                  // Eðer obje ekranýn dýþýna çýkarsa, yok et
                  if (transform.position.x > screenBounds.x)
                  {
                        Destroy(gameObject);
                  }
            }

            private void OnTriggerEnter2D(Collider2D other)
            {
                  if (other.CompareTag("Ammo"))
                  {
                        CreatePower();

                        Destroy(other.gameObject);

                        Destroy(gameObject);
                  }
            }

            private void CreatePower()
            {
                  var randomNumber = Random.Range(0, _ammo.Length);

                  Instantiate(_ammo[randomNumber], _mainTransform.position, Quaternion.identity);
            }
      }
}
