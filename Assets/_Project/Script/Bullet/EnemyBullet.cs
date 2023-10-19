using UnityEngine;

namespace Contra
{
      public class EnemyBullet : MonoBehaviour
      {
            [SerializeField] private int _damage = 1;
            [SerializeField] private float _speed = 10f;

            private Vector2 _direction;

            private void Start()
            {
                  var playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

                  _direction = (playerTransform.position - transform.position).normalized;
            }

            private void Update()
            {
                  // Hareket et
                  transform.Translate(_speed * Time.deltaTime *  _direction,Space.World);
            }

            private void OnTriggerEnter2D(Collider2D collision)
            {
                  if (collision.CompareTag("Player"))
                  {
                        collision.GetComponent<Player>().TakeDamage(_damage);
                        Destroy(gameObject);
                  }
            }
      }
}
