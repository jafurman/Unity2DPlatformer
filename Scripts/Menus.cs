using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour
{
    // Start is called before the first frame update
   public void Replay()
   {
   		FindObjectOfType<GameManager>().Reset();
   }

   public void QuitGame()
   {
   		Application.Quit();
   }
}
