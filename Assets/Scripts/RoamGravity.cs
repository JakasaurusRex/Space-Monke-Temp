using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class RoamGravity : MonoBehaviour
{
    private static readonly float Gravity = 6.67430f * Mathf.Pow(10, -11);
    public bool Active = true;
    public bool Zeroed = false;
    public bool Launching = false;
    [Tooltip("Gravity too weak? Crank it up!")]
    public float GravityTweak = 1;

    [Tooltip("The range at which a planet will influence the player, increase this for more precise physics at the cost of performance.")]
    public float BodyRange = 5;
    [Tooltip("Mass of the ship, tweak this to get better orbits.")]
    public float ShipMass = 1;
    private float _currentLaunchAngle = 0f;
    [Tooltip("This makes holding left and right sweep the launch angle faster")]
    public float AngleSensitivity = 1f;
    [Tooltip("This affects how fast the ship oscillates between min and max launch power")]
    public float LaunchSweepPeriod = 1;
    [Tooltip("This affects how hard the ship is launched before being scaled by the charge, also affects charge line size")]
    public float LaunchForceScale = 1;
    public Material lineMaterial;
    [Tooltip("Width of indicator lines")]
    public float LineWidth = .1f;
    [Tooltip("Indicator Line Length for Launches")]
    public float IndicatorLength = 5;
    [Tooltip("Which layers count as planets")]
    [SerializeField] private LayerMask _layerMask;
    [Tooltip("Below this distance two vectors will be considered equal")]
    [SerializeField] private float zeroFudgeFactor = .001f;
    [Tooltip("Tune how sensitive the face direction of movement behavior appears")]
    public float turnSmoothTime = 0.1f;
    [Tooltip("Controls the appearance of several gizmos and lines")]
    public bool DebugMode = false;
    public float DebugKnock = 1;
    private Rigidbody2D _rb;
    private GameObject _launchPowerLine;
    private LineRenderer lr;
    private bool prepped = false;
    Vector2 directionVector = Vector2.zero;
    private Vector2 myPos;
    private Vector2 lastPos;

    //input
    private PlayerControls _playerActions;
    private float inputAxis = 0;
    private bool spacebar = false;
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, BodyRange);
    }

    private void OnEnable()
    {
        _playerActions.LaunchScene.Enable();
    }
    void OnDisable()
    {
        _playerActions.LaunchScene.Disable();
    }

    private void Awake()
    {
        _playerActions = new PlayerControls();
    }

    void Start()
    {
        _playerActions.LaunchScene.Launching.performed += context =>
        {
            Debug.Log("Spacebar true");
            spacebar = true;
        };
        _playerActions.LaunchScene.Launching.canceled += context =>
        {
            Debug.Log("Spacebar false");
            spacebar = false;
        };

        lastPos = (Vector2)transform.position;
        myPos = (Vector2)transform.position;
        SetupLine(Color.blue, LineWidth, lineMaterial);
        _launchPowerLine.SetActive(false);
        _rb = GetComponent<Rigidbody2D>();
        if (DebugMode)
        {
            _rb.AddForce(Vector2.up * DebugKnock, ForceMode2D.Impulse);
        }
    }

    void Update()

    {
        UpdateInput();
        myPos = (Vector2)transform.position;
        if (DebugMode)
        {
            Debug.DrawLine(Planet.FindNearest(transform.position).position, transform.position);
        }
        lr.SetPosition(0, transform.position);
        if (spacebar)
        {
            Launching = true;
        }
        //if trying to leave orbit, catch input for launch angle adjustment
        if (Launching)
        {
            if (!prepped)
            {
                PrepLaunch();
                prepped = true;
            }

            UpdateLaunchingAngle();
            if (DebugMode)
            {
                Debug.Log("Current Launching Angle: " + _currentLaunchAngle);
            }
            lr.SetPosition(1, (UtilFunctions.DegreeToVector2(_currentLaunchAngle) * IndicatorLength) + myPos);
            //handle a launch
            HandleLaunch();
        }
        //if not trying to launch, then tick gravity system
        else if (Active && !Zeroed)
        {
            FaceMovement();
            _rb.AddForce(CalculateForces(), ForceMode2D.Impulse);
        }
        //freeze the ship if zeroed
        if (Zeroed)
        {
            _rb.velocity = Vector2.zero;
        }
        lastPos = myPos;
    }

    void FaceMovement()
    {
        var velVector = myPos - lastPos;
        if (DebugMode)
        {
            Debug.Log("Updating Bearing");
        }
        if (_rb.velocity.sqrMagnitude > zeroFudgeFactor)
        {
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg - 90);
        }
    }

    private Vector2 CalculateForces()
    {
        //GMM/R^2
        var forcesSum = Vector2.zero;
        //get all game objects matching tag "planet" in radius
        var cls = Physics2D.OverlapCircleAll(transform.position, BodyRange, _layerMask);
        foreach (var cl in cls)
        {
            var distanceSquared = Mathf.Pow(Vector2.Distance(transform.position, cl.transform.position), 2);
            //"line" drawn between the objects
            Vector2 heading = cl.transform.position - transform.position;
            var force = ((GravityTweak * Gravity * ShipMass * cl.GetComponent<Rigidbody2D>().mass) / distanceSquared) * Time.deltaTime;
            //Normalization
            forcesSum += (force * (heading / heading.magnitude));
        }

        return forcesSum;
    }

    //zero the ship's velocity, set up an initial exit angle, and prep to launch
    private void PrepLaunch()
    {
        Debug.Log("Prepping for launch...");
        Zeroed = true;
        _launchPowerLine.SetActive(true);

        //get a vector tangent to velocity
        directionVector = _rb.velocity;
        if (_rb.velocity.sqrMagnitude < zeroFudgeFactor)
        {
            Debug.Log("RB Vector considered 0 use simple tangent");
            directionVector = (Vector2)transform.position - (Vector2)Planet.FindNearest(transform.position).position;
        }
        if (DebugMode)
        {
            Debug.DrawRay(transform.position, directionVector, Color.green, 5);
        }

        //line between ship and planet
        float vectorAngle = Vector2.Angle(Vector2.right, directionVector);
        //if you are to the left of the planet, flip your heading to account for 2 quadrant nature of Vector2.Angle
        if (Planet.FindNearest(transform.position).position.x < transform.position.x)
        {
            Debug.Log("Correcting Angle");
            vectorAngle = 360 - vectorAngle;
        }
        _currentLaunchAngle = vectorAngle;
        transform.rotation = (Quaternion.Euler(0, 0, _currentLaunchAngle - 90));
        if (DebugMode)
        {
            Debug.Log("Calculated launch angle in Prep: " + _currentLaunchAngle);
        }
    }

    private void UpdateLaunchingAngle()
    {
        if (inputAxis == 0)
        {
            return;
        }
        //case where one key is pressed
        else
        {
            //if A is held, sweep left, if D is held, sweep right
            Debug.Log("Current angle: " + _currentLaunchAngle);
            var newAngle = _currentLaunchAngle + -inputAxis * Time.deltaTime * AngleSensitivity;
            _currentLaunchAngle = newAngle;
            transform.rotation = (Quaternion.Euler(0, 0, _currentLaunchAngle - 90));
        }
    }

    private void HandleLaunch()
    {
        Zeroed = true;
        //lerp launch from 0-1
        var wave = Mathf.Cos(Time.time * LaunchSweepPeriod) + 1;
        var directionVector = UtilFunctions.DegreeToVector2(_currentLaunchAngle).normalized;
        if (spacebar)
        {
            lr.SetPosition(1, (IndicatorLength * wave * directionVector) + myPos);
        }
        else
        {
            //launch the craft
            _rb.AddForce(LaunchForceScale * wave * directionVector, ForceMode2D.Impulse);
            //go back to pre launch state and remove indicator
            Launching = Zeroed = prepped = false;
            _launchPowerLine.SetActive(false);
        }
    }

    private void UpdateInput()
    {
        inputAxis = _playerActions.LaunchScene.Angling.ReadValue<float>();
    }

    //to manipulate the end of this line, just update the position of the first vertex
    void SetupLine(Color color, float width, Material mat)
    {
        _launchPowerLine = new GameObject();
        _launchPowerLine.transform.position = transform.position;
        lr = _launchPowerLine.AddComponent<LineRenderer>();
        lr.material = mat;
        lr.startColor = lr.endColor = color;
        lr.startWidth = lr.endWidth = width;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, (UtilFunctions.DegreeToVector2(_currentLaunchAngle) * IndicatorLength) + myPos);
    }

}
