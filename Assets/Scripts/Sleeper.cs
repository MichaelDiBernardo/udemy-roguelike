using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleeper : MonoBehaviour
{
    public SpriteRenderer theBody;

    public bool IsAsleep
    {
        get
        {
            return !(theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy);
        }
    }

    public bool IsAwake
    {
        get
        {
            return !IsAsleep;
        }
    }
}
