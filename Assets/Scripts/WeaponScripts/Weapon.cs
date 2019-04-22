﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float firerate;
    [SerializeField] private int ammo;
    public bool isHeld = false;
    [SerializeField]bool onGround = false;
    [SerializeField] Transform EndOfGun;
    private bool onCoolDown = false;
    public float coolDownTime;
    public float timestamp;
    public int Damage;
    public Bullet bulletGeneric;
    public GameObject smokeParticle;
    [SerializeField] AudioSource audioPlayer;
    [SerializeField] AudioClip[] soundEffects;
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Platform")) { onGround = true; }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Platform")) {onGround = false; }
    }

    void Update()
    {
        if (!onGround && !isHeld)
        {
            transform.Translate(new Vector2(0, -.02f));
        }
        if (onCoolDown)
        {
            coolDownTime -= Time.deltaTime;
            if (coolDownTime <= 0)
            {
                onCoolDown = false;
            }
        }
        if (!isHeld && ammo == 0)
        {
            Destroy(gameObject);
        }
    }

    public void Shoot()
    {
        if (!onCoolDown && ammo != 0)
        {
            Bullet bulletClone = Instantiate(bulletGeneric, transform.position, transform.rotation);
            bulletClone.owner = transform.parent.gameObject;
            ammo--;
            onCoolDown = true;
            coolDownTime = firerate;
            audioPlayer.PlayOneShot(soundEffects[0]);
        }
        if(ammo == 0)
        {
            audioPlayer.PlayOneShot(soundEffects[1]);
            Instantiate(smokeParticle, EndOfGun.position, transform.rotation);
        }
    }
}
