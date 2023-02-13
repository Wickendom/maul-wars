using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDRoyaleSingleton : MonoBehaviour
{
    public static TDRoyaleSingleton Instance;

    public DataLists dataLists { get; private set; }
    public GameManager gameManager { get; private set; }
    public PathManager pathManager { get; private set; }
    public BuildingPlacer buildingPlacer { get; private set; }
    public TileMap tileMap { get; private set; }
    public MonsterSpawner monsterSpawner { get; private set; }
    public CurrencyManager currencyManager { get; private set; }
    public UIController uiController { get; private set; }
    public Network network { get; private set; }
    public CameraManager cameraManager { get; private set; }
    public LifeManager lifeManager { get; private set; }


    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);

        dataLists = GetComponentInChildren<DataLists>();
        gameManager = GetComponentInChildren<GameManager>();
        pathManager = GetComponentInChildren<PathManager>();
        buildingPlacer = GetComponentInChildren<BuildingPlacer>();
        tileMap = GetComponentInChildren<TileMap>();
        monsterSpawner = GetComponentInChildren<MonsterSpawner>();
        currencyManager = GetComponentInChildren<CurrencyManager>();
        uiController = GetComponentInChildren<UIController>();
        network = GetComponentInChildren<Network>();
        cameraManager = GetComponentInChildren<CameraManager>();
        lifeManager = GetComponentInChildren<LifeManager>();

        network.Initialise(); 
    }
}
