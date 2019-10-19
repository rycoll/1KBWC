using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObject : MonoBehaviour
{

    void Update()
    {
        this.transform.Rotate(0f, 2.5f, 0f, Space.Self);
    }
}
