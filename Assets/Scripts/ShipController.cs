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

    //This value is degrees/sec, so higher number like 200ish feels normal.
    public float maxAngularSpeed;

    //Variables to force ship to stay in bounds
    public float maxX;
    public float maxY;
    public float minX;
    public float minY;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // applies the set drag value to the rb
        rb.angularDamping = rotationDrag;
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
}