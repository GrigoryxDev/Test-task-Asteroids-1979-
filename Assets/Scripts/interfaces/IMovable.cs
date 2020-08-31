using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    float Speed { get; set; }

    void Move();
}
