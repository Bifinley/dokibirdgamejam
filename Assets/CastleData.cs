using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class CastleData : MonoBehaviour
{
    public static CastleData Instance;

    private enum CastleState
    {
        Healthy,
        Damaged,
        Destroyed
    }
    private enum FloorState
    {
        Normal,
        Lots,
        Extreme
    }

    [Header("Castle Info")]
    public int castleHealthAmount = 100;

    [SerializeField] private GameObject[] castleObjects;

    public bool isLevel1Completed = false;
    public bool isLevel2Completed = false;
    public bool isLevel3Completed = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }

        isLevel1Completed = PlayerPrefs.GetInt("Level1Completed", 0) == 1;
        isLevel2Completed = PlayerPrefs.GetInt("Level2Completed", 0) == 1;
        isLevel3Completed = PlayerPrefs.GetInt("Level3Completed", 0) == 1;
    }

    private void Update()
    {
        CastleHealthGameObjectAndUIUpdater();

        CastleFloorGameObjectAndUIUpdater();
    }

    private void CastleHealthGameObjectAndUIUpdater()
    {
        if (castleHealthAmount <= 20)
        {
            castleObjects[(int)CastleState.Destroyed].SetActive(true);

            castleObjects[(int)CastleState.Damaged].SetActive(false);
            castleObjects[(int)CastleState.Healthy].SetActive(false);
        }
        else if (castleHealthAmount <= 50)
        {
            castleObjects[(int)CastleState.Damaged].SetActive(true);

            castleObjects[(int)CastleState.Destroyed].SetActive(false);
            castleObjects[(int)CastleState.Healthy].SetActive(false);
        }
        else if (castleHealthAmount <= 70)
        {
            castleObjects[(int)CastleState.Damaged].SetActive(true);

            castleObjects[(int)CastleState.Healthy].SetActive(false);
            castleObjects[(int)CastleState.Destroyed].SetActive(false);
        }
    }


    private void CastleFloorGameObjectAndUIUpdater()
    {
        
    }
}
