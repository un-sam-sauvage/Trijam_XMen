using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    private float _saveMovementSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        _saveMovementSpeed = movementSpeed;
        BulletTime.instance.startBulletTimeEvent.AddListener(StartBulletTime);
        BulletTime.instance.stopBulletTimeEvent.AddListener(StopBulletTime);
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

    public void StartBulletTime()
    {
        movementSpeed = 0f;
    }

    public void StopBulletTime()
    {
        movementSpeed = _saveMovementSpeed;
    }
}
