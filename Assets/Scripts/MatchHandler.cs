using UnityEngine;
using Nakama;
using System.Text;
using System.Collections.Generic;
 
public class MatchHandler : MonoBehaviour
{
    public GameObject playerPrefab;
    private Dictionary<string, GameObject> otherPlayers = new Dictionary<string, GameObject>();

    public void OnMatchState(IMatchState matchState)
    {
        if (matchState.OpCode == 1) // código da mensagem de movimento
        {
            var json = Encoding.UTF8.GetString(matchState.State);
            Vector2 pos = JsonUtility.FromJson<Vector2>(json);

            string userId = matchState.UserPresence.UserId;

            if (otherPlayers.ContainsKey(userId))
            {
                otherPlayers[userId].transform.position = pos;
            }
        }
    }
}
