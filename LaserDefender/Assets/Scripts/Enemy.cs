using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //config
    [Header("Config")]
    [SerializeField] float health = 100f;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 2f;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] GameObject projectile;
    [SerializeField] int scorePerKill = 50;
    [SerializeField] float powerUpSpawnChance = 10f;
    [SerializeField] List<PowerUp> powerUps;

    [Header("Effects")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject onHitVFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 0.5f;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] float shootSFXVolume = 0.75f;
    [SerializeField] AudioClip onHitSFX;
    [SerializeField] [Range(0, 1)] float onHitSFXVolume = 1f;

    GameSession gameSession;
    Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShot();
    }

    private void CountDownAndShot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(
        projectile,
        transform.position,
        Quaternion.identity) as GameObject;
        
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);

        if(laser.GetComponent<Mine>())
        {
            laser.GetComponent<Mine>().projectileSpeed = projectileSpeed;
        }

        AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        damageDealer.Hit();
        health -= damageDealer.GetDamage();
        if (health <= 0)
        {
            Die();
        }
        if (onHitVFX != null && onHitSFX != null)
        {
            PerfromHitEffects();
        }
    }

    private void PerfromHitEffects()
    {
        GameObject sparks = Instantiate(onHitVFX, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(onHitSFX, Camera.main.transform.position, onHitSFXVolume);
        Destroy(sparks, 0.5f);
    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
        Destroy(explosion, 1f);
        gameSession.AddToScore(scorePerKill);

        SpawnPowerUp();
    }

    private void SpawnPowerUp()
    {
        if (player.GetHealth() < player.GetMaxHealth() &&
            !FindObjectOfType<PowerUp>() &&
            Random.Range(0f, 100f) <= powerUpSpawnChance)
        {
            PowerUp powerUp = Instantiate(powerUps[0], transform.position, Quaternion.identity);
            powerUp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -powerUp.GetSpeed());
        }
    }

    public int GetScorePerKill() { return scorePerKill; }
}