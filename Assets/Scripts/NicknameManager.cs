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
    private IClient nakamaClient;
    private ISession nakamaSession;
    
    private void Start()
    {
        // Configuração inicial
        playButton.onClick.AddListener(() => _ = HandleNicknameSubmission());
        errorText.gameObject.SetActive(false);
        
        // Inicializa cliente Nakama (ajuste conforme sua configuração)
        nakamaClient = new Client("http", "127.0.0.1", 7350, "defaultkey");

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
            // Autenticação atualizada para versão mais recente do Nakama
            nakamaSession = await nakamaClient.AuthenticateDeviceAsync(
                SystemInfo.deviceUniqueIdentifier, // deviceId (primeiro parâmetro)
                nickname,                          // username (segundo parâmetro)
                true                              // create (terceiro parâmetro)
                );

            // Salva localmente
            PlayerPrefs.SetString(NicknameKey, nickname);
            UnityEngine.SceneManagement.SceneManager.LoadScene("HarmonyTown");
        }
        catch (System.Exception ex)
        {
            ShowError($"Erro ao conectar: {ex.Message}");
            Debug.LogError(ex);
        }
    }
    
    private async Task UpdateNakamaNickname(string nickname)
    {
    var storageObject = new Nakama.WriteStorageObject {
        Collection = "account",
        Key = "display_name",
        Value = $"\"{nickname}\""
    };
    }
    
    // Método para ser chamado após autenticação no Nakama
    private void ShowError(string message)
    {
        errorText.text = message;
        errorText.gameObject.SetActive(true);
    }
}