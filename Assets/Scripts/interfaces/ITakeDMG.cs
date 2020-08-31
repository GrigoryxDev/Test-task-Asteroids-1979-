using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeDMG
{
    int HP { get; set; }

    void TakeDMG();

    void OnObjectDestroy();
}
