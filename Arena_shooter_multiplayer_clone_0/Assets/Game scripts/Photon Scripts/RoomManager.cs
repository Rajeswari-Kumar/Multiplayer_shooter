using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;
using Photon.Realtime;
public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    private void Awake()
    {
        if(Instance)//If there is already an instance of room manager in scene - it destroys it self
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject); // if this is the only room manager
        Instance = this;
    }
    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded( Scene scene, LoadSceneMode loadSceneMode)
    {
        //for instantiation player prefab for every player in connection - by using name of prefab
        //**every object that needs to be initiated in player side must be put in resources folder rather than in editor
        //that way it is initated in every player even it is not present in the scene editor**

        //Whenever the scene is switched - this is called
        if (scene.buildIndex == 1)
        {
            PhotonNetwork.Instantiate(Path.Combine("Photon Prefabs","PlayerManager"), Vector3.zero,Quaternion.identity);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
   
}
