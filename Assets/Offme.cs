using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offme : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Off",1f);
    }

    // Update is called once per frame
    public void Off()
    {
        Destroy(this.gameObject);
    }
}
