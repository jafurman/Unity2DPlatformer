using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public GameManager gm;
    public LivesManager lm;

    private void OnTriggerEnter2D(Collider2D other)
    {
    	if (other.gameObject.tag == "Player")
    	{
    		Debug.Log("SPIDER BITE");
            gm.Reset();
    		lm.TakeLife();
    		//gm.GameOver();
    	}
    }
}
