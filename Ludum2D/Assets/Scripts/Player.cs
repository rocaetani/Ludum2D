using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{

    [Header("Movement")]
    public float Velocity;

    public float VelocitySideways;


    private Vector3 _direction;

    //private Vector3 _direction2ponto0 = new Vector3();

    private Animator animationController;

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

    [Header("Player State")]
    public PlayerState playerState;
    public enum PlayerState
    {
        GoingDown,
        GoingUp,
        Dead
    }

    void Start()
    {
        AirAmount = AirMaximum;
        VelocitySideways = 5;
        animationController = GetComponent<Animator>();

        _direction = Vector3.up * GoingDirection();
        animationController.SetBool("GoingUp", playerState == PlayerState.GoingUp);
    }

    void Update()
    {
        Move();
        LoseAir();
        if(AirAmount < 0)
        {
            PlayerDead();
        }

    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerState != PlayerState.GoingUp)
        {
            playerState = PlayerState.GoingUp;
            _direction = Vector3.up;
            animationController.SetBool("GoingUp", true);
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
        animationController.SetBool("IsDed", true);
        playerState = PlayerState.Dead;
        StartCoroutine(WaitForDeath());
    }

    public void GameOver()
    {
        Debug.Log("Implementar aqui a morte do personagem");
    }

    IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(3f);
        GameOver();
    }
    public int GoingDirection()
    {
        return playerState == PlayerState.GoingUp? 1 : -1;
    }

    void OnGUI()
    {
        GUI.color = Color.red;



        GUI.Label(new Rect(100, 10, 150, 100), AirAmount + "");

        GUI.Label(new Rect(100, 20, 150, 100), Time.time + "");

        GUI.Label(new Rect(100, 30, 150, 100), transform.position.y + "");
    }

}
