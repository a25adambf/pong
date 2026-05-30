using UnityEngine;

public class PalaController : MonoBehaviour
{
    const float MaxY = 4.2f;
    const float MinY = -4.2f;
    [SerializeField] float speed = 10f;
 
    void Update()
    {
        if (gameObject.CompareTag("pala2"))
        {
            if (Input.GetKey("up") && transform.position.y < MaxY)
            {
                // Movimiento hacia arriba
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            if (Input.GetKey("down") && transform.position.y > MinY)
            {
                // Movimiento hacia abajo
                transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
            }
        }
        else if (gameObject.CompareTag("pala1"))
        {
            if (Input.GetKey("w") && transform.position.y < MaxY)
            {
                // Movimiento hacia arriba
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            if (Input.GetKey("s") && transform.position.y > MinY)
            {
                // Movimiento hacia abajo
                transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
            }
        }
    }
}

