using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtr : MonoBehaviour
{
    public enum State {MoveState,AttackState,FlashState,HookState }
    private State currentState;
    public float JumpForce = 300;
    public float speed;//移动速度
    private bool IsJumping=false;//是否在跳跃
    private bool Flash = false;//是否冲刺
    private bool Move = true;//是否无法移动
    public float FlashSpeed;//冲刺速度
    public float FlashTime;//冲刺时间
    private float timer;
    private Rigidbody2D rig;
    float currentSpeed;//冲刺时递减的速度
    Vector2 currentDir;
    private bool IsOnGround = true;//是否在地上
    public GameObject Hook;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        currentSpeed = FlashSpeed;
        currentState = State.MoveState;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case State.MoveState:
                OnMoveState();
                break;
            case State.AttackState:
                OnAttackState();
                break;
            case State.HookState:
                OnHookState();
                break;
            case State.FlashState:
                OnFlashState();
                break;
        }
        if(IsOnGround)
        {
            Flash = true;
        }
        //if(Input.GetAxis("Horizontal")!=0)
        //{
        //    gameObject.transform.Translate(Vector2.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);
        //}
        //if(Input.GetKeyDown(KeyCode.Space)&&!IsJumping)
        //{
        //    rig.AddForce(new Vector2(0, 300));
        //    IsJumping = true;
        //}
        //if(timer<FlashTime&&!Flash&&Input.GetKeyDown(KeyCode.LeftShift)&&(Input.GetAxis("Horizontal") != 0||Input.GetAxis("Vertical")!=0))
        //{
        //    Move = false;
        //    Flash = true;
        //}
        //if(Flash)
        //{
        //    rig.AddForce(-Physics2D.gravity);
            
        //    currentSpeed = Mathf.MoveTowards(currentSpeed, 0, FlashTime*10);
        //    gameObject.transform.Translate(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized*currentSpeed*Time.deltaTime);
        //    timer += Time.deltaTime;
        //}
        //if(timer>=FlashTime)
        //{
        //    Flash = false;
        //    Move = true;
        //    timer = 0;
        //    currentSpeed = FlashSpeed;
        //}
    }
    void OnMoveState()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            gameObject.transform.Translate(Vector2.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround)
        {
            rig.AddForce(new Vector2(0, JumpForce));
        }
        //进入冲刺阶段
        if (/*timer < FlashTime && !Flash && */Input.GetKeyDown(KeyCode.LeftShift) && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)&&Flash)
        {
            currentState = State.FlashState;
            currentDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            
            //Move = false;
        }
        //勾人ing
    }
    void OnAttackState()
    {

    }
    void OnHookState()
    {
        rig.AddForce(-Physics2D.gravity);
    }
    void OnFlashState()
    {
        Flash = false;
        rig.AddForce(-Physics2D.gravity);
        currentSpeed = Mathf.MoveTowards(currentSpeed, 0, FlashTime * 10);
        gameObject.transform.Translate(currentDir.normalized * currentSpeed * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer >= FlashTime)
        {
            timer = 0;
            currentSpeed = FlashSpeed;
            currentState = State.MoveState;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = false;
        }
    }
}
