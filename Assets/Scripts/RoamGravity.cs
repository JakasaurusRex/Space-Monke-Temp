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

    private Rigidbody2D _rb;
    [SerializeField] private LayerMask _layerMask;


    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, BodyRange);
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    //can use an Enum and a switch to make this elegant
    void Update()
    {
        //catch input for launch angle adjustment
        if (Launching)
        {
            //handle launch button and behavior
            _currentLaunchAngle = handleLaunchAngle(_currentLaunchAngle);
            //handle a launch
            handleLaunch();
        }
        else if (Zeroed)
        {
            _rb.velocity = Vector2.zero;
        }
        else if (Active)
        {
            _rb.AddForce(CalculateForces(), ForceMode2D.Impulse);
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
            var force = (GravityTweak * Gravity * ShipMass * cl.GetComponent<Rigidbody2D>().mass) / distanceSquared;

            //Normalization
            forcesSum += (force * (heading / heading.magnitude));
            //Debug.Log("Force added from " + cl.gameObject.name + ": " + force);
        }

        return forcesSum;
    }

    private float handleLaunchAngle(float currentAngle)
    {
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
            return currentAngle + (L ? -1 : 1) * Time.deltaTime * AngleSensitivity;
        }
    }

    private void handleLaunch()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //lerp the power back and forth between low and high

        } else if (Input.GetKeyUp(KeyCode.Space))
        {
            //launch the craft
        }
    }
}
