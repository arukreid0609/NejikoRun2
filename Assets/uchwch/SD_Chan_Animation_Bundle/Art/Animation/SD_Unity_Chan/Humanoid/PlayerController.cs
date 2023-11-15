using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float run = Input.GetAxis("Vertical");
        animator.SetFloat("Run", run);

        if (Input.GetKeyDown("space"))
        {
            animator.SetTrigger("Jump");
        }
    }
}
