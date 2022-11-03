using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float jumpForce;  //only need this if you want to control the height of the char jump
	private bool canMove;
	private Rigidbody2D theRB2D;  //IMPORTANT!! need a rigidbody component on the player
    
    public bool grounded;  //if player == grounded it can jump again
    public bool shoot;   //if player == shooting then it can do the shoot animation
    public LayerMask whatIsGrd; //layerMask ground is so that we can place in sprites/things in heirarchy that we want to quantify as ground
    public Transform grdChecker;
    public Transform Player;
    
    public float grdCheckerRad;

    public float airTime;
    public float airTimeCounter; //control height jump

    private bool ctrActive;
    private bool isDead;

    private Collider2D playerCol;
    public GameObject[] childObjs;
    public float shockForce;

    private bool FacingRight = true;

    private Animator theAnimator;

    public GameManager gm;  //If you want to add a title screen and end game screen
    private LivesManager theLM; 

    // Start is called before the first frame update
    void Start()
    {
        theLM = FindObjectOfType<LivesManager>();
        theRB2D = GetComponent<Rigidbody2D>(); //instannce of rigidbody
        theAnimator = GetComponent<Animator>();

        playerCol = GetComponent<Collider2D>();

        airTimeCounter = airTime;

        ctrActive = true; 
    }

    // Update is called once per frame
    void Update()
    {  
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f) //if input is left, can move, if input is right, can move
        {
        	canMove = true;
        }

        

    }

    private void FixedUpdate() 
    {
            grounded = Physics2D.OverlapCircle(grdChecker.position, grdCheckerRad, whatIsGrd);  //controlling jump height

            if( ctrActive == true)
            {

            MovePlayer();
            Jump();
       
            }
            
    

    }

    void MovePlayer() 
    {
    	if (canMove == true) 
    	{
    		theRB2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, theRB2D.velocity.y); //Vector 2 is essnetially a unity movement plaftorm on a x,y,z plane. 

    		//important to mention that "horizontal" keys on getAxisRaw apply to left and right arrows and 'a' and 'd' keys.

            theAnimator.SetFloat("Speed", Mathf.Abs(theRB2D.velocity.x));  //animation code
    	}


    	
        if(theRB2D.velocity.x > 0 && !FacingRight)  //more sprite animation code so that the character can turn around when going opposing direction 
        {
           // transform.localScale = new Vector2(1f, 1f);
          Flip();

        } else if (theRB2D.velocity.x  < 0 && FacingRight)
        {
           //transform.localScale = new Vector2(-1f, 1f);
          Flip();
        
        }
        
    }

    private void Flip()
    {

    	FacingRight = !FacingRight;

    	transform.Rotate(0f, 180f, 0f);
    }


    void Jump() 
    {
        if (grounded == true) 
        {

        if (Input.GetKeyDown(KeyCode.W) || Input.GetMouseButtonDown(0)) //code for regular jump
        {
            theRB2D.velocity = new Vector2(theRB2D.velocity.x, jumpForce);
        }
        }

        if (Input.GetKey(KeyCode.W) || Input.GetMouseButton(0) ) //ability to hold down for higher jump
        {
            if ( airTimeCounter > 0)
            {
                theRB2D.velocity = new Vector2(theRB2D.velocity.x, jumpForce);
                airTimeCounter -= Time.deltaTime;
            }
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetMouseButtonUp(0) )
        {
                airTimeCounter = 0;

        }

        if(grounded) 
        {
            airTimeCounter = airTime;
        }
        theAnimator.SetBool("Grounded", grounded);  //allows for no infinite jump 
    }

    private void OntriggerEnter2D(Collider2D other)  //end game if in contact with anything tagged "Danger"
    {
        if (other.gameObject.tag == "Danger")
        {
            Debug.Log("LOSING LIFE");
            gm.Reset();
            theLM.TakeLife();
            //playerDeath();
            //gm.GameOver();
        }

        if(other.gameObject.tag == "Souls")
        {
            Debug.Log("Gaining Soul");
            Destroy(other);
        }
    }

    void playerDeath()
    {
        isDead = true; 
        theAnimator.SetBool("Dead", isDead);

        ctrActive = false;

        playerCol.enabled = false; 
        foreach(GameObject child in childObjs)
        {
            child.SetActive(false);
        }

        theRB2D.gravityScale = 2.5f;
        theRB2D.AddForce(transform.up * shockForce, ForceMode2D.Impulse);

        StartCoroutine("PlayerRespawn");
    }

    IEnumerator PlayerRespawn()
    {
        yield return new WaitForSeconds(1.5f);
        isDead = false; 
        theAnimator.SetBool("Dead", isDead);

        playerCol.enabled = true; 
        foreach(GameObject child in childObjs)
        {
            child.SetActive(true);
        }

        theRB2D.gravityScale = 5f;

        yield return new WaitForSeconds(0.1f);
        ctrActive = true;
        gm.Reset();
    }



}
