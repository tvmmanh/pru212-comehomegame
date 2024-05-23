using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision2d : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Barricade")
        Debug.Log("Collision Barricade");
    }
    
}
