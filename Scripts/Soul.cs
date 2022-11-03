using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{

	public int soulValue = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
    	if (collision.tag == "Player")
    	{
    		Destroy(gameObject);
    		ScoreManager.instance.ChangeScore(soulValue);
    	}
    }
}
