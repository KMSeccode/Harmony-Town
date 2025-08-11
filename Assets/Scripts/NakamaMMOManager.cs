using Nakama;
using UnityEngine;
using System;

public class NakamaBasicConnector : MonoBehaviour
{
    [Header("Server Settings")]
    public string scheme = "http"; // ou "https"
    public string host = "localhost"; // ou seu IP/DNS
    public int port = 7350;
    public string serverKey = "defaultkey";

    private IClient client;
    private ISession session;

    async void Start()
    {
        try
        {
            // 1. Criação do cliente (formato atualizado)
            client = new Client(scheme, host, port, serverKey, UnityWebRequestAdapter.Instance);

            // 2. Autenticação básica
            session = await client.AuthenticateDeviceAsync(
                SystemInfo.deviceUniqueIdentifier,
                username: "Player_" + UnityEngine.Random.Range(1000, 9999));

            Debug.Log($"Conectado como: {session.Username} (ID: {session.UserId})");
        }
        catch (Exception e)
        {
            Debug.LogError($"Falha na conexão: {e.Message}");
        }
    }
}