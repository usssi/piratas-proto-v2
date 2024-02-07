using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BarcoInGameController : MonoBehaviour
{

    public GameObject menuBarcosEnemigos;

    public Color colorBarco;

    [Range (0,1)]
    public float velocidadDeMovimiento;
    public float posicionDeMovimientoX;
    public float posicionDeMovimientoY;

    public Vector2 initalPos;

    [Range (0f,0.4f)]
    public float tamaño;

    public TextMeshPro textoNivel;


    private void Start()
    {
        menuBarcosEnemigos = GameObject.FindGameObjectWithTag("menuBarcos");

        RandomSpawnPositionAndSpeed();

        RandomColorShip();

        RandomRotationAndScale();

        textoNivel.text = Random.Range(1, 4).ToString();
    }

    private void Update()
    {
        MoveShip();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bounds")
        {
            //print("colisiono con el oput of bounds");
            Destroy(this.gameObject);
        }

        if (collision.tag == "Player")
        {
            //print("colisiono con el oput of bounds");
            Destroy(collision.gameObject);

            Invoke("ReloadScene", 2);

        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DisplayHideMenuEnemigos()
    {
        if (menuBarcosEnemigos.transform.localScale == Vector3.zero)
        {
            menuBarcosEnemigos.transform.localScale = Vector3.one;
        }
        else if (menuBarcosEnemigos.transform.localScale == Vector3.one)
        {
            menuBarcosEnemigos.transform.localScale = Vector3.zero;
        }
    }

    void OnMouseUp()
    {
        DisplayHideMenuEnemigos();

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
    }

    //esto despues tiene que cambiar para ajustarse al color que le corresponde
    //depende de su tipo con el scriptable object
    void RandomColorShip()
    {
        int i = Random.Range(0, 3);

        if (i == 0)
        {
            colorBarco = Color.red;
        }
        else if (i == 1)
        {
            colorBarco = Color.green;
        }
        else if (i == 2)
        {
            colorBarco = Color.blue;
        }
        else if (i == 3)
        {
            colorBarco = Color.white;
        }

        gameObject.GetComponent<SpriteRenderer>().color = colorBarco;

    }

    void RandomSpawnPositionAndSpeed()
    {
        //velocidad random
        float iV = Random.Range(0.1f, .3f);
        velocidadDeMovimiento = iV;

        //random spawn point

        int iX = Random.Range(-7, 7);
        int iY = Random.Range(-3, 4);
        //print("sexo" + iX + iY);

        transform.localPosition = new Vector3(iX, iY, 0);

        initalPos = transform.localPosition;
    }

    void RandomRotationAndScale()
    {
        int i = Random.Range(0, 2);

        gameObject.transform.localScale = new Vector3(tamaño, tamaño, tamaño);

        if (i == 0)
        {
            //gameObject.transform.localScale = new Vector3(tamaño, tamaño, tamaño);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (i == 1)
        {
            //gameObject.transform.localScale = new Vector3(-tamaño, tamaño, tamaño);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

        }

        int zR = Random.Range(-45, 45);
        gameObject.transform.rotation = new Quaternion(0, 0, zR, 113);

    }

    void MoveShip()
    {
        //determina x & y negativa o positiva
        if (/*transform.localScale == new Vector3(-tamaño, tamaño, tamaño)*/gameObject.GetComponent<SpriteRenderer>().flipX == false)
        {
            posicionDeMovimientoX -= velocidadDeMovimiento * Time.deltaTime;

            if (transform.rotation.z > 0)
            {
                posicionDeMovimientoY -= transform.rotation.z * velocidadDeMovimiento * Time.deltaTime;

            }
            else if (transform.rotation.z < 0)
            {
                posicionDeMovimientoY += -transform.rotation.z * velocidadDeMovimiento * Time.deltaTime;

            }
        }
        else if (/*transform.localScale == new Vector3(tamaño, tamaño, tamaño)*/gameObject.GetComponent<SpriteRenderer>().flipX == true)
        {
            posicionDeMovimientoX += velocidadDeMovimiento * Time.deltaTime;

            if (transform.rotation.z > 0)
            {
                posicionDeMovimientoY += transform.rotation.z * velocidadDeMovimiento * Time.deltaTime;

            }
            else if (transform.rotation.z < 0)
            {
                posicionDeMovimientoY -= -transform.rotation.z * velocidadDeMovimiento * Time.deltaTime;

            }
        }

        gameObject.transform.localPosition = new Vector2(initalPos.x + posicionDeMovimientoX, initalPos.y + posicionDeMovimientoY);
    }
}
