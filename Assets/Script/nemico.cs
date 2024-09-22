using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nemico : MonoBehaviour
{

    public Transform shootingPoint;
    public GameObject laserRossoPrefab;  
    public GameObject esplosionePrefab; 
    
    public AudioClip[] audioClip;
    AudioSource audioSource;
    
    float shootTimer;
    float shootRate = 2f;
    
    float movementSpeed;
    bool movingUp = true;
    
    void Start() {
    	movingUp = (Random.value > 0.5f);
    	movementSpeed = Random.Range(1f,4f);
    
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    	if(!GameController.gameover) {
		    if(this.transform.position.x > 7) {
		    	transform.position = new Vector2(transform.position.x - 2f * Time.deltaTime, transform.position.y);
		    }
		    
		    float newYPos = transform.position.y + (movingUp ? 1 : -1) * movementSpeed * Time.deltaTime;
		    newYPos = Mathf.Clamp(newYPos, -2f, 3f);  // Impedisce al nemico di uscire dagli estremi verticali dello schermo
		    transform.position = new Vector2(transform.position.x, newYPos);

		    // Cambia direzione quando raggiunge il bordo superiore o inferiore dello schermo
		    if (newYPos >= 3f || newYPos <= -2f)
		    {
			movingUp = !movingUp;
		    }
		
		    shootTimer += Time.deltaTime;
		    if (shootTimer >= shootRate)
		    {
		    	shootTimer -= shootRate;
			shoot();
		    }
	   }
    }
    
        private void OnCollisionEnter2D(Collision2D collision)
    {
    	    GameController.nemiciUccisi++;
    	    audioSource.PlayOneShot(audioClip[1]);
    	    AttivaEsplosione();
    	    Destroy(gameObject);
    }
    
    private void AttivaEsplosione()
    {
	    if (esplosionePrefab != null)
	    {
		// Istanziare l'esplosione nel punto della morte del nemico
		GameObject esplosione = Instantiate(esplosionePrefab, transform.position, Quaternion.identity);

		// Avvia manualmente la riproduzione del sistema di particelle
		ParticleSystem particleSystem = esplosione.GetComponent<ParticleSystem>();
		if (particleSystem != null)
		{
		    particleSystem.Play();
		}
	    }
	    else
	    {
		Debug.LogError("Prefab del sistema di particelle non assegnato al nemico!");
	    }

	    // Altre azioni di cleanup o distruzione del nemico...
	    Destroy(gameObject);
    }

    
    public void shoot() {
    	Instantiate(this.laserRossoPrefab, shootingPoint.position, Quaternion.Euler(0f,0f,90f)); 
    	audioSource.PlayOneShot(audioClip[0]);
    }
}
