using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	private Animator theAnimator;
    public Transform firePoint;
    public GameObject bulletPrefab;

    public bool shoot;

    // Update is called once per frame
    void Update()
    {
    	
    	theAnimator = GetComponent<Animator>();
        if (Input.GetKeyDown(KeyCode.Space))
        {
        	StartCoroutine(Shoot());
        	 //calls the animation with given boolean
        } 
          //if not shooting, false. 
        
      
    }

    IEnumerator Shoot () ///bulletlogic
    {
        shoot = true;
    	Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        theAnimator.SetBool("Shoot", shoot); 
        yield return new WaitForSeconds(1);
        shoot = false;
        theAnimator.SetBool("Shoot", shoot);
    }
}
