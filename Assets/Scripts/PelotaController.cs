using System.Collections;
using UnityEngine;

public class PelotaController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float force;
    [SerializeField] float delay;
    [SerializeField] GameManager gameManager;
    [SerializeField] AudioClip sfxPaddel;  
    [SerializeField] AudioClip sfxWall;    
    [SerializeField] AudioClip sfxFail;    
    AudioSource sfx;  

    const float MIN_ANG = 25.0f;
    const float MAX_ANG = 40.0f;

    const float MAX_Y = 2.5f;
    const float MIN_Y = -2.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sfx = GetComponent<AudioSource>();
        int direccionX = Random.Range(0, 2) == 0 ? -1 : 1; 
        StartCoroutine(LanzarPelota(direccionX));
    }

    IEnumerator LanzarPelota(int direccionX)
    {
        float angulo = Random.Range(MIN_ANG, MAX_ANG) * Mathf.Deg2Rad;
        float x = Mathf.Cos(angulo) * direccionX;



        int direccionY = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Mathf.Sin(angulo) * direccionY;

        float posY = Random.Range(MIN_Y, MAX_Y);
        transform.position = new Vector3(0, posY, 0);

        Vector2 impulso = new Vector2(x, y);
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(impulso * force, ForceMode2D.Impulse);


        yield return new WaitForSeconds(delay);
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Gol en " + other.tag + "!!");

        sfx.clip = sfxFail;
        sfx.Play();

        if (other.tag == "PorteriaIzquierda")
        {

            gameManager.AddPointP1();
            StartCoroutine(LanzarPelota(1));
        }
        else if (other.tag == "PorteriaDerecha")
        {
            gameManager.AddPointP2();
            StartCoroutine(LanzarPelota(-1));
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        string tag = other.gameObject.tag;

        if (tag == "Pala1" || tag == "Pala2")
        {
            sfx.clip = sfxPaddel;
            sfx.Play();
        }

        if (tag == "LimiteSuperior" || tag == "LimiteInferior")
        {
            sfx.clip = sfxWall;
            sfx.Play();
        }
    }
}
