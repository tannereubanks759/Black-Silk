using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spideranimfunctions : MonoBehaviour
{
    void destroyobject()
    {
        Destroy(gameObject.GetComponentInParent<AISpider>().gameObject);
    }
}
