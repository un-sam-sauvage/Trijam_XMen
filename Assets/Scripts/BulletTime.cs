using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletTime : MonoBehaviour
{
    public bool isOn;
    public float timer;

    private float saveTimer;

    [HideInInspector] public UnityEvent startBulletTimeEvent;
    [HideInInspector] public UnityEvent stopBulletTimeEvent;

    public static BulletTime instance;

    void Awake()
    {
        startBulletTimeEvent = new UnityEvent();
        stopBulletTimeEvent = new UnityEvent();

        if (instance == null)
        {
            instance = this;
        }
    }
    
    void Start()
    {
        saveTimer = timer;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("BulletTime") && timer > 0)
        {
            if (!isOn)
            {
                isOn = true;
                startBulletTimeEvent.Invoke();
                AudioManager.instance.Play("EnterBulletTime");
            }
            else
            {
                isOn = false;
                stopBulletTimeEvent.Invoke();
                AudioManager.instance.Play("LeaveBulletTime");
            }
        }

        if (isOn)
        {
            //TODO Changer vitesse des bullets
            //TODO rendre les bullets possibles de grab
            
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                stopBulletTimeEvent.Invoke();
                AudioManager.instance.Play("LeaveBulletTime");
                isOn = false;
            }
        }
        else
        {
            //TODO Remettre vitesse de base
            if (timer < saveTimer)
            {
                timer += Time.deltaTime * 0.85f;
                if (timer > saveTimer)
                {
                    timer = saveTimer;
                }
            }
        }
        
        
    }
}
