using UnityEngine;
public class SpaceshipController : MonoBehaviour
{
    
    //Adjust in editor. A "normal" feeling speed is around 1f. 
    public float thrustForce;
    public float brakeForce;

    // how much force is applied to the rotation
    public float rotationTorque;

    // how quickly rotation slows down after letting go of the button
    public float rotationDrag;

    //5ish feels normal, so maybe just 5x thrustForce is a decent rule. 
    public float maxSpeed;

    //This value is degrees/sec, so higher number like 200ish feels normal.s
    public float maxAngularSpeed;

    private Rigidbody2D rb;
    private Camera mainCam;
    private Vector2 screenBounds;

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
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.up * thrustForce);
        }

        // Brake! It's negative because going backwards
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.up * brakeForce);
        }

        // Rotate left or right! Time.deltaTime makes rotating feel more weighty but also means the rotation speed has to be way way higher, so. 
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(rotationTorque);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(-rotationTorque);
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
}