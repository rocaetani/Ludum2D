using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{

    [Header("Movement")]
    public float Velocity;
    public float Score;

    public float VelocitySideways;

    public GameObject loserScreen;
    public GameObject anchor;
    public GameObject caradoCara;
    private Vector3 _direction;

    //private Vector3 _direction2ponto0 = new Vector3();

    public bool GoingUp;
    public Animator anim;

    private int _currentSeconds;

    [Header("Air Bar")]
    public float AirMaximum;
    public float AirAmount;
    public float DecreasePerSecond;
    public float DecreasePerSecondPerButton;
    public float AirRatio {
        get {
            return AirAmount / AirMaximum;
        }
    }

    void Start()
    {
        AirAmount = AirMaximum;
        VelocitySideways = 5;
        _direction = Vector3.down;
        GoingUp = false;
        anim = GetComponent<Animator>();
        Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LoseAir();
        if(AirAmount < 1)
            PlayerDead();
        if(GoingUp && (transform.position.y >= 0))
            PlayerSurface();
            
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _direction = Vector3.up;
            GoingUp = true;
            anim.SetBool("GoingUp", true);
            Score = transform.position.y;

        }
        transform.position += Time.deltaTime * Velocity * _direction;
    }

    private void LoseAir()
    {
        AirAmount -= DecreasePerSecond * Time.deltaTime;
    }

    public void AddAirAmount(float airToAdd)
    {
        if (AirMaximum > AirAmount + airToAdd)
        {
            AirAmount += airToAdd;
        }
        else
        {
            AirAmount = AirMaximum;
        }
    }

    /*
    public void MoveSideways(Vector3 objPos)
    {
        _direction2ponto0.x = objPos.x;
        transform.position += Time.deltaTime * Velocity * _direction2ponto0; //sla se é isso, vejo depois

    }
    */
    public void PlayerDead()
    {
        anim.SetBool("IsDed", true);
        StartCoroutine(WaitForDeath());
    }

    public void PlayerSurface()
    {
        Debug.Log("colocar coisa aqui");
        Debug.Log(Score);
    }

    public void GameOver()
    {
        loserScreen.SetActive(true);
        caradoCara.SetActive(false);
        anchor.SetActive(false);
        DeactivateKeys();
        Debug.Log("Implementar aqui a morte do personagem");
    }


    public void DeactivateKeys()
    {
        //colocar aqui um método pra tirar as teclas que estão aparecendo na tela
    }
    IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(3f);
        GameOver();
    }
    public int GoingDirection()
    {
        if (GoingUp)
        {
            return 1;
        }

        return -1;
    }

    void OnGUI()
    {
        GUI.color = Color.red;



        GUI.Label(new Rect(100, 10, 150, 100), AirAmount + "");

        GUI.Label(new Rect(100, 20, 150, 100), Time.time + "");

        GUI.Label(new Rect(100, 30, 150, 100), transform.position.y + "");
    }

}
