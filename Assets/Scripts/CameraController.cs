using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    public float followSpeed = 2f;
    private float lookAhead;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(player.position.x, player.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
        //lookAhead = Mathf.Lerp(lookAhead,(aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }
    
}
