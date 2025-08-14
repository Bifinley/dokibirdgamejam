using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ChainFishTEST : MonoBehaviour
{

    [SerializeField] private float enemyMoveSpeed = 2f;

    [SerializeField] public float distanceFromPlayer;

    [SerializeField] public GameObject FISH1;
    [SerializeField] public GameObject FISH2;
    [SerializeField] public Vector2 fish1positionX = new Vector2(0f, 0f);
    [SerializeField] public Vector2 fish2positionX = new Vector2(0f, 0f);
    [SerializeField] public LineController lineController;
    [SerializeField] public LineRenderer lineRenderer;

    [SerializeField] public GameObject lineRendererObject;

    [SerializeField] private float retractSpeed = 2f; // speed at which line end moves




    GameObject Fish1;
    GameObject Fish2;

    private LineRenderer lineToFish1;
    private LineRenderer lineToFish2;

    Vector3 AttachPoint1;
    Vector3 AttachPoint2;



    private float enemyMinSpeed = 2.5f;
    private float enemyMaxSpeed = 5f;
    private float enemyStartingPosition = 12f;

    float[] specificYaxisSpawningRange = { 3.32f, 0.21f, -2.94f }; // this is very specific so enemies stay on a very specific 3 layer path

    MainMenu.GameDifficulty gameDifficulty;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //lineRendererObject = FindAnyObjectByType<LineRenderer>().gameObject;
        //lineController = GetComponent<LineController>();

        //lineRenderer = GetComponent<LineRenderer>();
        ////lineController = GetComponent<LineController>();
        //// Create our own LineRenderer
        ////lineRenderer = gameObject.AddComponent<LineRenderer>();
        //lineRenderer.positionCount = 2;
        //lineRenderer.widthMultiplier = 0.05f; // thickness of line
        //lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        //lineRenderer.startColor = Color.white;
        //lineRenderer.endColor = Color.white;

        AttachPoint1 = transform.position;
        AttachPoint2 = transform.position;

        // Create line to Fish1
        lineToFish1 = CreateLineRenderer("LineToFish1");
        // Create line to Fish2
        lineToFish2 = CreateLineRenderer("LineToFish2");



        gameDifficulty = MainMenu.SelectedDifficulty;

        switch (gameDifficulty)
        {
            case MainMenu.GameDifficulty.Easy:
                enemyMinSpeed = 1.2f;
                enemyMaxSpeed = 3f;
                break;
            case MainMenu.GameDifficulty.Normal:
                enemyMinSpeed = 1.5f;
                enemyMaxSpeed = 3.5f;
                break;
            case MainMenu.GameDifficulty.Medium:
                enemyMinSpeed = 3.7f;
                enemyMaxSpeed = 6f;
                break;
            case MainMenu.GameDifficulty.Hard:
                enemyMinSpeed = 7.5f;
                enemyMaxSpeed = 14f;
                break;
        }
        SetEnemyPositionAndSpeed();


        // instantiate two fish enemies to be "connected" to. 
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


    }


    private void Update()
    {
        //lineRendererObject = FindAnyObjectByType<LineRenderer>().gameObject;
        //lineController = lineRendererObject.GetComponent<LineController>();
        //lineController.SetUpLine(new UnityEngine.Transform[] { Fish1.transform, Fish2.transform });

        


        EnemyMovement();


        //fish1positionX = Fish1.transform.position;
        //fish2positionX = Fish2.transform.position;

        //lineRenderer.SetPosition(0, Fish2.transform.position);
        //lineRenderer.SetPosition(1, this.transform.position);
        //lineRenderer.SetPosition(2, Fish1.transform.position);

        //gamer
        //lineToFish1.SetPosition(0, transform.position);
        //if (Fish1 != null)
        //    lineToFish1.SetPosition(1, Fish1.transform.position);



        //lineToFish1.SetPosition(1, Fish1.transform.position);

        //lineToFish1.SetPosition(0, Fish2.transform.position);
        //lineToFish1.SetPosition(1, this.transform.position);
        //lineToFish1.SetPosition(2, Fish1.transform.position);

        //gamer
        //lineToFish2.SetPosition(0, transform.position);
        //if (Fish1 != null)
        //    lineToFish2.SetPosition(1, Fish2.transform.position);

        //lineToFish2.SetPosition(0, Fish2.transform.position);
        //lineToFish2.SetPosition(1, this.transform.position);
        //lineToFish2.SetPosition(2, Fish1.transform.position);


        //lineRenderer.SetPosition(0, Fish2.transform.position);
        //lineRenderer.SetPosition(1, Fish1.transform.position);

        //lineController.points = new Transform[] { Fish1.transform, Fish2.transform };

        //for (int i = 0; i < lineController.points.Length; i++)
        //{
        //    lineRenderer.SetPosition(i, lineController.points[i].position);
        //}


        bool controllerAlive = this != null; // Or your own "is dead" flag
        bool fish1Alive = Fish1 != null;
        bool fish2Alive = Fish2 != null;

        // Handle Fish 1 line
        if (fish1Alive && controllerAlive)
        {
            AttachPoint1 = transform.position;
            lineToFish1.enabled = true;
            lineToFish1.SetPosition(0, transform.position);
            lineToFish1.SetPosition(1, Fish1.transform.position);
        }
        else if (!fish1Alive && fish2Alive) // retract toward surviving fish2
        {
            AttachPoint1 = Vector3.MoveTowards(AttachPoint1, Fish2.transform.position, retractSpeed * Time.deltaTime);
            lineToFish1.SetPosition(0, Fish2.transform.position);
            lineToFish1.SetPosition(1, AttachPoint1);

            if (Vector3.Distance(AttachPoint1, Fish2.transform.position) < 0.01f)
                lineToFish1.enabled = false;
        }
        else if (!fish1Alive && controllerAlive) // retract toward controller
        {
            AttachPoint1 = Vector3.MoveTowards(AttachPoint1, transform.position, retractSpeed * Time.deltaTime);
            lineToFish1.SetPosition(0, transform.position);
            lineToFish1.SetPosition(1, AttachPoint1);

            if (Vector3.Distance(AttachPoint1, transform.position) < 0.01f)
                lineToFish1.enabled = false;
        }

        // Handle Fish 2 line
        if (fish2Alive && controllerAlive)
        {
            AttachPoint2 = transform.position;
            lineToFish2.enabled = true;
            lineToFish2.SetPosition(0, transform.position);
            lineToFish2.SetPosition(1, Fish2.transform.position);
        }
        else if (!fish2Alive && fish1Alive) // retract toward surviving fish1
        {
            AttachPoint2 = Vector3.MoveTowards(AttachPoint2, Fish1.transform.position, retractSpeed * Time.deltaTime);
            lineToFish2.SetPosition(0, Fish1.transform.position);
            lineToFish2.SetPosition(1, AttachPoint2);

            if (Vector3.Distance(AttachPoint2, Fish1.transform.position) < 0.01f)
                lineToFish2.enabled = false;
        }
        else if (!fish2Alive && controllerAlive) // retract toward controller
        {
            AttachPoint2 = Vector3.MoveTowards(AttachPoint2, transform.position, retractSpeed * Time.deltaTime);
            lineToFish2.SetPosition(0, transform.position);
            lineToFish2.SetPosition(1, AttachPoint2);

            if (Vector3.Distance(AttachPoint2, transform.position) < 0.01f)
                lineToFish2.enabled = false;
        }



    }

    private void EnemyMovement()
    {
        transform.position += Vector3.left * enemyMoveSpeed * Time.deltaTime; // moving towards the player which is left
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
        lr.positionCount = 2; // this needs to be 2, one line per fish
        lr.widthMultiplier = 0.05f;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = Color.white;
        lr.endColor = Color.white;
        return lr;
    }
}