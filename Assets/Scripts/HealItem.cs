using UnityEngine;

public class HealItem : MonoBehaviour, IItemUseHandler
{
    NejikoController target;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<NejikoController>();
    }
    public void useItem()
    {
        target.RecoverLife();
        Destroy(this.gameObject);
    }
}