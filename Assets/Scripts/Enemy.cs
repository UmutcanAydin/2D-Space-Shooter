using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject enemyLazer;
    [SerializeField] float enemyLazerPush;
    [SerializeField] GameObject explosionVFX;

    private void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots,maxTimeBetweenShots);
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
        Destroy(explosion, 1f);
        Destroy(gameObject);
    }
}
