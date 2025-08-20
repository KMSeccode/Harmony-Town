using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nakama;

public class NakamaConnection : MonoBehaviour
{
    private string scheme = "http";
    private string host = "localhost";
    private int port = 7350;
    private string serverkey = "defaultkey";



    private IClient client;
    private ISession session;
    private ISocket socket;
    private string ticket;

    public IClient GetClient() => client;
    public ISession GetSession() => session;
    public ISocket GetSocket() => socket;


    async void Start()
    {
        client = new Client(scheme, host, port, serverkey, UnityWebRequestAdapter.Instance);
        session = await client.AuthenticateDeviceAsync(SystemInfo.deviceUniqueIdentifier);
        socket = client.NewSocket();
        await socket.ConnectAsync(session, true);

        socket.ReceivedMatchmakerMatched += OnReceivedMatchmakerMatched;

        Debug.Log(session);
        Debug.Log(socket);
    }

    public async void FindMatch()
    {
        Debug.Log("Finding match");

        var matchmakingTicket = await socket.AddMatchmakerAsync("*", 2, 2);
        ticket = matchmakingTicket.Ticket;
    }

    private async void OnReceivedMatchmakerMatched(IMatchmakerMatched matchmakerMatched)
    {
        var match = await socket.JoinMatchAsync(matchmakerMatched);

        Debug.Log("Our Session Id: " + match.Self.SessionId);

        foreach(var user in match.Presences)
        {
            Debug.Log("Connect User Session Id: " + user.SessionId);
        }
    }
}
