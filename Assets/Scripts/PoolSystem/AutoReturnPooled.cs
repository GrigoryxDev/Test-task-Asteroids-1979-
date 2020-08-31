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

        private void OnEnable()
        {
            StartCoroutine(StartCountdown());
        }

        private IEnumerator StartCountdown()
        {
            yield return new WaitForSeconds(time);
            GetComponent<IPooledObject>().OnReturnToPool();
        }

    }
}