using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToChangehisFace : MonoBehaviour
{

    public Animator animator;
    public float qlqrcoisa;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        qlqrcoisa = GameObjectAccess.Player.AirRatio;
        animator.SetFloat("O2Meter", qlqrcoisa);
    }
}
