using UnityEngine;

namespace Contra
{
      public class EnemyController : MonoBehaviour
      {
            [SerializeField] private EnemyType _enemyType;
            [SerializeField] private float _moveSpeed = 1f;
            [SerializeField] private Transform _player;

            [Header("Component")]
            [SerializeField] private BoxCollider2D _boxCollider2D;
            [SerializeField] private SpriteRenderer _spriteRenderer;

            [Header("Path")]
            [SerializeField] private Transform startPoint;
            [SerializeField] private Transform endPoint;

            [Header("Shooter")]
            [SerializeField] private Sprite[] characterSprites;
            [SerializeField] private GameObject bulletPrefab;
            [SerializeField] private Transform firePoint;
            [SerializeField] private float fireRate = 3f;
            private float nextFireTime = 0f;

            private bool movingToEnd = true;

            private float startX;
            private float endX;

            void Start()
            {
                  if (startPoint != null) startX = startPoint.position.x;
                  if (endPoint != null) endX = endPoint.position.x;

                  _player = GameObject.FindGameObjectWithTag("Player").transform;
            }

            private void Update()
            {
                  switch (_enemyType)
                  {
                        case EnemyType.Runner:
                              Runner();
                              break;
                        case EnemyType.Shooter:
                              Shooter();
                              break;
                        case EnemyType.Turret:
                              Turret();
                              break;
                  }
            }

            #region Enemy Type

            private void Runner()
            {
                  float targetX = movingToEnd ? endX : startX;
                  float step = _moveSpeed * Time.deltaTime;

                  // Yalnýzca x ekseni boyunca hareket et
                  transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, targetX, step), transform.position.y, transform.position.z);

                  // Flip yap
                  if (transform.position.x > targetX)
                  {
                        transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                  }
                  else if (transform.position.x < targetX)
                  {
                        transform.localScale = new Vector3(-0.25f, 0.25f, 0.25f);
                  }

                  if (Mathf.Approximately(transform.position.x, targetX))
                  {
                        movingToEnd = !movingToEnd;
                  }
            }

            private void Shooter()
            {
                  if (_player.position.y > transform.position.y + 2f)
                  {
                        _spriteRenderer.sprite = characterSprites[0];
                  }
                  else if (_player.position.y > transform.position.y - 2f)
                  {
                        _spriteRenderer.sprite = characterSprites[1];
                  }
                  else
                  {
                        _spriteRenderer.sprite = characterSprites[2];
                  }

                  if (transform.position.x > _player.position.x)
                  {
                        transform.localScale = new Vector3(1f, 1f, 1f);
                  }
                  else
                  {
                        transform.localScale = new Vector3(-1f, 1f, 1f);
                  }

                  var distance = Vector2.Distance(transform.position, _player.position);

                  if (distance > 10) return;

                  if (Time.time > nextFireTime)
                  {
                        Fire();
                        nextFireTime = Time.time + 1f / fireRate;
                  }
            }

            void Fire()
            {
                  Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            }

            private void Turret()
            {

            }

            #endregion
      }
}
