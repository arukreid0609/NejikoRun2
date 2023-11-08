using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem2 : MonoBehaviour, IItemUseHandler
{
    public NejikoController target;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<NejikoController>();
    }
    public void useItem()
    {
        target.RecoverLife();
        Destroy(this.gameObject);
    }

}
