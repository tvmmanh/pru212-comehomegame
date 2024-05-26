using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ComeHomeGame
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Vector2 moveInput;
        private CapsuleCollider2D mycapsuleCollider;
        [SerializeField] float Char_speed = 10f;
        [SerializeField] float ClimbSpeed = 2f;
        [SerializeField] float wSpeed;
        [SerializeField] float rSpeedl;
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * wSpeed, rb.velocity.y);
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x,wSpeed);
            }
            //ClimbLadder();
        }
        //void ClimbLadder()
        //{
        //    if (!mycapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        //    {
        //        return;
        //    }
        //    Vector2 climbVelocity = new Vector2(rb.velocity.x, moveInput.y * ClimbSpeed);
        //    rb.velocity = climbVelocity;
        //}
    }
    
}
