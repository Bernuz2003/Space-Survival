using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip backgroundMusic;  // Trascina l'audio clip della musica di sottofondo nell'Inspector
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Assegna l'audio clip della musica di sottofondo all'AudioSource
        audioSource.clip = backgroundMusic;

        // Avvia la riproduzione della musica di sottofondo
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // Puoi aggiungere eventuali controlli aggiuntivi o manipolazioni dell'audio qui, se necessario.
    }
}

