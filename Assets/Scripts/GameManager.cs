using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int p1Score;
    int p2Score;
    bool running = false;

    [SerializeField] TMP_Text txtP1Score;
    [SerializeField] TMP_Text txtP2Score;
    [SerializeField] TMP_Text txtReglas;
    [SerializeField] GameObject pelota;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if (!running && Input.GetKeyDown(KeyCode.Space))
        {
            pelota.SetActive(true);

            running = true;

            txtReglas.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (p1Score == 9)
        {
            pelota.SetActive(false);
            txtReglas.text = "¡Ha ganado jugador 2!";
            txtReglas.gameObject.SetActive(true);
        }
        if (p2Score == 9)
        {
            pelota.SetActive(false);
            txtReglas.text = "¡Ha ganado jugador 1!";
            txtReglas.gameObject.SetActive(true);
        }
    }

    public void AddPointP1()
    {
        p1Score++;
        txtP1Score.text = p1Score.ToString();
    }
    public void AddPointP2()
    {
        p2Score++;
        txtP2Score.text = p2Score.ToString();
    }
}
