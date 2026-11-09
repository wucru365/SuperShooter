using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    public Animator animator;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            Debug.Log("Parameter: " + parameter.name +
                      " | Type: " + parameter.type);
        }
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Velocity", rb.velocity.magnitude);
        Debug.Log(animator.GetFloat("Velocity"));
    }
}
