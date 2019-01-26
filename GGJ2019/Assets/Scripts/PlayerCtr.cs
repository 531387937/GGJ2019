using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtr : MonoBehaviour
{
    public enum State {MoveState,AttackState,FlashState,HookState, RopeState }
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
    public Camera ca;
    private Vector3 currentHookDir;
    public float HookLen;//钩子的长度
    private bool HookBacking = false;
    private LineRenderer HookLine;
    public float HookTime;
    public float grav_force;
    public float force;
    public float drag;
    public AudioSource AttackSound;
    public AudioSource RopeSound;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        currentSpeed = FlashSpeed;
        currentState = State.MoveState;
        HookLine = Hook.GetComponent<LineRenderer>();
        
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
            case State.RopeState:
                OnRopeState();
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
        if (/*timer < FlashTime && !Flash && */Input.GetKeyDown(KeyCode.LeftShift) && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") >= 0)&&Flash)
        {
            currentState = State.FlashState;
            currentDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            
            //Move = false;
        }
        //勾人ing
        if(Input.GetMouseButtonDown(1))
        {
            RopeSound.Play();
            HookBacking = false;
            Hook.transform.position = gameObject.transform.position;
            rig.velocity = Vector2.zero;
           currentHookDir =  (ca.ScreenToWorldPoint(Input.mousePosition)-gameObject.transform.position);
            currentState = State.HookState;           
        }
        if(Input.GetMouseButtonDown(0))
        {
            AttackSound.Play();
            currentState = State.AttackState;
        }
    }
    void OnAttackState()
    {

    }
    void OnHookState()
    {if (Hook.transform.childCount != 0)
        {
            if (Vector2.Distance(Hook.transform.GetChild(0).position, transform.position) <= 1.6f)

            {
                Hook.transform.GetChild(0).SetParent(null);
            }
        }
        HookLine.SetPosition(0, gameObject.transform.position);
        HookLine.SetPosition(1, Hook.gameObject.transform.position);
        rig.simulated = false;
        rig.velocity = Vector2.zero;
        Hook.SetActive(true);
        if(timer<HookTime/2)
        Hook.gameObject.transform.Translate(new Vector2(currentHookDir.x, currentHookDir.y).normalized * HookLen * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer >= (HookTime/2))
        {

            Hook.gameObject.transform.Translate(new Vector2(currentHookDir.x, currentHookDir.y).normalized * HookLen * Time.deltaTime*-1);
        }
        //if(timer>=(HookTime-0.2f))
        //{
        //    Hook.transform.GetChild(0).SetParent(null);
        //}
            if (timer>=HookTime)
        {
            
            rig.simulated = true;
            timer = 0;
            currentState = State.MoveState;
            Hook.SetActive(false);
        }

    }
    void OnFlashState()
    {
        rig.simulated = false;
        Flash = false;
        rig.AddForce(-Physics2D.gravity);
        currentSpeed = Mathf.MoveTowards(currentSpeed, 0, FlashTime * 10);
        gameObject.transform.Translate(currentDir.normalized * currentSpeed * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer >= FlashTime)
        { rig.simulated = true;
            timer = 0;
            currentSpeed = FlashSpeed;
            currentState = State.MoveState;         
        }
    }
    void OnRopeState()
    {
        if (rig.simulated==false)
        {
            rig.velocity = Vector3.zero;
            rig.gravityScale = 0;
            rig.simulated = true;
            rig.drag = drag;
        }
        HookLine.SetPosition(0, gameObject.transform.position);
        HookLine.SetPosition(1, Hook.gameObject.transform.position);

        Vector3 d = gameObject.transform.position - Hook.transform.position;
        Debug.Log(Vector3.Distance(Vector3.zero, d));
        d = Vector3.Normalize(d);
        //Vector2 dir = new Vector2(d.x, d.y);
        //Vector2 dir_Ver = new Vector2(-d.y, d.x);
        Vector3 d_ver = new Vector3(-d.y, d.x, 0);
        float angle = Mathf.Atan(d.x / d.y) ;

        //rig.AddForce(d_ver * 100 , ForceMode2D.Force);
        Vector2 g = d - Vector3.down;



        rig.AddForce(d_ver * grav_force * Mathf.Sin(angle), ForceMode2D.Force);
        //rig.velocity = d_ver * grav_force * Mathf.Sin(angle);



        //Debug.Log("Before:"+rig.velocity);

        if (Input.GetKey(KeyCode.D))
        {

             rig.AddForce(d_ver * force * Mathf.Cos(angle), ForceMode2D.Force);
            //rig.velocity = d_ver * force * Mathf.Cos(angle);
        }
        else
        {
           // rig.velocity = Vector3.zero;
        }
    
        if (Input.GetKey(KeyCode.A))
        {

            rig.AddForce(-d_ver * force * Mathf.Cos(angle), ForceMode2D.Force);

           // rig.velocity = -d_ver * force * Mathf.Cos(angle);
        }
        else
        {
           // rig.velocity = Vector3.zero;
        }



        if (Input.GetMouseButtonDown(1))
        {
            rig.gravityScale = 1.5f;
            timer = 0;
            currentState = State.MoveState;
            Hook.SetActive(false);
            rig.drag = 2;
          
            
            rig.velocity = d_ver * Vector2.Distance(Vector2.zero, rig.velocity);
            Debug.Log(rig.velocity);
        }


    }

    void SwitchRopeState()
    {
        currentState = State.RopeState;
    }
    void HookBack()
    {
        if (!HookBacking)
        { timer = HookTime - timer;
            HookBacking = true;
        }
    }
    void HookChildBack(GameObject Enemy)
    {
        if (!HookBacking)
        {
            if (Vector2.Distance(Enemy.transform.position, transform.position) > 1.7f)
            { Enemy.transform.SetParent(Hook.transform); }
            timer = HookTime - timer;
            HookBacking = true;
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
