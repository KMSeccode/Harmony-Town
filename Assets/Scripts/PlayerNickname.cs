using UnityEngine;
using UnityEngine.UI;

public class PlayerNickname : MonoBehaviour
{
    [SerializeField] private GameObject nicknameCanvasPrefab;
    [SerializeField] private Vector3 offset = new Vector3(0, 2f, 0); // Ajuste a altura
    
    private GameObject nicknameCanvas;
    private Text nicknameText;

    private void Start()
    {
        // Instancia o Canvas do nickname
        nicknameCanvas = Instantiate(nicknameCanvasPrefab, transform.position + offset, Quaternion.identity);
        nicknameCanvas.transform.SetParent(transform);
        
        // Obtém o componente Text
        nicknameText = nicknameCanvas.GetComponentInChildren<Text>();
        
        // Carrega o nickname salvo
        string nickname = PlayerPrefs.GetString("player_nickname", "Player");
        SetNickname(nickname);
    }

    public void SetNickname(string name)
    {
        if (nicknameText != null)
        {
            nicknameText.text = name;
        }
    }

    private void LateUpdate()
    {
        // Mantém o nickname sempre virado para a câmera
        if (nicknameCanvas != null)
        {
            nicknameCanvas.transform.LookAt(
                nicknameCanvas.transform.position + 
                Camera.main.transform.rotation * Vector3.forward,
                Camera.main.transform.rotation * Vector3.up
            );
        }
    }

    private void OnDestroy()
    {
        if (nicknameCanvas != null)
        {
            Destroy(nicknameCanvas);
        }
    }
}