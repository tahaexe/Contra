using System;
using UnityEngine;

namespace Contra
{
      public class EventManager : MonoBehaviour
      {
            public static event Action PlayerDead;
            public static event Action PlayerWin;
            public static event Action<float> BossHealth;

            public static void OnPlayerDead() => PlayerDead?.Invoke();
            public static void OnPlayerWin() => PlayerWin?.Invoke();
            public static void OnBossHealth(float health) => BossHealth?.Invoke(health);
      }
}
