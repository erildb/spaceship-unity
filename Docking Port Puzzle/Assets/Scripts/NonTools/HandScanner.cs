using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScanner : MonoBehaviour
{
    public void Fall()
    {
        transform.GetComponent<Rigidbody>().isKinematic = false;
    }
}
