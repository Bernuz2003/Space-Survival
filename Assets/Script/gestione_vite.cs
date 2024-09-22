using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GestioneVite : MonoBehaviour
{
    public int viteRimanenti = 6;
    public Sprite spriteRettangoloRosso;
    
    public void RiduciVite() {
	    if (viteRimanenti > 0)
	    {
		viteRimanenti--;
		AggiornaVite();
	    }
    }

    void AggiornaVite()
    {
	    // Recupera i figli dell'oggetto "Vita"
	    Transform[] vite = new Transform[transform.childCount];
	    for (int i = 0; i < transform.childCount; i++)
	    {
		vite[i] = transform.GetChild(i);
	    }

	    // Disattiva uno dei rettangolini rossi in base alle vite rimaste
	    for (int i = 0; i < viteRimanenti; i++)
	    {
		vite[i].GetComponent<SpriteRenderer>().sprite = spriteRettangoloRosso;
	    }
    }

}

