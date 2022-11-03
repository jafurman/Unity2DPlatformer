using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	private float dirY;
	private float moveSpeed;
	private Rigidbody2D rb;
	private Vector2 localScale;

    private bool FacingDown = true;

    private Animator theAnim;
    private bool isDead;

    public static float health = .6f;

    public GameObject deathEffect;

    public GameObject nothing;



public void TakeDamage (float damage)
{
    damage = .2f;
    health -= damage;

    if (health <= 0) 
    {
         isDead = true; 
        theAnim.SetBool("Dead", isDead);
        Die();
    }
}

void Die()
{
    StartCoroutine(stopAnim());
    Destroy(gameObject);

}

    IEnumerator stopAnim()
    {
       Instantiate(deathEffect, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        isDead = false; 
        Instantiate(nothing, transform.position, Quaternion.identity);
        theAnim.SetBool("Dead", isDead);
        
    }

    // Start is called before the first frame update
    void Start()
    {

        theAnim = transform.parent.GetComponent<Animator>();
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        dirY = 1f;
        moveSpeed = 1f;

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    	if ( collision.GetComponent<Wall>())
    	{

    		dirY *= -1f;
            Flip();
    	}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2( rb.velocity.x, dirY * moveSpeed);
    }
    private void Flip()
    {

        FacingDown = !FacingDown;

        transform.Rotate(180f, 0f, 0f);
    }
}
