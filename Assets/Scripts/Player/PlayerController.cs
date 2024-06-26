using Cainos.PixelArtPlatformer_VillageProps;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace ComeHomeGame
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float jumpPower;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask wallLayer;
        private Rigidbody2D body;
        private Animator anm;
        private BoxCollider2D boxCollider;
        private float wallJumpCooldown;
        private float horizontalInput;
        private float chosenScale = 6.0f;

        private Chest currentChest;

        [Header("SFX")]
        [SerializeField] private AudioClip jumpSound;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            anm = GetComponent<Animator>();
            boxCollider = GetComponent<BoxCollider2D>();
        }

        public void SetCharacterSize(float scale)
        {
            chosenScale = scale;
            UpdateCharacterScale(horizontalInput);
        }

        private void Update()
        {
            horizontalInput = Input.GetAxis("Horizontal");

            UpdateCharacterScale(horizontalInput);

            //Set animator parameters
            anm.SetBool("run", horizontalInput != 0);
            anm.SetBool("jump", isGrounded());
            anm.SetBool("land", !Input.GetKey(KeyCode.DownArrow));

            //Wall Jump Logic
            if(wallJumpCooldown > 0.2f)
            {
                body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

                if(onWall() && !isGrounded())
                {
                    body.gravityScale = 0;
                    body.velocity = Vector2.zero;
                }
                else
                {
                    body.gravityScale = 7;
                }

                if (Input.GetKey(KeyCode.Space))
                {
                    Jump();

                    if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
                    {
                        SoundManage.instance.PlaySound(jumpSound);
                    }
                }
            } 
            else
            {
                wallJumpCooldown += Time.deltaTime;
            }

            if(Input.GetKey(KeyCode.E) && currentChest != null)
            {
                if(currentChest.IsOpened)
                {
                    currentChest.Close();
                }
                else
                {
                    currentChest.Open();
                }
            }
        }

        private void UpdateCharacterScale(float horizontalInput)
        {
            //Flip character
            if (horizontalInput > 0.01f)
            {
                // transform.localScale = Vector3.one;
                transform.localScale = new Vector3(chosenScale, chosenScale, chosenScale);
            }
            else if (horizontalInput < -0.01f)
            {
                transform.localScale = new Vector3(-chosenScale, chosenScale, chosenScale);
            }
        }

        private void Jump()
        {
            if (isGrounded())
            {
                body.velocity = new Vector2(body.velocity.x, jumpPower);
                anm.SetTrigger("Jump");
            }
            else if(onWall() && !isGrounded())
            {
                if(horizontalInput == 0)
                {
                    body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                    transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x) * chosenScale, chosenScale, chosenScale);
                }
                else
                {
                    body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
                }
                wallJumpCooldown = 0;
            }
        }

        private bool isGrounded()
        {
            RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size * chosenScale, 0, Vector2.down, 0.1f * chosenScale, groundLayer);
            return raycastHit.collider != null;
        }
        private bool onWall()
        {
            RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size * chosenScale, 0, new Vector2(transform.localScale.x, 0), 0.1f * chosenScale, wallLayer);
            return raycastHit.collider != null;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Chest"))
            {
                currentChest = collision.gameObject.GetComponent<Chest>();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Chest"))
            {
                currentChest = null;
            }
        }

        public bool canAttack()
        {
            return horizontalInput == 0 && isGrounded() && !onWall();
        }
    }
}
