using UnityEngine;

namespace Contra
{
      public class Power : MonoBehaviour
      {
            [SerializeField] private Ammo _ammo;

            private void OnTriggerEnter2D(Collider2D other)
            {
                  if (other.CompareTag("Player"))
                  {
                        var player = other.GetComponent<Player>();

                        if (player != null)
                        {
                              AudioManager.Instance.Play(AudioName.SFX_Power_Take);

                              player.AddAmmo(_ammo);
                        }

                        Destroy(gameObject);
                  }
            }
      }
}
