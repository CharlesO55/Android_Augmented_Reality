using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    public void OnFire()
    {
        animator.SetTrigger("Fire");
    }
}
