using UnityEngine;

namespace Contra
{

      public class CameraController : MonoBehaviour
      {
            [Header("Camera Setting")]
            [SerializeField] private Transform _player;
            [SerializeField] private float minX;
            [SerializeField] private float maxX;
            [SerializeField] private float minY;
            [SerializeField] private float maxY;

            [SerializeField] private float _smoothForce = 5.0f;

            private Vector3 _offset;

            private void Start()
            {
                  _player = GameObject.FindGameObjectWithTag("Player").transform;

                  _offset = transform.position - _player.position;

                  _player.GetComponent<Player>().SetDeadline(minX, maxX, minY,maxY);
            }

            void LateUpdate()
            {
                  Vector3 hedefPozisyon = _player.position + _offset;

                  hedefPozisyon.y = transform.position.y;

                  hedefPozisyon.x = Mathf.Clamp(hedefPozisyon.x, minX, maxX);

                  transform.position = Vector3.Lerp(transform.position, hedefPozisyon, _smoothForce * Time.deltaTime);
            }
      }
}
