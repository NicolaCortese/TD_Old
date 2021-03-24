using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 5;
    [SerializeField] Text healthText;
    [SerializeField] AudioClip DamagePlayerSFX;

    private void Start()
    {
        healthText.text = health.ToString();
        
    }

    
    private void OnTriggerEnter(Collider other)
    {
        health--;
        GetComponent<AudioSource>().PlayOneShot(DamagePlayerSFX);
        healthText.text = health.ToString();
    }

}
