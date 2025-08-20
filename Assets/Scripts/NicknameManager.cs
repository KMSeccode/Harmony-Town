using UnityEngine;
using UnityEngine.UI;
using Nakama;
using System.Threading.Tasks;

public class NicknameManager : MonoBehaviour
{
    [SerializeField] private InputField nicknameInput;
    [SerializeField] private Button playButton;
    [SerializeField] private Text errorText;
    
    private const string NicknameKey = "player_nickname";
    private NakamaConnection nakamaConnection; // Reference to existing connection
    
    private void Start()
    {
        // Configuração inicial
        playButton.onClick.AddListener(() => _ = HandleNicknameSubmission());
        errorText.gameObject.SetActive(false);
        
        // Get reference to existing NakamaConnection
        nakamaConnection = FindObjectOfType<NakamaConnection>();

        // Verifica se já tem nickname salvo
        if (PlayerPrefs.HasKey(NicknameKey))
        {
            nicknameInput.text = PlayerPrefs.GetString(NicknameKey);
        }
    }

    private async Task HandleNicknameSubmission()
    {
        string nickname = nicknameInput.text.Trim();
        
        if (string.IsNullOrEmpty(nickname))
        {
            ShowError("Nickname é obrigatório!");
            return;
        }
        
        if (nickname.Length < 3 || nickname.Length > 16)
        {
            ShowError("Nickname deve ter entre 3 e 16 caracteres!");
            return;
        }

        try
        {
        var storageObject = new WriteStorageObject
        {
            Collection = "users",
            Key = "profile",
            Value = $"{{\"nickname\":\"{nickname}\"}}"
        };

        await nakamaConnection.GetClient().WriteStorageObjectsAsync(
            nakamaConnection.GetSession(),
            new[] { storageObject }
        );
        
        PlayerPrefs.SetString(NicknameKey, nickname);
        UnityEngine.SceneManagement.SceneManager.LoadScene("HarmonyTown");
        }
        catch (System.Exception ex)
        {
            ShowError($"Erro ao conectar: {ex.Message}");
            Debug.LogError(ex);
        }
    }
        
    // Método para ser chamado após autenticação no Nakama
    private void ShowError(string message)
    {
        errorText.text = message;
        errorText.gameObject.SetActive(true);
    }
}