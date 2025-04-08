using System;
using System.Collections;
using UnityEngine;
public class SpaceshipController : MonoBehaviour
{

    /*
    Ship state variables
    */

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
    public int health;
    public bool altShot;
    public bool tempInvincibility;
    // How long you're invincible for after being hit
    public float iFrames;
    private Color baseColor = Color.white;

    /*
    Game objects
    */
    private Rigidbody2D rb;
    private Camera mainCam;
    private AudioSource[] audios;
    private SpriteRenderer shipSprite;


    /*
    Thruster variables
    */
 
    public ParticleSystem mainThruster;
    public ParticleSystem rightThruster;
    public ParticleSystem leftThruster;
    public ParticleSystem frontThruster1;
    public ParticleSystem frontThruster2;


    /*
    Shooting variables
    */

    public GameObject projectilePrefab;
    // How far from the center either left or right projectiles should spawn. This number looked good in testing, so hard coded. 
    private float lateralOffset = 0.3f;
    // how often you can shoot
    public float fireCooldown = 0.25f;
    public float altCooldown = 1.0f;
    // Starts negative so you can shoot at game start
    private float lastFireTime = -100;

    public GameObject altProjectile;
    public float spreadAngle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // applies the set drag value to the rb
        rb.angularDamping = rotationDrag;

        mainCam = Camera.main;
        health = 3;
        altShot = false;
        tempInvincibility = false;

        audios = GetComponentsInChildren<AudioSource>();
        shipSprite = GetComponent<SpriteRenderer>();
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

        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)))
        {
            if(altShot)
            {
                ShootAlt();
            }

            else
            {
                Shoot();
            }
        }

        /*
        //Cheat codes for testing purposes. Commented out for production. 
        if (Input.GetKeyDown(KeyCode.O))
        {
            setAlt();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            heal();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            setImmune();
        }
        */

        LimitSpeed();
        HandleScreenWrap();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!tempInvincibility && collision.gameObject.tag == "Obstacle")
        {
            takeDamage();
        }
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
        if (Time.time < lastFireTime + fireCooldown) return;

        audios[0].Play();

        Instantiate(projectilePrefab, (transform.position - transform.right * lateralOffset), transform.rotation);
        Instantiate(projectilePrefab, (transform.position + transform.right * lateralOffset), transform.rotation);

        lastFireTime = Time.time;
    }

    void ShootAlt()
    {

        if (Time.time < lastFireTime + altCooldown) return;

        audios[1].Play();

        int pelletCount = UnityEngine.Random.Range(8, 13);

        for (int i = 0; i < pelletCount; i++)
        {
            float angleOffset = UnityEngine.Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
            Quaternion pelletRotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + angleOffset);

            Vector3 spawnPosition = transform.position + transform.up * 0.5f; // Slightly in front of ship

            Instantiate(altProjectile, spawnPosition, pelletRotation);
        }

        lastFireTime = Time.time;
    }

    void takeDamage()
    {
        health--;
        altShot = false;
        baseColor = Color.white;
        StartCoroutine(Ouch());

        if (health == 0)
        {
            StartCoroutine(Die());
        }

        else
        {
            audios[2].Play();
            tempInvincibility = true;
            StartCoroutine(ResetInvincibility());
        }
    }

    public void heal()
    {
        if(health != 3)
        {
            health++;
        }
        StartCoroutine(Heal());
    }

    public void setAlt()
    {
        altShot = true;
        ColorUtility.TryParseHtmlString("#C59D34", out Color orange);
        baseColor = orange;
        shipSprite.color = baseColor;
    }

    public void setImmune()
    {
        StartCoroutine(Immune());
    }

    private IEnumerator ResetInvincibility()
    {
        yield return new WaitForSeconds(iFrames);
        tempInvincibility = false;
    }

    private IEnumerator Die()
    {
        Time.timeScale = 0f;
        audios[3].Play();
        yield return new WaitForSecondsRealtime(audios[3].clip.length);
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lose");
    }

    private IEnumerator Ouch()
    {
        ColorUtility.TryParseHtmlString("#AC1E1E", out Color flashColor);

        flashColor.a = 0.5f;

        shipSprite.color = flashColor;

        yield return new WaitForSecondsRealtime(0.25f);

        shipSprite.color = baseColor;
    }

    private IEnumerator Heal()
    {
        ColorUtility.TryParseHtmlString("#25AC1E", out Color flashColor);

        flashColor.a = 0.5f;

        shipSprite.color = flashColor;

        yield return new WaitForSecondsRealtime(0.25f);

        shipSprite.color = baseColor;
    }

    private IEnumerator Immune()
    {
        tempInvincibility = true;

        ColorUtility.TryParseHtmlString("#A134E1", out Color flashColor);
        shipSprite.color = flashColor;

        yield return new WaitForSecondsRealtime(2f);

        tempInvincibility = false;
        shipSprite.color = baseColor;
    }
}