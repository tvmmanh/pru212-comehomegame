using UnityEngine;

public class ProjectTile : MonoBehaviour
{

    //code sai logic - nen coi lai

    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;

    private Animator anm;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        anm = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return; 
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(0, movementSpeed, 0);

        lifetime += Time.deltaTime;
        if(lifetime > 5)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anm.SetTrigger("explode");

        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Health>().TakeDamage(1);
        }
    }

    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(transform.localScale.x, -localScaleX, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
