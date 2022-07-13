using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

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
    public bool DebugMode = false;
    public float DebugKnock = 1;

    private GameObject _nearestBody;

    private Rigidbody2D _rb;
    [SerializeField] private LayerMask _layerMask;

    private GameObject _launchPowerLine;
    private LineRenderer lr;
    private bool prepped = false;

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, BodyRange);
    }

    void Start()
    {
        SetupLine(Color.blue, LaunchForceScale, LineWidth, lineMaterial);
        _launchPowerLine.SetActive(false);
        _rb = GetComponent<Rigidbody2D>();
        if (DebugMode)
        {
            _rb.AddForce(Vector2.up * DebugKnock, ForceMode2D.Impulse);
        }
    }

    //can use an Enum and a switch to make this elegant
    void Update()
    {
        if (DebugMode)
        {
            Debug.DrawLine(Planet.FindNearest(transform.position).position, transform.position);

        }
        //if trying to leave orbit, catch input for launch angle adjustment
        if (Launching)
        {
            //handle launch button and behavior
            if (!prepped)
            {
                PrepLaunch();
                prepped = true;
            }

            float lastLaunchAngle = _currentLaunchAngle;
            _currentLaunchAngle = updateLaunchingAngle(_currentLaunchAngle);
            if (_currentLaunchAngle != lastLaunchAngle)
            {
                Debug.Log("Current Launching Angle: " + _currentLaunchAngle);
            }
            //handle a launch
            handleLaunch();
        }
        //if not trying to launch, then tick gravity system
        else if (Active && !Zeroed)
        {
            _rb.AddForce(CalculateForces(), ForceMode2D.Impulse);
        }
        //freeze the ship if zeroed
        if (Zeroed)
        {
            _rb.velocity = Vector2.zero;
        }
    }

    private Vector2 CalculateForces()
    {
        //Debug.Log("Forces Calculating...");
        var forcesSum = Vector2.zero;
        //get all game objects matching tag "planet" in radius
        var cls = Physics2D.OverlapCircleAll(transform.position, BodyRange, _layerMask);
        foreach (var cl in cls)
        {
            //Debug.Log("Checking Planet");

            //self explanatory
            var distanceSquared = Mathf.Pow(Vector2.Distance(transform.position, cl.transform.position), 2);

            //"line" drawn between the objects
            Vector2 heading = cl.transform.position - transform.position;

            //GMM / R^2 equation for universal gravitation
            var force = ((GravityTweak * Gravity * ShipMass * cl.GetComponent<Rigidbody2D>().mass) / distanceSquared) * Time.deltaTime;

            //Normalization
            forcesSum += (force * (heading / heading.magnitude));
            //Debug.Log("Force added from " + cl.gameObject.name + ": " + force);
        }

        return forcesSum;
    }

    //zero the ship's velocity, set up an initial exit angle, and prep to launch
    private void PrepLaunch()
    {
        Debug.Log("Prepping for launch...");
        Zeroed = true;
        Planet nearest = Planet.FindNearest(transform.position);
        if(nearest == null)
        {
            Debug.LogError("No Planet found nearby. Do you have your planets marked with the appropriate script?");
        }
        //get a vector somewhat tangent to the orbit
        var initialVector2 = Vector2.Perpendicular(Planet.FindNearest(transform.position).position - transform.position);
        if (DebugMode)
        {
            Debug.DrawRay(transform.position, initialVector2, Color.white, 5);
        }
        var desiredVector2 = initialVector2 - (Vector2)(transform.position);
        //line between ship and planet
        float vectorAngle = Mathf.Acos(Mathf.Deg2Rad * desiredVector2.x);
        vectorAngle *= Mathf.Rad2Deg;
        if(desiredVector2.x < 0)
        {
            vectorAngle *= -1;
        }

        _currentLaunchAngle = vectorAngle;
        Debug.Log("Calculated launch angle in Prep: " + _currentLaunchAngle);
    }


    private float updateLaunchingAngle(float currentAngle)
    {
        Debug.Log("Updating Launch Angle");
        var L = Input.GetKey(KeyCode.A);
        var R = Input.GetKey(KeyCode.D);
        //ignore both and neither keys pressed
        if ((L && R) || (!L && !R))
        {
            return currentAngle;
        }
        //case where one key is pressed
        else
        {
            //if A is held, sweep left, if D is held, sweep right
            Debug.Log("Current angle: " + currentAngle);
            return currentAngle + (L ? -1 : 1) * Time.deltaTime * AngleSensitivity;
        }
    }

    private void handleLaunch()
    {
        Zeroed = true;
        if (Input.GetKey(KeyCode.Space))
        {
            _launchPowerLine.SetActive(true);
            Vector2 pos = transform.position;
            //lerp the power back and forth between low and high
            lr.SetPosition(1, pos + (UtilFunctions.DegreeToVector2(_currentLaunchAngle) * LaunchForceScale * Mathf.Sin(Time.time)));
        } else if (Input.GetKeyUp(KeyCode.Space))
        {
            //launch the craft
            _rb.AddForce(UtilFunctions.DegreeToVector2(_currentLaunchAngle), ForceMode2D.Impulse);
            //we are launched, no longer launching, and we are no longer prepped for a launch
            Launching = false;
            prepped = false;
            _launchPowerLine.SetActive(false);
            Destroy(_launchPowerLine);
        }
    }


    //to manipulate the end of this line, just update the position of the first vertex
    void SetupLine(Color color, float scaling, float width, Material mat)
    {
        _launchPowerLine = new GameObject();
        _launchPowerLine.transform.position = transform.position;
        lr = _launchPowerLine.AddComponent<LineRenderer>();
        lr.material = mat;
        lr.startColor = lr.endColor = color;
        lr.startWidth = lr.endWidth = width;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, UtilFunctions.DegreeToVector2(_currentLaunchAngle) * scaling);
    }

}
