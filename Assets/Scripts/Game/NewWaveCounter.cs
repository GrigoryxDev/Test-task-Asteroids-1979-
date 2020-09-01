using System.Collections;
using System.Collections.Generic;
using Scripts.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Game
{
    public class NewWaveCounter : MonoBehaviour
    {
        [SerializeField] private Text newWaveCountdownText;


        public void StartCountdown(float timeToWait)
        {
            gameObject.SetActive(true);
            StartCoroutine(Countdown(timeToWait));
        }

        private IEnumerator Countdown(float time)
        {
            while (time > 0)
            {
                UpdateText(time);
                yield return new WaitForSeconds(1f);
                time--;
            }
            gameObject.SetActive(false);
            App.Instance.GameManager.StartNewWave();
        }
        private void UpdateText(float time)
        {
            newWaveCountdownText.text = time.ToString("0");
        }

    }
}