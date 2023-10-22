using UnityEngine;

namespace Contra
{
      public class EnemyController : MonoBehaviour
      {
            [SerializeField] private EnemyType _enemyType;
            [SerializeField] private float _moveSpeed = 1f;
            [SerializeField] private Transform _player;

            [Header("Component")]
            [SerializeField] private Transform _enemy;
            [SerializeField] private Transform _WayPoint;
            [SerializeField] private BoxCollider2D _boxCollider2D;
            [SerializeField] private SpriteRenderer _spriteRenderer;
            [SerializeField] private Animator _animator;

            [Header("Path")]
            [SerializeField] private Transform startPoint;
            [SerializeField] private Transform endPoint;

            [Header("Shooter")]
            [SerializeField] private Sprite[] characterSprites;
            [SerializeField] private GameObject bulletPrefab;
            [SerializeField] private Transform firePoint;
            [SerializeField] private float fireRate = 3f;

            private bool _isDead = false;
            private float nextFireTime = 0f;
            private bool movingToEnd = true;
            private float startX;
            private float endX;

            private void Start()
            {
                  _isDead = false;

                  if (startPoint != null) startX = startPoint.position.x;
                  if (endPoint != null) endX = endPoint.position.x;

                  _player = GameObject.FindGameObjectWithTag("Player").transform;

                  if (_WayPoint != null) _WayPoint.parent = null;
            }

            private void Update()
            {
                  if (_isDead) return;

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

                  _animator?.SetBool("Run", true);

                  // Yalnýzca x ekseni boyunca hareket et
                  transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, targetX, step), transform.position.y, _enemy.position.z);

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
                        _animator.SetBool("Down", false);
                        _animator.SetBool("Up", true);
                        _animator.SetBool("Forward", false);
                  }
                  else if (_player.position.y > transform.position.y - 2f)
                  {
                        _animator.SetBool("Down", false);
                        _animator.SetBool("Up", false);
                        _animator.SetBool("Forward", true);
                  }
                  else
                  {
                        _animator.SetBool("Down", true);
                        _animator.SetBool("Up", false);
                        _animator.SetBool("Forward", false);
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

                  if (distance > 7.5f) return;

                  if (Time.time > nextFireTime)
                  {
                        Fire();

                        nextFireTime = Time.time + 1f / fireRate;
                  }
            }

            void Fire()
            {
                  AudioManager.Instance.Play(AudioName.SFX_Weapon_M);

                  Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            }

            private void Turret()
            {

            }

            #endregion

            public void TakeDamage(int damage)
            {
                  if (_isDead) return;
                  _isDead = true;
                  _boxCollider2D.enabled = false;

                  AudioManager.Instance.Play(AudioName.SFX_EnemyDie);

                  if (_animator != null)
                  {
                        _animator.SetBool("Down", false);
                        _animator.SetBool("Up", false);
                        _animator.SetBool("Forward", false);
                        _animator.SetBool("Run", false);
                        _animator.SetBool("Die", true);
                  }

                  Destroy(gameObject, 0.48f);
            }

            private void OnTriggerEnter2D(Collider2D collision)
            {
                  if (collision.CompareTag("Player"))
                  {
                        collision.GetComponent<Player>().TakeDamage(1);
                  }
            }

            private void OnCollisionEnter2D(Collision2D collision)
            {
                  if (collision.collider.CompareTag("Player"))
                  {
                        collision.collider.GetComponent<Player>().TakeDamage(1);
                  }
            }
      }
}
