using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    private float saveMovementSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        saveMovementSpeed = movementSpeed;
        
        GetComponent<BulletTime>().startBulletTimeEvent.AddListener(BulletTime);
        GetComponent<BulletTime>().stopBulletTimeEvent.AddListener(StopBulletTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        
        Vector2 dir = new Vector2(hor, ver).normalized; 
        
        transform.Translate(dir * movementSpeed * Time.deltaTime);
    }

    public void BulletTime()
    {
        movementSpeed = 0f;
    }

    public void StopBulletTime()
    {
        movementSpeed = saveMovementSpeed;
    }
}
