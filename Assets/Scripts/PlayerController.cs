using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ComeHomeGame
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D rb;
        [SerializeField] float Char_speed;
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
        }
    }
}
