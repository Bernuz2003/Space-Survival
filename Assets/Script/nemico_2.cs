using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nemico_2 : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject laserRossoPrefab;
    public GameObject esplosionePrefab; 
    
    public AudioClip[] audioClip;
    AudioSource audioSource;

    float timeBetweenBursts = 2f;
    float burstTimer = 0f;
    int shotsPerBurst = 2;
    int shotsFired = 0;
    
    bool movingUp = true;

    float movementSpeed;
    
    float vitaMax = 3;
    float vita;
    
    [SerializeField] Barra_vita barra_vita;

    void Start()
    {
        movingUp = (Random.value > 0.5f);
        movementSpeed = Random.Range(1f, 4f);
        
        vita = vitaMax;
        barra_vita = GetComponentInChildren<Barra_vita>();
        barra_vita.updateHealthBar(vita, vitaMax);
        
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.gameover)
        {
            if (transform.position.x > 7)
            {
                transform.position = new Vector2(transform.position.x - 2f * Time.deltaTime, transform.position.y);
            }

            float newYPos = transform.position.y + (movingUp ? 1 : -1) * movementSpeed * Time.deltaTime;
            newYPos = Mathf.Clamp(newYPos, -2.5f, 3.5f);
            transform.position = new Vector2(transform.position.x, newYPos);

            // Cambia direzione quando raggiunge il bordo superiore o inferiore dello schermo
            if (newYPos >= 3.5f || newYPos <= -2.5f)
            {
                movingUp = !movingUp;
            }

            // Logica di burst di sparo
            burstTimer += Time.deltaTime;
            if (burstTimer >= timeBetweenBursts)
            {
                burstTimer -= timeBetweenBursts;

                // Sparo un set di colpi
                StartCoroutine(ShootBurst());
            }
        }
    }
    
    private void danno(float valore)
    {
        vita -= valore;
        barra_vita.updateHealthBar(vita, vitaMax);
        if (vita <= 0)
        {
            audioSource.PlayOneShot(audioClip[1]);
            AttivaEsplosione();
            GameController.nemiciUccisi++;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        danno(1f);
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

    IEnumerator ShootBurst()
    {
        while (shotsFired < shotsPerBurst)
        {
            // Sparo un colpo
            shoot();
            shotsFired++;

            // Aspetta 0.2 secondi prima del prossimo colpo
            yield return new WaitForSeconds(0.3f);
        }

        // Resetta il contatore di colpi sparati
        shotsFired = 0;

        // Aspetta 2 secondi prima del prossimo burst
        yield return new WaitForSeconds(3f);
    }

    public void shoot()
    {
        Instantiate(this.laserRossoPrefab, shootingPoint.position, Quaternion.Euler(0f, 0f, 90f));
        audioSource.PlayOneShot(audioClip[0]);
    }
}

