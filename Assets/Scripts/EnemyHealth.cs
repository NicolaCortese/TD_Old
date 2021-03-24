using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] ParticleSystem EnemyDeathFX;
    [SerializeField] ParticleSystem hitFX;
    [SerializeField] int health = 10;
    [SerializeField] AudioClip EnemyHitSFX;
    [SerializeField] AudioClip EnemyDeathSFX;

    AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        AddBoxCollider();
    }

    private void AddBoxCollider()
    {
        Collider bc = gameObject.AddComponent<BoxCollider>();
        bc.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {

        health--;
        ParticleSystem hitfx = Instantiate(hitFX, transform.position, Quaternion.identity);
        myAudioSource.PlayOneShot(EnemyHitSFX);
        Destroy(hitfx.gameObject, hitfx.main.duration);
        if (health <= 0)
        {
            KillEnemy();
        }
    }

    public void KillEnemy()
    {
        ParticleSystem fx = Instantiate(EnemyDeathFX, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(EnemyDeathSFX, Camera.main.transform.position, 1f);
      
        Destroy(fx.gameObject, fx.main.duration);
        Destroy(gameObject);
    }
   
}
