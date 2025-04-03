using UnityEngine;
public class SpaceshipController : MonoBehaviour
{
    
    // Adjust in editor. A "normal" feeling speed is around 1f. 
    public float thrustForce;
    public float brakeForce;

    // how much force is applied to the rotation
    public float rotationTorque;

    // how quickly rotation slows down after letting go of the button
    public float rotationDrag;

    // 5ish feels normal, so maybe just 5x thrustForce is a decent rule. 
    public float maxSpeed;

    // This value is degrees/sec, so higher number like 200ish feels normal.
    public float maxAngularSpeed;

    // Game objects
    private Rigidbody2D rb;
    private Camera mainCam;

    // Thruster variables!
    public ParticleSystem mainThruster;
    public ParticleSystem rightThruster;
    public ParticleSystem leftThruster;
    public ParticleSystem frontThruster1;
    public ParticleSystem frontThruster2;


    // Shooting variables
    public GameObject projectilePrefab;
    // How far from the center either left or right projectiles should spawn. This number looked good in testing, so hard coded. 
    private float lateralOffset = 0.3f;
    // how often you can shoot
    public float fireCooldown = 0.25f;
    // Starts negative so you can shoot at game start
    private float lastFireTime = -100;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // applies the set drag value to the rb
        rb.angularDamping = rotationDrag;

        mainCam = Camera.main;
    }

    void Update()
    {
        // Thrust!
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(transform.up * thrustForce);
            mainThruster.Emit(1);
        }

        // Brake! It's negative because going backwards
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(-transform.up * brakeForce);
            frontThruster1.Emit(5);
            frontThruster2.Emit(5);
        }

        // Rotate left or right!  
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddTorque(rotationTorque);
            rightThruster.Emit(5);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddTorque(-rotationTorque);
            leftThruster.Emit(5);
        }

        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) && Time.time >= lastFireTime + fireCooldown)
        {
            Shoot();
            lastFireTime = Time.time;
        }

        LimitSpeed();
        HandleScreenWrap();
    }

    //Ensures speeds don't get too crazy
    void LimitSpeed()
    {
        
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }

       
        if (Mathf.Abs(rb.angularVelocity) > maxAngularSpeed)
        {
            rb.angularVelocity = Mathf.Sign(rb.angularVelocity) * maxAngularSpeed;
        }
    }

    //Screenwrap! When you go out of bounds on one side, teleport to the oposite side of the screen.
    void HandleScreenWrap()
    {
        Vector3 newPosition = transform.position;

        // Convert screen edges to world coordinates
        Vector2 camSize = new Vector2(
            mainCam.orthographicSize * mainCam.aspect,
            mainCam.orthographicSize
        );

        if (transform.position.x > camSize.x)
        {
            newPosition.x = -camSize.x;
        }

        else if (transform.position.x < -camSize.x)
        {
            newPosition.x = camSize.x;
        }

        if (transform.position.y > camSize.y)
        {
            newPosition.y = -camSize.y;
        }

        else if (transform.position.y < -camSize.y)
        {
            newPosition.y = camSize.y;
        }

        transform.position = newPosition;
    }

    //Creates projectiles, slightly spreadout. Projectiles have their own code to handle movement. 
    void Shoot()
    {
        Instantiate(projectilePrefab, (transform.position - transform.right * lateralOffset), transform.rotation);
        Instantiate(projectilePrefab, (transform.position + transform.right * lateralOffset), transform.rotation);
    }
}