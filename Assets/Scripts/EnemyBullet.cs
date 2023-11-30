using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    private Vector3 diraction;

    // Start is called before the first frame update
    void Start()
    {
        diraction = PlayerController.instance.transform.position - transform.position;
        diraction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += diraction * speed * Time.deltaTime;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);    
        if (other.tag == "Player")
        {
            PlayerHealthContoller.instance.DamagePlayer();
        }
        AudioManager.instance.PlaySFX(7);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
