﻿using System.Collections;
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

    void Awake()
    {
        startBulletTimeEvent = new UnityEvent();
        stopBulletTimeEvent = new UnityEvent();
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
            }
            else
            {
                isOn = false;
                stopBulletTimeEvent.Invoke();
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