using UnityEngine;

namespace Contra
{
      [CreateAssetMenu(fileName = "New Ammo", menuName = "Contra/Ammo/New Ammo")]
      public class Ammo : ScriptableObject
      {
            public AmmoType AmmoType;
            public Transform AmmoPrefab;
            public AudioName SoundName;
            public float AmmoSpeed;
      }
}
