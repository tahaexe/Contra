using UnityEngine;

namespace Contra
{
      public class Ammo_R : MonoBehaviour
      {
            [SerializeField] private Transform _ammoTransform;
            [SerializeField] private float _rotationSpeed = 30.0f;
            [SerializeField] private float _ammoSpeed = 3.0f;
            [SerializeField] private float _radius = 2.0f;
            [SerializeField] private float _angle = 0;

            private Camera _camera;

            private void Start()
            {
                  _camera = Camera.main;
            }

            private void Update()
            {
                  _angle += _rotationSpeed * Time.deltaTime;
                  float radian = _angle * Mathf.Deg2Rad;

                  // Yeni pozisyon hesapla
                  float newX = transform.position.x + Mathf.Cos(radian) * _radius;
                  float newY = transform.position.y + Mathf.Sin(radian) * _radius;
                  Vector3 newPosition = new Vector3(newX, newY, transform.position.z);

                  if (_ammoTransform == null) Destroy(gameObject);

                  if (_ammoTransform != null) _ammoTransform.position = newPosition;

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
      }
}
