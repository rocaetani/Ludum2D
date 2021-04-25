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
    public GameObject musica1;
    public GameObject musica2;

    private Vector3 _direction;
    private int _sidewaysHeading;

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

        _sidewaysHeading = 1;

        _direction = Vector3.up * GoingDirection();
        animationController.SetBool("GoingUp", playerState == PlayerState.GoingUp);

        Score = 0;
    }

    void Update()
    {
        Move();
        MoveSideways();
        LoseAir();
        if(AirAmount < 0)
        {
            PlayerDead();
        }
        else if(playerState == PlayerState.GoingUp && (transform.position.y >= 0))
        {
            PlayerSurface();
        }

    }

    private void Move()
    {
        if(playerState == PlayerState.Dead) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && playerState != PlayerState.GoingUp)
        {
            startMovingUp();
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

    public void MoveSideways()
    {
        int direction = 0;
        if(Input.GetKey(KeyCode.LeftArrow)) {
            direction = -1;
        } else if(Input.GetKey(KeyCode.RightArrow)) {
            direction = 1;
        }

        if(direction != 0) {
            animationController.SetBool("MovingSideways", true);
            if(direction != _sidewaysHeading) {
                _sidewaysHeading = direction;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }

            print("side");
            transform.position += Time.deltaTime * VelocitySideways * direction * Vector3.right;
        } else {
            animationController.SetBool("MovingSideways", false);
        }

    }

    public void PlayerDead()
    {
        animationController.SetBool("IsDed", true);
        playerState = PlayerState.Dead;
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
        return playerState == PlayerState.GoingUp? 1 : -1;
    }

    void OnGUI()
    {
        GUI.color = Color.red;

        GUI.Label(new Rect(100, 10, 150, 100), AirAmount + "");

        GUI.Label(new Rect(100, 20, 150, 100), Time.time + "");

        GUI.Label(new Rect(100, 30, 150, 100), transform.position.y + "");
    }

    private void startMovingUp() {
        playerState = PlayerState.GoingUp;
        _direction = Vector3.up;
        animationController.SetBool("GoingUp", true);
        musica1.SetActive(false);
        musica2.SetActive(true);

        Score = transform.position.y;

        GameObject attachedCable = gameObject.findChildWithTag("Cable");
        if(attachedCable != null) {
            // Detach cable from player
            attachedCable.transform.parent = null;
        }
    }
}
