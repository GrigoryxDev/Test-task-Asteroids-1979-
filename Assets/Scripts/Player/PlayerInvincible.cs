using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvincible : MonoBehaviour
{
    public float InvincibleTime { get; set; }
    public IEnumerator Countdown()
    {
        yield return new WaitForSeconds(InvincibleTime);
        RemoveInvincible();
    }

    private void RemoveInvincible()
    {
        gameObject.SetActive(false);
    }
}
