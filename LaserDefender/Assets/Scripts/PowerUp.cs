using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] float speed = 3f;
    [SerializeField] AudioClip collectSFX;
    [SerializeField] [Range(0, 1)] float collectSFXVolume = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (!player) { return; }
        player.AddHealth(health);
        AudioSource.PlayClipAtPoint(collectSFX, Camera.main.transform.position, collectSFXVolume);
        Destroy(gameObject);
    }

    public float GetSpeed()
    {
        return speed;
    }
}
