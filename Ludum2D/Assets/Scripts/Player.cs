using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{

    [Header("Movement")]
    public float Velocity;
    public float Score;

    public float SidewaysImpulse;

    private Vector2 _direction;
    private int _sidewaysHeading;
    private Rigidbody2D _rb;
    private Coroutine _sidewaysMovement = null;

    [Header("Game Objects")]
    public GameObject loserScreen;
    public GameObject winnerScreen;
    public GameObject anchor;
    public GameObject caradoCara;
    public GameObject musica1;
    public GameObject musica2;
    public GameObject soundDeath;
    public GameObject soundWin;
    public GameObject tecladoAparecendo;

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
        _rb = gameObject.GetComponent<Rigidbody2D>();

        AirAmount = AirMaximum;
        SidewaysImpulse = 5;
        animationController = GetComponent<Animator>();

        _sidewaysHeading = 1;

        _direction = Vector2.up * GoingDirection();
        animationController.SetBool("GoingUp", playerState == PlayerState.GoingUp);

        Score = 0;
    }

    void FixedUpdate() {
        if(playerState == PlayerState.Dead) {
            return;
        }

        Move();
    }

    void Update()
    {
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
        if (Input.GetKeyDown(KeyCode.Space) && playerState != PlayerState.GoingUp)
        {
            startMovingUp();
        }
        _rb.position += Time.deltaTime * Velocity * _direction;
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

    public void MoveSideways(GameObject towards)
    {
        if(_sidewaysMovement == null) {
            _sidewaysMovement = StartCoroutine(fixedUpdateMoveSideways(towards.transform.position));
        }

    }

    private IEnumerator fixedUpdateMoveSideways(Vector3 towards) {
        yield return new WaitForFixedUpdate();

        animationController.SetBool("MovingSideways", true);

        Vector3 heading = towards.x < _rb.transform.position.x? Vector3.left : Vector3.right;

        #if UNITY_EDITOR
        Debug.DrawRay(_rb.transform.position, heading, Color.yellow, 1);
        #endif

        int direction = Vector3.Dot(Vector3.right, heading) > 0? 1 : -1;
        if(direction != _sidewaysHeading) {
            _sidewaysHeading = direction;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        _rb.AddForce(SidewaysImpulse * direction * Vector2.right, ForceMode2D.Impulse);

        _sidewaysMovement = null;
    }

    public void stopSidewaysAnimation() => animationController.SetBool("MovingSideways", false);

    public void PlayerDead()
    {
        animationController.SetBool("IsDed", true);
        playerState = PlayerState.Dead;
        tecladoAparecendo.SetActive(false);
        musica1.SetActive(false);
        musica2.SetActive(false);
        DecreasePerSecondPerButton = 0;
        DecreasePerSecond = 0;
        soundDeath.SetActive(true);
        StartCoroutine(WaitForDeath());
    }

    public void PlayerSurface()
    {
        tecladoAparecendo.SetActive(false);
        animationController.SetBool("IsWon", true);
        Velocity = 0;
        anchor.SetActive(false);
        musica1.SetActive(false);
        musica2.SetActive(false);
        soundWin.SetActive(true);
        caradoCara.SetActive(false);
        anchor.SetActive(false);
        DecreasePerSecondPerButton = 0;
        DecreasePerSecond = 0;
        winnerScreen.SetActive(true);

    }

    public void GameOver()
    {
        loserScreen.SetActive(true);
        caradoCara.SetActive(false);
        anchor.SetActive(false);
        soundDeath.SetActive(true);
        GameObjectAccess.MainCamera.gameObject.transform.parent = null;
        _rb.gravityScale = 1;
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

    private void startMovingUp() {
        playerState = PlayerState.GoingUp;
        _direction = Vector3.up;
        animationController.SetBool("GoingUp", true);
        musica1.SetActive(false);
        musica2.SetActive(true);

        Score = -transform.position.y;

        GameObject attachedCable = gameObject.findChildWithTag("Cable");
        if(attachedCable != null) {
            // Detach cable from player
            attachedCable.transform.parent = null;
        }
    }

    
}
