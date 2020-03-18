using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float destroyTime = .4f;
    private void Start()
    {
        //destroy explosion after time
        Destroy(gameObject, destroyTime);
    }
    private void Update()
    {
        //Simple rotation animation.Best choice for this animations use .tween
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
    }
}
