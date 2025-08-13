using UnityEngine;
using Nakama;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        // Verifica se o jogador está autenticado
        if (NakamaManager.Instance == null || NakamaManager.Instance.Session == null)
        {
            Debug.LogError("Player not authenticated! Returning to login...");
            SceneManager.LoadScene("Login");
            return;
        }
        
        // Inicializa o player aqui
        InitializePlayer();
    }

    private void InitializePlayer()
    {
        // Sua lógica para criar/posicionar o jogador
        Debug.Log($"Player initialized: {NakamaManager.Instance.Session.Username}");
    }
}