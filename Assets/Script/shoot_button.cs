using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_button : MonoBehaviour
{
	Airship airship;
	AudioSource audioSparo;
	
	void Start()
    	{
        	airship = FindObjectOfType<Airship>(); // Trova l'istanza di Airship nell scena
        	audioSparo = airship.GetComponent<AudioSource>();
    	}

	public void clickShoot() {
		airship.shoot();
		airship.SetIsShooting(true);
		PlayShootSound(); 
	}
	
	void PlayShootSound()
    {
        if (audioSparo != null)
        {
            // Controlla se l'AudioSource è presente nell'Airship
            audioSparo.Play(); // Riproduci l'audio
        }
        else
        {
            Debug.LogWarning("AudioSource non trovato sull'oggetto Airship.");
        }
    }
}
