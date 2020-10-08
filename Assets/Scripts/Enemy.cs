using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 100;

    [Header("Lazer")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject enemyLazer;
    [SerializeField] float enemyLazerPush;

    [SerializeField] GameObject explosionVFX;

    [Header("Sound FX")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip lazerSFX;
    [SerializeField] float volume = 1f;

    [SerializeField] int scoreOnDeath = 25;

    GameSession gameSession;

    private void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots,maxTimeBetweenShots);
        gameSession = FindObjectOfType<GameSession>();
    }

    private void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
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
        GameObject lazer = Instantiate(enemyLazer, transform.position,Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(lazerSFX, Camera.main.transform.position, volume);
        lazer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyLazerPush);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
            ProcessHit(damageDealer);
        }
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.getDamageAmount();
        damageDealer.Hit();
        if (health <= 0)
        {
            GetDestroyed();
        }
    }

    private void GetDestroyed()
    {
        GameObject explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, volume);
        Destroy(explosion, 1f);
        Destroy(gameObject);
        gameSession.AddToScore(scoreOnDeath);
    }
}
