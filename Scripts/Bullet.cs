using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	public float speed = 20f;
	public Rigidbody2D rb;
    public float damage = .2f; 
	public GameObject impactEffect;
    // Start is called before the first frame update
    void Start()

    {
        rb.velocity = transform.right * speed;  //rigidbody, please move right accordinfg to our speed
    }


    void OnTriggerEnter2D (Collider2D hitInfo)  //when anything happens when bullet hits
    {

    	Enemy enemy = hitInfo.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }


    	Instantiate(impactEffect, transform.position, transform.rotation);
    	Destroy(gameObject); //if it hits another rigidBody it goes away
        StartCoroutine(DestroyImp());
        
    }

    IEnumerator DestroyImp()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(impactEffect);
    }
}
