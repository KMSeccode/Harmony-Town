using UnityEngine;
using UnityEngine.SceneManagement;
using Nakama;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class NakamaManager : MonoBehaviour
{
    public static NakamaManager Instance;
    
    // Referências que devem persistir
    public IClient Client { get; private set; }
    public ISession Session { get; private set; }
    public ISocket Socket { get; private set; }
    
    private string _matchId;
    
    // Referências de UI (serão buscadas na cena atual)
    [System.NonSerialized] public GameObject loginPanel;
    [System.NonSerialized] public TMP_InputField usernameInput;
    [System.NonSerialized] public Button loginButton;
    
    private string currentScene;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.name;
        
        // Só busca referências de UI na cena de login
        if (currentScene == "Login")
        {
            FindUIReferences();
            SetupLoginUI();
        }
        else
        {
            // Limpa referências quando não for a cena de login
            loginPanel = null;
            usernameInput = null;
            loginButton = null;
        }
    }

    private void FindUIReferences()
    {
        loginPanel = GameObject.Find("LoginPanel");
        usernameInput = FindObjectOfType<TMP_InputField>();
        loginButton = GameObject.Find("LoginButton")?.GetComponent<Button>();
        
        if (loginButton == null)
        {
            Debug.LogWarning("Botão de login não encontrado na cena atual");
        }
    }

    private void SetupLoginUI()
    {
        if (loginButton != null)
        {
            loginButton.onClick.RemoveAllListeners();
            loginButton.onClick.AddListener(OnLoginClicked);
        }
    }

    private async void OnLoginClicked()
    {
        // Sua lógica de login aqui...
        
        // Após login bem-sucedido:
        SceneManager.LoadScene("HarmonyTown");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

public async Task SendPositionToServer(Vector2 position)
    {
        if (Socket == null || string.IsNullOrEmpty(_matchId)) // Corrigido para usar _matchId
        {
            Debug.LogWarning("Socket não está pronto ou matchId não definido");
            return;
        }

        try
        {
            await Socket.SendMatchStateAsync(
                _matchId, // Corrigido para usar _matchId
                1, // OpCode para atualizações de posição
                JsonUtility.ToJson(position)
            );
            Debug.Log($"Posição enviada: {position}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Erro ao enviar posição: {e.Message}");
        }
    }



}