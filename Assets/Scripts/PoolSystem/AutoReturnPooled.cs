using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnSystem
{
    /// <summary>
    /// This class automatically returns pooled object to its origin pool after a certain amount of time has passed.
    /// </summary>
    public class AutoReturnPooled : MonoBehaviour
    {
        [SerializeField] private float time;
        private Coroutine coroutine;

        private void OnEnable()
        {
            StartCountdown();
        }

        private void OnDisable()
        {
            StopCountdown();
        }

        public void StartCountdown()
        {
            coroutine = StartCoroutine(Countdown());
        }

        public void StopCountdown()
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }

        private IEnumerator Countdown()
        {
            yield return new WaitForSeconds(time);
            GetComponent<IPooledObject>().OnReturnToPool();
        }

    }
}