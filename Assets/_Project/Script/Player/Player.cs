using UnityEngine;

namespace Contra
{
      public class Player : MonoBehaviour
      {
            [Header("Player Stat")]
            [SerializeField] private float _attackRate;
            [SerializeField] private int _attackDamage;
            [SerializeField] private int _health;
            [SerializeField] private float _moveSpeed = 10f;
            [SerializeField] private float _jumpSpeed = 10f;

            [SerializeField] private Ammo _ammo;

            [SerializeField] private LayerMask _groundLayerMask;

            [Header("Component")]
            [SerializeField] private Rigidbody2D _rigidbody2D;
            [SerializeField] private Animator _animator;
            [SerializeField] private SpriteRenderer _spriteRenderer;
            [SerializeField] private Transform _groundCheck;
            [SerializeField] private BoxCollider2D _boxCollider2D;
            [SerializeField] private Transform _transformAttackPoint;
            [SerializeField] private AudioSource _audioSource;

            [SerializeField] private bool _isGrounded;
            [SerializeField] private bool _isGoingDown;
            [SerializeField] private bool _isInWater;
            [SerializeField] private bool _isCrouch;



            private Vector2 _lookingDirection;


            private bool _isDie;
            private float _horizontalMove;
            private float _nextAttackTime;
            private float nextFireTime = 0f;

            private int _runAnimation;
            private int _jumpAnimation;
            private int _attackAnimation;
            private int _upAnimation;
            private int _downAnimation;
            private int _crouchAnimation;
            private int _standUpAnimation;
            private int _inWaterAnimation;

            private float _minX, _maxX, _minY, _maxY;

            private void Start()
            {
                  _isDie = false;

                  _runAnimation = Animator.StringToHash("Run");
                  _jumpAnimation = Animator.StringToHash("Jump");
                  _attackAnimation = Animator.StringToHash("Attack");
                  _upAnimation = Animator.StringToHash("Up");
                  _downAnimation = Animator.StringToHash("Down");
                  _crouchAnimation = Animator.StringToHash("Crouch");
                  _standUpAnimation = Animator.StringToHash("StandUP");
                  _inWaterAnimation = Animator.StringToHash("InWater");
            }

            private void Update()
            {
                  if (_isDie) return;

                  _horizontalMove = Input.GetAxisRaw("Horizontal");

                  _isGrounded = Physics2D.OverlapPoint(_groundCheck.position, _groundLayerMask);

                  _nextAttackTime += Time.deltaTime;

                  Jump();

                  GoDown();

                  PlayerController();

                  DeathZoneHandle();
            }

            private void DeathZoneHandle()
            {
                  var x = Mathf.Clamp(transform.position.x, _minX - 3, _maxX);
                  var y = Mathf.Clamp(transform.position.y, _minY, _maxY);

                  transform.position = new Vector3(x, y, transform.position.z);

                  if (transform.position.y < -10)
                  {
                        Die();
                  }
            }

            private void FixedUpdate()
            {
                  if (_isDie) return;

                  Movement();
            }

            private void PlayerController()
            {
                  bool isMoving = _rigidbody2D.velocity.x != 0;
                  bool isGrounded = _isGrounded && !_isGoingDown;
                  _animator.SetBool(_jumpAnimation, !_isGrounded);

                  // Up Arrow
                  if (Input.GetKey(KeyCode.UpArrow) && isGrounded)
                  {
                        _animator.SetBool(_upAnimation, isMoving);
                        _animator.SetBool(_standUpAnimation, !isMoving);

                        if (isMoving && _lookingDirection == Vector2.right)
                        {
                              _transformAttackPoint.rotation = Quaternion.Euler(0, 0, 35);
                        }
                        else if (isMoving && _lookingDirection == Vector2.left)
                        {
                              _transformAttackPoint.rotation = Quaternion.Euler(0, 0, 150);
                        }
                        else
                        {
                              _transformAttackPoint.rotation = Quaternion.Euler(0, 0, 0);
                        }

                        if (!isMoving)
                        {
                              _transformAttackPoint.rotation = Quaternion.Euler(0, 0, 90);
                        }
                  }

                  if (Input.GetKeyUp(KeyCode.UpArrow))
                  {
                        _animator.SetBool(_upAnimation, false);
                        _animator.SetBool(_standUpAnimation, false);
                        _transformAttackPoint.rotation = Quaternion.Euler(0, 0, 0);
                  }

                  // Down Arrow
                  if (Input.GetKey(KeyCode.DownArrow) && isGrounded)
                  {
                        _animator.SetBool(_crouchAnimation, !isMoving);
                        _animator.SetBool(_downAnimation, isMoving);

                        if (!isMoving && isGrounded)
                        {
                              _isCrouch = true;

                              if (_rigidbody2D.velocity.x > 0)
                              {
                                    _transformAttackPoint.rotation = Quaternion.Euler(0, 0, 0);
                              }
                              else if (_rigidbody2D.velocity.x < 0)
                              {
                                    _transformAttackPoint.rotation = Quaternion.Euler(0, 0, 180);
                              }
                        }

                        if (isMoving && _lookingDirection == Vector2.right)
                        {
                              _transformAttackPoint.rotation = Quaternion.Euler(0, 0, -35);
                        }
                        else if (isMoving && _lookingDirection == Vector2.left)
                        {
                              _transformAttackPoint.rotation = Quaternion.Euler(0, 0, -150);
                        }
                        else
                        {
                              _transformAttackPoint.rotation = Quaternion.Euler(0, 0, 0);
                        }
                  }

                  if (Input.GetKeyUp(KeyCode.DownArrow))
                  {
                        _isCrouch = false;
                        _animator.SetBool(_crouchAnimation, false);
                        _animator.SetBool(_downAnimation, false);
                        _transformAttackPoint.rotation = Quaternion.Euler(0, 0, 0);
                  }

                  // Attack (Z)
                  if (Input.GetKey(KeyCode.Z) && isGrounded)
                  {
                        _animator.SetBool(_attackAnimation, true);

                        Attack();
                  }

                  if (Input.GetKeyUp(KeyCode.Z))
                  {
                        _animator.SetBool(_attackAnimation, false);
                  }

                  if (_rigidbody2D.velocity.x != 0 && _isGrounded)
                  {
                        _animator.SetBool(_runAnimation, true);
                        _animator.SetBool(_standUpAnimation, false);
                        _animator.SetBool(_crouchAnimation, false);
                  }
                  else
                  {
                        _animator.SetBool(_runAnimation, false);
                  }

                  if (_rigidbody2D.velocity.x > 0)
                  {
                        //_spriteRenderer.flipX = false;
                        transform.localScale = new Vector3(1, 1, 1);
                        _lookingDirection = Vector2.right;
                        _transformAttackPoint.rotation = Quaternion.Euler(0, 0, 0);
                  }
                  else if (_rigidbody2D.velocity.x < 0)
                  {
                        //_spriteRenderer.flipX = true;
                        transform.localScale = new Vector3(-1, 1, 1);
                        _lookingDirection = Vector2.left;
                        _transformAttackPoint.rotation = Quaternion.Euler(0, 0, 180);
                  }

                  if (_isInWater)
                  {
                        _animator.SetBool(_inWaterAnimation, true);
                  }
                  else
                  {
                        _animator.SetBool(_inWaterAnimation, false);
                  }

                  if (_rigidbody2D.velocity.y > 0 && _isGoingDown == false)
                  {
                        _boxCollider2D.isTrigger = true;
                        _animator.SetBool(_runAnimation, false);
                        _animator.SetBool(_upAnimation, false);
                        _animator.SetBool(_downAnimation, false);
                        _animator.SetBool(_standUpAnimation, false);
                        _animator.SetBool(_crouchAnimation, false);
                  }
                  else if (_isGoingDown == false)
                  {
                        _boxCollider2D.isTrigger = false;
                  }
            }

            private void Attack()
            {
                  if (_isCrouch) return;

                  if (Time.time >= nextFireTime)
                  {
                        nextFireTime = Time.time + 1f / _ammo.AmmoSpeed;

                        AudioManager.Instance.Play(_ammo.SoundName);                        

                        Instantiate(_ammo.AmmoPrefab, _transformAttackPoint.position, _transformAttackPoint.rotation);
                  }
            }

            private void Movement()
            {
                  Vector2 newVelocity = new Vector2(_horizontalMove * _moveSpeed * Time.fixedDeltaTime, _rigidbody2D.velocity.y);

                  _rigidbody2D.velocity = newVelocity;
            }

            private void Jump()
            {
                  if (_isGrounded && Input.GetButtonDown("Jump"))
                  {
                        _rigidbody2D.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Force);

                        _animator.SetBool(_jumpAnimation, true);

                        _isGrounded = false;
                  }
            }

            private void GoDown()
            {
                  if (_isGrounded && Input.GetKeyDown(KeyCode.X))
                  {
                        _boxCollider2D.isTrigger = true;

                        _isGoingDown = true;

                        Invoke(nameof(ResetCollider), 0.5f);
                  }
            }

            private void ResetCollider()
            {
                  _boxCollider2D.isTrigger = false;

                  _isGoingDown = false;
            }

            private void Die()
            {
                  if (_isCrouch) return;

                  if (_isDie) return;
                  _isDie = true;

                  _animator.SetTrigger("Die");

                  Debug.Log("Die");

                  _rigidbody2D.velocity = Vector2.zero;

                  EventManager.OnPlayerDead();                  

                  AudioManager.Instance.Play(AudioName.SFX_PlayerDie);
            }

            public void SetDeadline(float minX, float maxX, float minY, float maxY)
            {
                  _minX = minX;
                  _maxX = maxX;
                  _minY = minY;
                  _maxY = maxY;
            }

            public void TakeDamage(int damage)
            {
                  _health -= damage;

                  if (_health <= 0)
                  {
                        Die();
                  }
            }

            public void AddAmmo(Ammo ammo)
            {
                  _ammo = ammo;
            }

            private void OnTriggerEnter2D(Collider2D collision)
            {
                  if (collision.CompareTag("DeathZone"))
                  {
                        Die();
                  }
            }

            private void OnTriggerExit2D(Collider2D collision)
            {
                  if (collision.CompareTag("WaterZone"))
                  {
                        _isInWater = false;
                  }
            }

            private void OnCollisionEnter2D(Collision2D collision)
            {
                  if (collision.transform.CompareTag("WaterZone"))
                  {
                        _isInWater = true;
                  }
            }

            private void OnCollisionExit2D(Collision2D collision)
            {
                  if (collision.transform.CompareTag("WaterZone"))
                  {
                        _isInWater = false;
                  }
            }
      }
}

