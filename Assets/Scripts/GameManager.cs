using Nakama;
using Nakama.TinyJson;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public NakamaConnection NakamaConnection;
    public GameObject NetworkLocalPlayerPrefab;
    public GameObject NetworkRemotePlayerPrefab;
    public GameObject MainMenu;
    public GameObject InGameMenu;
    public Text WinningPlayerText;
    public GameObject SpawnPoints;

    private IDictionary<string, GameObject> players;
    private IUserPresence localUser;
    private GameObject localPlayer;
    private IMatch currentMatch;

    private Transform[] spawnPoints;

    private string localDisplayName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
