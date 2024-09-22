using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public Button pauseButton;

    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);
        pauseButton.onClick.AddListener(PauseGame);
    }

    void PauseGame()
    {
        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0f; // Ferma il tempo di gioco
            pausePanel.SetActive(true);
        }
    }

    void Update()
    {
        if (isPaused && Input.GetMouseButtonDown(0))
        {
            // Riprendi il gioco quando lo schermo viene toccato
            StartCoroutine(ResumeGame());
        }
    }

    IEnumerator ResumeGame()
    {
        // Disabilita l'input per evitare doppi click
        isPaused = false;

        // Ottieni il componente Text del messaggio di pausa
        Text pauseMessage = pausePanel.GetComponentInChildren<Text>();

        // Countdown di 3 secondi
        for (int i = 3; i > 0; i--)
        {
            pauseMessage.text = i.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }

        // Messaggio "GO!"
        pauseMessage.text = "GO!";
        yield return new WaitForSecondsRealtime(1f);

        // Ripristina il gioco
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Riprende il tempo di gioco

        // Ripristina il messaggio originale
        pauseMessage.text = "PAUSA\nToccare lo schermo per riprendere il gioco";
    }
}
