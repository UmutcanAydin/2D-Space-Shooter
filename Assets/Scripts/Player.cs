using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int health = 100;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float boundaryPadding = 0.5f;

    [Header("Lazer")]
    [SerializeField] GameObject lazer;
    [SerializeField] float lazerPush = 25f;
    [SerializeField] float waitBetweenBullets = 0.1f;

    [Header("Sound FX")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip lazerSFX;
    [SerializeField] float volume = 1f;

    [SerializeField] float afterDeathSeconds = 2f;

    [SerializeField] GameObject explosionVFX;


    Coroutine fireCoroutine;
    float minX,maxX,minY,maxY;
    SceneManagement sceneManager;

    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        SetupMoveBoundries();
        sceneManager = FindObjectOfType<SceneManagement>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        PrepareFireCoroutine();  
    }

    private void PrepareFireCoroutine()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = StartCoroutine(Fire());
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }
    }

    private void SetupMoveBoundries()
    {
        Camera gameCamera = Camera.main;

        minX = gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).x + boundaryPadding; //ViewportToWorldPoint() takes a Vector3, not Vector2
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - boundaryPadding;

        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + boundaryPadding; 
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - boundaryPadding; 
    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float newXPos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);

        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        float newYPos = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private IEnumerator Fire()
    {

        while (true)
        {
            GameObject newLazer = Instantiate(lazer, transform.position, Quaternion.identity) as GameObject;
            AudioSource.PlayClipAtPoint(lazerSFX, Camera.main.transform.position, volume);
            newLazer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, lazerPush);
            yield return new WaitForSeconds(waitBetweenBullets);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
            ProcessHit(damageDealer);
        }
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.getDamageAmount();
        gameSession.DecreaseHealth(damageDealer.getDamageAmount());
        damageDealer.Hit();
        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, volume);
            GameObject explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
            Destroy(gameObject);
            sceneManager.Invoke("loadFailScene",2);
        }
    }

    public int getHealth()
    {
        return health;
    }
}
