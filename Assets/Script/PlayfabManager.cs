using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;

public class PlayfabManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject loginWindow;
    public Button openLoginButton;
    public Button closeButton;
    public Button loginButton;
    public Button registerButton;
    public Text messageText;

    void Start()
    {
        loginWindow.SetActive(false);
        openLoginButton.onClick.AddListener(OpenLoginWindow);
        closeButton.onClick.AddListener(CloseLoginWindow);

        loginButton.onClick.AddListener(OnLoginButtonClicked);
        registerButton.onClick.AddListener(OnRegisterButtonClicked);

        // Inizializza il messaggio come vuoto
        messageText.text = "";
    }

    void OpenLoginWindow()
    {
        loginWindow.SetActive(true);
    }

    void CloseLoginWindow()
    {
        loginWindow.SetActive(false);
    }

    void OnLoginButtonClicked()
    {
        messageText.text = "Login effettuato con successo";
        StartCoroutine(ClearMessageAfterDelay(3f));
    }

    void OnRegisterButtonClicked()
    {
        messageText.text = "Registrazione e Login effettuati con successo";
        StartCoroutine(ClearMessageAfterDelay(3f));
        loginWindow.SetActive(false);
    }

    IEnumerator ClearMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText.text = "";
    }
}
