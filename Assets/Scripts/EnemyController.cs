using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;

    public float rangeToChasePlayer;
    private Vector3 moveDirection;

    public Animator anim;
    public int health = 150;

    public GameObject[] deathSplatters;
    public GameObject hitEffect;

    public bool shouldShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;
    public SpriteRenderer theBody;

    public float shootRange;
    Vector3 val = new Vector3(-1.5f, 1f, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy) 
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
            {
                moveDirection = PlayerController.instance.transform.position - transform.position;
            }
            else
            {
                moveDirection = Vector3.zero;
            }

            moveDirection.Normalize();

            theRB.velocity = moveDirection * moveSpeed;


            if (shouldShoot && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < shootRange)
            {
                fireCounter -= Time.deltaTime;
                if (fireCounter < 0)
                {
                    fireCounter = fireRate;
                    Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
                    AudioManager.instance.PlaySFX(5);
                }
            }
        } else 
        {
            theRB.velocity = Vector3.zero;
        }
        if (moveDirection != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    public void DamageEnemy(int damage)
    {

        health -= damage;

        AudioManager.instance.PlaySFX(1);

        Instantiate(hitEffect, transform.position - val, transform.rotation);

        if (health <= 0)
        {
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(0);
            int selectedSplatter = Random.Range(0, deathSplatters.Length);
            
            Instantiate(deathSplatters[selectedSplatter], transform.position - val, transform.rotation);
        }
    }
}
