using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvincible : MonoBehaviour
{
    public float invincibleTime { get; set; }


    public IEnumerator Countdown()
    {
        yield return new WaitForSeconds(invincibleTime);
        RemoveInvincible();
    }

    private void RemoveInvincible()
    {
        Destroy(this);
    }
}
