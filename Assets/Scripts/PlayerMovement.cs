using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    private float _saveMovementSpeed;
    private float objectWidth, objectHeight;
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        gm = GameManager.Instance;
        _saveMovementSpeed = movementSpeed;
        BulletTime.instance.startBulletTimeEvent.AddListener(StartBulletTime);
        BulletTime.instance.stopBulletTimeEvent.AddListener(StopBulletTime);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, -gm.screenBounds.x + objectWidth, gm.screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, -gm.screenBounds.y + objectHeight, gm.screenBounds.y - objectHeight);
        transform.position = viewPos;
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("touché");
        if (other.gameObject.CompareTag("Bullet"))
        {
            GameManager.Instance.DeathPlayer();
            Time.timeScale = 0f;
        }
    }
}