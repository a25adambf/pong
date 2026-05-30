using UnityEngine;
using System.Collections;
 
public class PelotaController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float force;
    [SerializeField] float delay;
    [SerializeField] GameManager gameManager;
 
    const float MIN_ANG = 25.0f;
    const float MAX_ANG = 40.0f;
 
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        transform.position = Vector3.zero;
  
        int directionX = Random.Range(0, 2) == 0 ? -1 : 1;
        StartCoroutine(throwBall(directionX));
    }
 
    IEnumerator throwBall(int directionX)
    {
        transform.position = Vector3.zero;
        rb.linearVelocity = Vector2.zero;
 
        yield return new WaitForSeconds(delay);  
  
        float angulo = Random.Range(MIN_ANG, MAX_ANG) * Mathf.Deg2Rad;
        int directionY = Random.Range(0, 2) == 0 ? -1 : 1;
 
        float x = Mathf.Cos(angulo) * directionX;
        float y = Mathf.Sin(angulo) * directionY;
  
        rb.AddForce(new Vector2(x, y) * force, ForceMode2D.Impulse);  
    }
 
    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// Resets the ball to center and stops its movement.
    /// Called by GameManager when restarting.
    /// </summary>
    public void ResetBall()
    {
        StopAllCoroutines();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
        transform.position = Vector3.zero;
    }
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
  
        if (tag.Equals("pala1") || tag.Equals("pala2"))
        {
            // Play paddle hit sound
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlayPaddleHit();
        }
        else
        {
            // Wall/border collision
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlayWallBounce();
        }
    }
 
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Gol en " + collider.tag + "!!");
        
        // Play goal sound
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlayGoal();
        
        if (collider.tag.Equals("GoalLeft"))
        {
            gameManager.AddPointP1();
            if (gameManager.IsRunning())
                StartCoroutine(throwBall(1));
        }
        else if (collider.tag.Equals("GoalRight"))
        {
            gameManager.AddPointP2();
            if (gameManager.IsRunning())
                StartCoroutine(throwBall(-1));
        }
    }
}

