using Nakama;
using UnityEngine;

public class NakamaManager : MonoBehaviour
{
    private IClient client;
    private ISession session;

    async void Start()
    {
        // Conecta ao servidor local (ou substitua pelo IP do seu servidor Nakama)
        client = new Client("http", "localhost", 7350, "defaultkey");
        
        // Autenticação (exemplo com dispositivo único)
        session = await client.AuthenticateDeviceAsync(SystemInfo.deviceUniqueIdentifier);
        Debug.Log("Conectado! ID do Jogador: " + session.UserId);
    }
}