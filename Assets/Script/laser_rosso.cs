using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_rosso : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	if(!GameController.gameover)
        	transform.position = new Vector2(transform.position.x - 10f * Time.deltaTime, transform.position.y);
        	
       	if(this.transform.position.x <= -10) {
       		Destroy(gameObject);
       	}
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Controlla se la collisione è avvenuta con un oggetto diverso dal laser stesso
        if (!collision.gameObject.CompareTag("laser_rosso"))
        {
            // Distruggi l'oggetto laser
            Destroy(gameObject);
        }
    }
}
