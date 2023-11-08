using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpItem : MonoBehaviour, IItemUseHandler
{
    NejikoController target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<NejikoController>();
    }
    public void useItem()
    {
        target.JumpSpeedUp();
    }
}
