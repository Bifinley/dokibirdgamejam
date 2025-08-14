using UnityEngine;

public class CastleData : MonoBehaviour
{
    public static CastleData Instance;

    private enum CastleState
    {
        None,
        Healthy,
        Damaged,
        Destroyed
    }
    private enum FloorState
    {
        None,
        Normal,
        Lots,
        Extreme
    }

    [Header("Castle Info")]
    public int castleHealthAmount = 100;

    [SerializeField] private GameObject[] castleObjects;

    /*public bool isLevel1Completed = false;
    public bool isLevel2Completed = false;
    public bool isLevel3Completed = false;*/

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (castleObjects != null && castleObjects.Length > 0)
        {
            CastleHealthGameObjectAndUIUpdater();
            CastleFloorGameObjectAndUIUpdater();
        }
    }

    private void CastleHealthGameObjectAndUIUpdater()
    {
        foreach (GameObject obj in castleObjects)
        {
            if (obj != null) obj.SetActive(false);
        }

        if (castleHealthAmount <= 20)
            SetCastleState(CastleState.Destroyed);
        else if (castleHealthAmount <= 50)
            SetCastleState(CastleState.Damaged);
        else if (castleHealthAmount <= 70)
            SetCastleState(CastleState.Healthy);
        else
            SetCastleState(CastleState.None);
    }

    private void CastleFloorGameObjectAndUIUpdater()
    {

    }

    private void SetCastleState(CastleState state)
    {
        int index = (int)state;
        if (index >= 0 && index < castleObjects.Length && castleObjects[index] != null)
        {
            castleObjects[index].SetActive(true);
        }
    }
}
