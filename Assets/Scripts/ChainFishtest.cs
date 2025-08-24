using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ChainFishTEST : MonoBehaviour
{
    private enum EnemyVariants
    {
        EasyFish,
        NormalFish,
        MediumFish,
        HardSpicyPepper
    }

    [SerializeField] private float enemyMoveSpeed = 2f;
    [SerializeField] public float distanceFromPlayer;

    [SerializeField] public GameObject FISH1;
    [SerializeField] public GameObject FISH2;
    [SerializeField] public GameObject explosionGameObjectPrefab;
    [SerializeField] public Vector2 fish1positionX = new Vector2(0f, 0f);
    [SerializeField] public Vector2 fish2positionX = new Vector2(0f, 0f);
    [SerializeField] public LineController lineController;
    [SerializeField] public LineRenderer lineRenderer;

    [SerializeField] public GameObject lineRendererObject;

    [SerializeField] private float retractSpeed = 2f;

    [SerializeField] public bool alive = true;
    [SerializeField] public bool Uninstall = false;

    private bool explosionSpawned = false;

    [SerializeField] GameObject[] fishVarients;
    [SerializeField] private SpriteRenderer originalFishRenderer;

    GameObject Fish1;
    GameObject Fish2;
    private GameObject fish1Target;
    private GameObject fish2Target;

    private bool fish1Retracting = false;
    private bool fish2Retracting = false;

    private LineRenderer lineToFish1;
    private LineRenderer lineToFish2;

    Vector3 AttachPoint1;
    Vector3 AttachPoint2;

    private float enemyMinSpeed = 2.5f;
    private float enemyMaxSpeed = 5f;
    private float enemyStartingPosition = 12f;

    float[] specificYaxisSpawningRange = { 3.32f, 0.21f, -2.94f };

    GameEnums.GameDifficulty gameDifficulty;

    private void Awake()
    {
        if (fishVarients == null || fishVarients.Length < 4)
        {
            Debug.LogError("Fish variants not assigned in Inspector!");
            return;
        }

        foreach (var fish in fishVarients)
        {
            if (fish != null)
                fish.SetActive(false);
        }
    }

    private void EnableDifficultyFish(GameEnums.GameDifficulty difficulty)
    {
        foreach (var fish in fishVarients)
            fish.SetActive(false);

        int index = 0;
        switch (difficulty)
        {
            case GameEnums.GameDifficulty.Easy:
                enemyMinSpeed = 1.2f;
                enemyMaxSpeed = 2.9f;
                index = (int)EnemyVariants.EasyFish;
                break;
            case GameEnums.GameDifficulty.Normal:
                enemyMinSpeed = 1.3f;
                enemyMaxSpeed = 2.5f;
                index = (int)EnemyVariants.NormalFish;
                break;
            case GameEnums.GameDifficulty.Hard:
                enemyMinSpeed = 3.3f;
                enemyMaxSpeed = 5f;
                index = (int)EnemyVariants.MediumFish;
                break;
            case GameEnums.GameDifficulty.Expert:
                enemyMinSpeed = 6.5f;
                enemyMaxSpeed = 10f;
                index = (int)EnemyVariants.HardSpicyPepper;
                break;
        }

        if (fishVarients[index] != null)
            fishVarients[index].SetActive(true);
    }

    void Start()
    {
        AttachPoint1 = transform.position;
        AttachPoint2 = transform.position;

        lineToFish1 = CreateLineRenderer("LineToFish1");
        lineToFish2 = CreateLineRenderer("LineToFish2");

        gameDifficulty = GameEnums.SelectedDifficulty;
        EnableDifficultyFish(gameDifficulty);

        SetEnemyPositionAndSpeed();

        enemyMoveSpeed = Random.Range(enemyMinSpeed, enemyMaxSpeed);
        float enemyY = specificYaxisSpawningRange[Random.Range(0, specificYaxisSpawningRange.Length)];
        Vector3 fish1pos = new Vector3(enemyStartingPosition, enemyY, 0);

        enemyMoveSpeed = Random.Range(enemyMinSpeed, enemyMaxSpeed);
        float enemyY2 = specificYaxisSpawningRange[Random.Range(0, specificYaxisSpawningRange.Length)];
        Vector3 fish2pos = new Vector3(enemyStartingPosition, enemyY2, 0);

        Fish1 = Instantiate(FISH1, fish1pos, Quaternion.identity);
        Fish2 = Instantiate(FISH2, fish2pos, Quaternion.identity);

        GameManager gameManager = FindFirstObjectByType<GameManager>();
        gameManager.activeEnemyList.Add(Fish1);
        gameManager.activeEnemyList.Add(Fish2);
    }

    private void Update()
    {
        bool controllerAlive = alive;
        bool fish1Alive = Fish1 != null && Fish1.GetComponent<Enemy>().alive;
        bool fish2Alive = Fish2 != null && Fish2.GetComponent<Enemy>().alive;

        if (Uninstall)
        {
            Destroy(gameObject);
            return;
        }

        if (!alive && !explosionSpawned)
        {
            SpawnExplosion(gameObject);

            if (fishVarients != null)
            {
                foreach (var fish in fishVarients)
                    if (fish != null) fish.SetActive(false);
            }

            controllerAlive = false;
            explosionSpawned = true;
        }
        else if (alive)
        {
            EnemyMovement();
        }

        bool controllerJustDied = !controllerAlive && !fish1Retracting && !fish2Retracting;

        // --- Fish1 Line ---
        if (fish1Alive && controllerAlive)
        {
            AttachPoint1 = Fish1.transform.position;
            lineToFish1.enabled = true;
            lineToFish1.SetPosition(0, transform.position);
            lineToFish1.SetPosition(1, Fish1.transform.position);
        }
        else if (!fish1Alive && !fish1Retracting)
        {
            fish1Retracting = true;
            fish1Target = controllerAlive ? gameObject : (fish2Alive ? Fish2 : null);
        }
        else if (controllerJustDied && fish1Alive)
        {
            fish1Retracting = true;
            fish1Target = fish2Alive ? Fish2 : null;
            AttachPoint1 = transform.position;
        }

        if (fish1Retracting && fish1Target != null)
        {
            AttachPoint1 = Vector3.MoveTowards(AttachPoint1, fish1Target.transform.position, retractSpeed * Time.deltaTime);
            lineToFish1.enabled = true;
            lineToFish1.SetPosition(0, fish1Target.transform.position);
            lineToFish1.SetPosition(1, AttachPoint1);

            if (Vector3.Distance(AttachPoint1, fish1Target.transform.position) < 0.01f)
            {
                if (fish1Target == gameObject)
                {
                    alive = false;
                    SpawnExplosion(gameObject);
                }
                else
                {
                    fish1Target.GetComponent<Enemy>().alive = false;
                    SpawnExplosion(fish1Target);
                }

                lineToFish1.enabled = false;
                fish1Retracting = false;
            }
        }
        else if (!fish1Alive || (!controllerAlive && !fish1Retracting))
        {
            lineToFish1.enabled = false;
        }

        // --- Fish2 Line ---
        if (fish2Alive && controllerAlive)
        {
            AttachPoint2 = Fish2.transform.position;
            lineToFish2.enabled = true;
            lineToFish2.SetPosition(0, transform.position);
            lineToFish2.SetPosition(1, Fish2.transform.position);
        }
        else if (!fish2Alive && !fish2Retracting)
        {
            fish2Retracting = true;
            fish2Target = controllerAlive ? gameObject : (fish1Alive ? Fish1 : null);
        }
        else if (controllerJustDied && fish2Alive)
        {
            fish2Retracting = true;
            fish2Target = fish1Alive ? Fish1 : null;
            AttachPoint2 = transform.position;
        }

        if (fish2Retracting && fish2Target != null)
        {
            AttachPoint2 = Vector3.MoveTowards(AttachPoint2, fish2Target.transform.position, retractSpeed * Time.deltaTime);
            lineToFish2.enabled = true;
            lineToFish2.SetPosition(0, fish2Target.transform.position);
            lineToFish2.SetPosition(1, AttachPoint2);

            if (Vector3.Distance(AttachPoint2, fish2Target.transform.position) < 0.01f)
            {
                if (fish2Target == gameObject)
                {
                    alive = false;
                    SpawnExplosion(gameObject);
                }
                else
                {
                    fish2Target.GetComponent<Enemy>().alive = false;
                    SpawnExplosion(fish2Target);
                }

                lineToFish2.enabled = false;
                fish2Retracting = false;
                fish2Target = null;
            }
        }
        else if (!fish2Alive || (!controllerAlive && !fish2Retracting))
        {
            lineToFish2.enabled = false;
        }

        bool fish1Dead = Fish1 == null || !Fish1.GetComponent<Enemy>().alive;
        bool fish2Dead = Fish2 == null || !Fish2.GetComponent<Enemy>().alive;

        if (fish1Dead && fish2Dead)
        {
            Uninstall = true;
        }
    }

    private void EnemyMovement()
    {
        transform.position += Vector3.left * enemyMoveSpeed * Time.deltaTime;
    }

    private void SetEnemyPositionAndSpeed()
    {
        enemyMoveSpeed = Random.Range(enemyMinSpeed, enemyMaxSpeed);
        float enemyY = specificYaxisSpawningRange[Random.Range(0, specificYaxisSpawningRange.Length)];
        transform.position = new Vector3(enemyStartingPosition, enemyY, 0);
    }

    private LineRenderer CreateLineRenderer(string name)
    {
        GameObject lineObj = new GameObject(name);
        lineObj.transform.parent = transform;
        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.widthMultiplier = 0.05f;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = Color.white;
        lr.endColor = Color.white;
        return lr;
    }

    private void SpawnExplosion(GameObject target)
    {
        if (target == null || explosionGameObjectPrefab == null) return;

        Instantiate(explosionGameObjectPrefab, target.transform.position, Quaternion.identity);

        var renderer = target.GetComponentInChildren<SpriteRenderer>();
        if (renderer != null) renderer.enabled = false;
    }
}


    //// Handle Fish2 retraction
    //if (fish2Retracting && fish2Target != null)
    //{
    //    AttachPoint2 = Vector3.MoveTowards(AttachPoint2, fish2Target.transform.position, retractSpeed * Time.deltaTime);
    //    lineToFish2.enabled = true;
    //    lineToFish2.SetPosition(0, fish2Target.transform.position);
    //    lineToFish2.SetPosition(1, AttachPoint2);

    //    if (Vector3.Distance(AttachPoint2, fish2Target.transform.position) < 0.01f)
    //    {
    //        // Line reached target - kill it
    //        if (fish2Target == gameObject)
    //            alive = false;
    //        else if (fish2Target != null)
    //            fish2Target.GetComponent<Enemy>().alive = false;

    //        lineToFish2.enabled = false;
    //        fish2Retracting = false;
    //    }
    //}
    //else if (!fish2Alive || (!controllerAlive && !fish2Retracting))
    //{
    //    // Hide Fish2's line when it should not be visible
    //    lineToFish2.enabled = false;
    //}

    //// Clean up when all fish are dead
    //bool fish1Dead = Fish1 == null || !Fish1.GetComponent<Enemy>().alive;
    //bool fish2Dead = Fish2 == null || !Fish2.GetComponent<Enemy>().alive;

    //if (fish1Dead && fish2Dead)
    //{
    //    Uninstall = true;
    //}

    //lineRenderer = FindAnyObjectByType<LineRenderer>();
    //lineRenderer = GetComponent<LineRenderer>();
    //lineController.points = new Transform[] { FISH1.transform, FISH2.transform };
    //lineController.SetUpLine(new Transform[] { this.transform, Fish1.transform });
    //lineController.SetUpLine(new Transform[] { this.transform, Fish2.transform });
    // Update line positions
    //lineRenderer.SetPosition(0, Fish1.transform.position);
    //lineRenderer.SetPosition(1, Fish2.transform.position);

    //lineRenderer.SetPosition(0, Fish2.transform.position);
    //lineRenderer.SetPosition(1, this.transform.position);
    //lineRenderer.SetPosition(2, Fish1.transform.position);


    //lineToFish1.SetPosition(0, transform.position);
    //lineToFish1.SetPosition(1, Fish1.transform.position);

    //lineToFish2.SetPosition(0, transform.position);
    //lineToFish2.SetPosition(1, Fish2.transform.position);



    //lineRenderer.SetPosition(0, Fish2.transform.position);
    //lineRenderer.SetPosition(1, Fish1.transform.position);

    //lineToFish1 = Fish1.AddComponent<LineRenderer>();
    //// Create line to Fish2
    //lineToFish2 = Fish2.AddComponent<LineRenderer>();





    //private void Update()
    //{
    //    //lineRendererObject = FindAnyObjectByType<LineRenderer>().gameObject;
    //    //lineController = lineRendererObject.GetComponent<LineController>();
    //    //lineController.SetUpLine(new UnityEngine.Transform[] { Fish1.transform, Fish2.transform });

    //    if (Uninstall)
    //    {
    //        Destroy(gameObject);
    //        return; // uninstalling life.exe
    //    }


    //    if (!alive)
    //    {
    //        var renderer = GetComponentInChildren<SpriteRenderer>();
    //        if (renderer != null)
    //            renderer.enabled = false;
    //    }
    //    else
    //        EnemyMovement();



    //    //fish1positionX = Fish1.transform.position;
    //    //fish2positionX = Fish2.transform.position;

    //    //lineRenderer.SetPosition(0, Fish2.transform.position);
    //    //lineRenderer.SetPosition(1, this.transform.position);
    //    //lineRenderer.SetPosition(2, Fish1.transform.position);

    //    //gamer
    //    //lineToFish1.SetPosition(0, transform.position);
    //    //if (Fish1 != null)
    //    //    lineToFish1.SetPosition(1, Fish1.transform.position);



    //    //lineToFish1.SetPosition(1, Fish1.transform.position);

    //    //lineToFish1.SetPosition(0, Fish2.transform.position);
    //    //lineToFish1.SetPosition(1, this.transform.position);
    //    //lineToFish1.SetPosition(2, Fish1.transform.position);

    //    //gamer
    //    //lineToFish2.SetPosition(0, transform.position);
    //    //if (Fish1 != null)
    //    //    lineToFish2.SetPosition(1, Fish2.transform.position);

    //    //lineToFish2.SetPosition(0, Fish2.transform.position);
    //    //lineToFish2.SetPosition(1, this.transform.position);
    //    //lineToFish2.SetPosition(2, Fish1.transform.position);


    //    //lineRenderer.SetPosition(0, Fish2.transform.position);
    //    //lineRenderer.SetPosition(1, Fish1.transform.position);

    //    //lineController.points = new Transform[] { Fish1.transform, Fish2.transform };

    //    //for (int i = 0; i < lineController.points.Length; i++)
    //    //{
    //    //    lineRenderer.SetPosition(i, lineController.points[i].position);
    //    //}


    //    // Get alive states
    //    bool controllerAlive = alive;
    //    bool fish1Alive = Fish1 != null && Fish1.GetComponent<Enemy>().alive;
    //    bool fish2Alive = Fish2 != null && Fish2.GetComponent<Enemy>().alive;

    //    //// --- Fish 1 line ---
    //    //if (fish1Alive && controllerAlive)
    //    //{
    //    //    AttachPoint1 = transform.position;
    //    //    lineToFish1.enabled = true;
    //    //    lineToFish1.SetPosition(0, transform.position);
    //    //    lineToFish1.SetPosition(1, Fish1.transform.position);
    //    //}
    //    //else if (!fish1Alive)
    //    //{
    //    //    // Determine retraction target: the closest alive fish
    //    //    Vector3 target = controllerAlive ? transform.position :
    //    //                    (fish2Alive ? Fish2.transform.position : AttachPoint1);

    //    //    AttachPoint1 = Vector3.MoveTowards(AttachPoint1, target, retractSpeed * Time.deltaTime);
    //    //    lineToFish1.SetPosition(0, target);
    //    //    lineToFish1.SetPosition(1, AttachPoint1);

    //    //    //    if (Vector3.Distance(AttachPoint1, target) < 0.01f)
    //    //    //        lineToFish1.enabled = false;
    //    //    //        Fish1.GetComponent<Enemy>().Uninstall = true; // this is a workaround to not destroy the enemy, but to stop it from moving and rendering.
    //    //    //        this.alive= false; // this is a workaround to not destroy the enemy, but to stop it from moving and rendering.
    //    //    //        Fish2.GetComponent<Enemy>().alive = false; // this is a workaround to not destroy the enemy, but to stop it from moving and rendering.

    //    //    if (Vector3.Distance(AttachPoint1, target) < 0.01f)
    //    //    {
    //    //        lineToFish1.enabled = false;
    //    //        if (Fish1 != null) Fish1.GetComponent<Enemy>().Uninstall = true;
    //    //        this.alive = false;
    //    //        if (Fish2 != null) Fish2.GetComponent<Enemy>().alive = false;
    //    //    }

    //    //}   


    //    //// --- Fish 2 line ---
    //    //if (fish2Alive && controllerAlive)
    //    //{
    //    //    AttachPoint2 = transform.position;
    //    //    lineToFish2.enabled = true;
    //    //    lineToFish2.SetPosition(0, transform.position);
    //    //    lineToFish2.SetPosition(1, Fish2.transform.position);
    //    //}
    //    //else if (!fish2Alive)
    //    //{
    //    //    Vector3 target = controllerAlive ? transform.position :
    //    //                    (fish1Alive ? Fish1.transform.position : AttachPoint2);

    //    //    AttachPoint2 = Vector3.MoveTowards(AttachPoint2, target, retractSpeed * Time.deltaTime);
    //    //    lineToFish2.SetPosition(0, target);
    //    //    lineToFish2.SetPosition(1, AttachPoint2);

    //    //    //if (Vector3.Distance(AttachPoint2, target) < 0.01f)
    //    //    //    lineToFish2.enabled = false;
    //    //    //    Fish2.GetComponent<Enemy>().Uninstall = true; // this is a workaround to not destroy the enemy, but to stop it from moving and rendering.
    //    //    //    this.alive = false; // this is a workaround to not destroy the enemy, but to stop it from moving and rendering.
    //    //    //    Fish2.GetComponent<Enemy>().alive = false; // this is a workaround to not destroy the enemy, but to stop it from moving and rendering.

    //    //    if (Vector3.Distance(AttachPoint2, target) < 0.01f)
    //    //    {
    //    //        lineToFish2.enabled = false;
    //    //        if (Fish2 != null) Fish2.GetComponent<Enemy>().Uninstall = true;
    //    //        this.alive = false;
    //    //        if (Fish1 != null) Fish1.GetComponent<Enemy>().alive = false;
    //    //    }


    //    //}

    //    //if (Fish1 != null && Fish2 != null)
    //    //{
    //    //    if (!this.alive)
    //    //    {
    //    //        this.Uninstall = true; // this is a workaround to not destroy the enemy, but to stop it from moving and rendering.
    //    //    }
    //    //}


    //    //// --- Handle Fish 1 Fuse ---
    //    //if (!fish1Alive && !fish1Retracting)
    //    //{
    //    //    fish1Retracting = true;
    //    //    fish1Target = controllerAlive ? gameObject : (fish2Alive ? Fish2 : null);
    //    //}

    //    //if (fish1Retracting && fish1Target != null)
    //    //{
    //    //    AttachPoint1 = Vector3.MoveTowards(AttachPoint1, fish1Target.transform.position, retractSpeed * Time.deltaTime);
    //    //    lineToFish1.SetPosition(0, fish1Target.transform.position);
    //    //    lineToFish1.SetPosition(1, AttachPoint1);

    //    //    if (Vector3.Distance(AttachPoint1, fish1Target.transform.position) < 0.01f)
    //    //    {
    //    //        // Fuse burned to target — kill target
    //    //        if (fish1Target == gameObject) alive = false;
    //    //        else if (fish1Target != null) fish1Target.GetComponent<Enemy>().alive = false;

    //    //        lineToFish1.enabled = false;
    //    //        fish1Retracting = false;
    //    //    }
    //    //}
    //    //else if (fish1Alive && controllerAlive)
    //    //{
    //    //    AttachPoint1 = Fish1.transform.position;
    //    //    lineToFish1.SetPosition(0, transform.position);
    //    //    lineToFish1.SetPosition(1, Fish1.transform.position);
    //    //}

    //    //// --- Handle Fish 2 Fuse ---
    //    //if (!fish2Alive && !fish2Retracting)
    //    //{
    //    //    fish2Retracting = true;
    //    //    fish2Target = controllerAlive ? gameObject : (fish1Alive ? Fish1 : null);
    //    //}

    //    //if (fish2Retracting && fish2Target != null)
    //    //{
    //    //    AttachPoint2 = Vector3.MoveTowards(AttachPoint2, fish2Target.transform.position, retractSpeed * Time.deltaTime);
    //    //    lineToFish2.SetPosition(0, fish2Target.transform.position);
    //    //    lineToFish2.SetPosition(1, AttachPoint2);

    //    //    if (Vector3.Distance(AttachPoint2, fish2Target.transform.position) < 0.01f)
    //    //    {
    //    //        if (fish2Target == gameObject) alive = false;
    //    //        else if (fish2Target != null) fish2Target.GetComponent<Enemy>().alive = false;

    //    //        lineToFish2.enabled = false;
    //    //        fish2Retracting = false;
    //    //    }
    //    //}
    //    //else if (fish2Alive && controllerAlive)
    //    //{
    //    //    AttachPoint2 = Fish2.transform.position;
    //    //    lineToFish2.SetPosition(0, transform.position);
    //    //    lineToFish2.SetPosition(1, Fish2.transform.position);
    //    //}





    //    // FISH





    //    //
    //    //// --- Handle Fish 1 Fuse ---
    //    //if (!fish1Alive && !fish1Retracting)
    //    //{
    //    //    fish1Retracting = true;
    //    //    // if controller dead, target the surviving fish
    //    //    fish1Target = alive ? gameObject : (fish2Alive ? Fish2 : null);
    //    //}

    //    //if (fish1Retracting && fish1Target != null)
    //    //{
    //    //    AttachPoint1 = Vector3.MoveTowards(AttachPoint1, fish1Target.transform.position, retractSpeed * Time.deltaTime);
    //    //    lineToFish1.SetPosition(0, fish1Target.transform.position);
    //    //    lineToFish1.SetPosition(1, AttachPoint1);

    //    //    if (Vector3.Distance(AttachPoint1, fish1Target.transform.position) < 0.01f)
    //    //    {
    //    //        if (fish1Target == gameObject) alive = false;
    //    //        else if (fish1Target != null) fish1Target.GetComponent<Enemy>().alive = false;

    //    //        lineToFish1.enabled = false;
    //    //        fish1Retracting = false;
    //    //    }
    //    //}
    //    //else if (fish1Alive && alive)
    //    //{
    //    //    AttachPoint1 = Fish1.transform.position;
    //    //    lineToFish1.SetPosition(0, transform.position);
    //    //    lineToFish1.SetPosition(1, Fish1.transform.position);
    //    //}

    //    ////// --- Handle Fish 2 Fuse ---
    //    ////if (!fish2Alive && !fish2Retracting)
    //    ////{
    //    ////    fish2Retracting = true;
    //    ////    fish2Target = alive ? gameObject : (fish1Alive ? Fish1 : null);
    //    ////}

    //    ////if (fish2Retracting && fish2Target != null)
    //    ////{
    //    ////    AttachPoint2 = Vector3.MoveTowards(AttachPoint2, fish2Target.transform.position, retractSpeed * Time.deltaTime);
    //    ////    lineToFish2.SetPosition(0, fish2Target.transform.position);
    //    ////    lineToFish2.SetPosition(1, AttachPoint2);

    //    ////    if (Vector3.Distance(AttachPoint2, fish2Target.transform.position) < 0.01f)
    //    ////    {
    //    ////        if (fish2Target == gameObject) alive = false;
    //    ////        else if (fish2Target != null) fish2Target.GetComponent<Enemy>().alive = false;

    //    ////        lineToFish2.enabled = false;
    //    ////        fish2Retracting = false;
    //    ////    }
    //    ////}
    //    ////else if (fish2Alive && alive)
    //    ////{
    //    ////    AttachPoint2 = Fish2.transform.position;
    //    ////    lineToFish2.SetPosition(0, transform.position);
    //    ////    lineToFish2.SetPosition(1, Fish2.transform.position);
    //    ////}


    //    //// --- Handle Fish 2 Fuse ---
    //    //if (!fish2Alive && !fish2Retracting)
    //    //{
    //    //    fish2Retracting = true;
    //    //    // If controller alive, line retracts to controller
    //    //    // If controller dead, line retracts to surviving fish1
    //    //    fish2Target = alive ? gameObject : (fish1Alive ? Fish1 : null);
    //    //}

    //    //if (fish2Retracting && fish2Target != null)
    //    //{
    //    //    AttachPoint2 = Vector3.MoveTowards(AttachPoint2, fish2Target.transform.position, retractSpeed * Time.deltaTime);
    //    //    lineToFish2.SetPosition(0, fish2Target.transform.position);
    //    //    lineToFish2.SetPosition(1, AttachPoint2);

    //    //    if (Vector3.Distance(AttachPoint2, fish2Target.transform.position) < 0.01f)
    //    //    {
    //    //        // Kill target only if it exists
    //    //        if (fish2Target == gameObject) alive = false;
    //    //        else if (fish2Target != null) fish2Target.GetComponent<Enemy>().alive = false;

    //    //        lineToFish2.enabled = false;
    //    //        fish2Retracting = false;
    //    //    }
    //    //}






    //    //// --- Handle controller death ---
    //    //if (!alive)
    //    //{
    //    //    // start retracting any surviving fish
    //    //    if (fish1Alive && !fish1Retracting)
    //    //    {
    //    //        fish1Retracting = true;
    //    //        fish1Target = Fish2 != null && Fish2.GetComponent<Enemy>().alive ? Fish2 : null;
    //    //    }
    //    //    if (fish2Alive && !fish2Retracting)
    //    //    {
    //    //        fish2Retracting = true;
    //    //        fish2Target = Fish1 != null && Fish1.GetComponent<Enemy>().alive ? Fish1 : null;
    //    //    }
    //    //}


    //    ////HandleLine(ref lineToFish1, ref AttachPoint1, gameObject, Fish1, ref fish1Retracting, ref fish1Target);
    //    ////HandleLine(ref lineToFish2, ref AttachPoint2, gameObject, Fish2, ref fish2Retracting, ref fish2Target);

    //    //// If both fish are null or dead, uninstall the controller
    //    //bool fish1Dead = Fish1 == null || !Fish1.GetComponent<Enemy>().alive;
    //    //bool fish2Dead = Fish2 == null || !Fish2.GetComponent<Enemy>().alive;

    //    //if (fish1Dead && fish2Dead)
    //    //{
    //    //    Uninstall = true;
    //    //}


    //    // --- Handle Fish 1 Fuse ---
    //    if (!fish1Alive && !fish1Retracting)
    //    {
    //        fish1Retracting = true;
    //        // If controller alive, line retracts to controller
    //        // If controller dead, line retracts to surviving Fish2
    //        if (alive)
    //            fish1Target = gameObject;
    //        else if (fish2Alive)
    //            fish1Target = Fish2;
    //        else
    //            fish1Target = null;
    //    }

    //    if (fish1Retracting && fish1Target != null)
    //    {
    //        AttachPoint1 = Vector3.MoveTowards(AttachPoint1, fish1Target.transform.position, retractSpeed * Time.deltaTime);
    //        lineToFish1.SetPosition(0, fish1Target.transform.position);
    //        lineToFish1.SetPosition(1, AttachPoint1);

    //        if (Vector3.Distance(AttachPoint1, fish1Target.transform.position) < 0.01f)
    //        {
    //            if (fish1Target == gameObject) alive = false;
    //            else if (fish1Target != null) fish1Target.GetComponent<Enemy>().alive = false;

    //            lineToFish1.enabled = false;
    //            fish1Retracting = false;
    //        }
    //    }
    //    else if (fish1Alive && alive)
    //    {
    //        AttachPoint1 = Fish1.transform.position;
    //        lineToFish1.SetPosition(0, transform.position);
    //        lineToFish1.SetPosition(1, Fish1.transform.position);
    //    }

    //    // --- Handle Fish 2 Fuse ---
    //    if (!fish2Alive && !fish2Retracting)
    //    {
    //        fish2Retracting = true;
    //        // If controller alive, line retracts to controller
    //        // If controller dead, line retracts to surviving Fish1
    //        if (alive)
    //            fish2Target = gameObject;
    //        else if (fish1Alive)
    //            fish2Target = Fish1;
    //        else
    //            fish2Target = null;
    //    }

    //    if (fish2Retracting && fish2Target != null)
    //    {
    //        AttachPoint2 = Vector3.MoveTowards(AttachPoint2, fish2Target.transform.position, retractSpeed * Time.deltaTime);
    //        lineToFish2.SetPosition(0, fish2Target.transform.position);
    //        lineToFish2.SetPosition(1, AttachPoint2);

    //        if (Vector3.Distance(AttachPoint2, fish2Target.transform.position) < 0.01f)
    //        {
    //            if (fish2Target == gameObject) alive = false;
    //            else if (fish2Target != null) fish2Target.GetComponent<Enemy>().alive = false;

    //            lineToFish2.enabled = false;
    //            fish2Retracting = false;
    //        }
    //    }
    //    else if (fish2Alive && alive)
    //    {
    //        AttachPoint2 = Fish2.transform.position;
    //        lineToFish2.SetPosition(0, transform.position);
    //        lineToFish2.SetPosition(1, Fish2.transform.position);
    //    }

    //    // --- Handle controller death ---
    //    if (!alive)
    //    {
    //        // start retracting any surviving fish
    //        if (fish1Alive && !fish1Retracting)
    //        {
    //            fish1Retracting = true;
    //            fish1Target = Fish2 != null && Fish2.GetComponent<Enemy>().alive ? Fish2 : null;
    //        }
    //        if (fish2Alive && !fish2Retracting)
    //        {
    //            fish2Retracting = true;
    //            fish2Target = Fish1 != null && Fish1.GetComponent<Enemy>().alive ? Fish1 : null;
    //        }
    //    }

    //    // If both fish are dead or null, uninstall the controller
    //    bool fish1Dead = Fish1 == null || !Fish1.GetComponent<Enemy>().alive;
    //    bool fish2Dead = Fish2 == null || !Fish2.GetComponent<Enemy>().alive;

    //    if (fish1Dead && fish2Dead)
    //    {
    //        Uninstall = true;
    //    }





    //}

