using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterFSM : MonoBehaviour, IKillable
{
    [Header("Character Settings")]
    [SerializeField] private bool characterUpOrDown;
    [SerializeField] private MainCharacterFSM otherCharacter;
    [SerializeField] private float minDistanceUnionX;
    [Header("Movements Settings")]
    [SerializeField] private float speed;
    [SerializeField][Range(0f,1f)] private float coefficientSpeedInAir;
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce;
    [Header("Smash Settings")]
    [SerializeField] private float smashForce;
    [Header("Climb Settings")]
    [SerializeField] private float climbSpeed;
    [SerializeField][Range(0f,0.1f)] private float thresholdY;
    [Header("Throw Arm Settings")]
    [SerializeField] private float speedTransition;
    [Header("Spawn Point")]
    [SerializeField] private Transform Spawn; 

    #region Finite State Machine
    private FSM fsmMC;
    #region States of FSM
    private FSMState IdleState;
    private FSMState MovementFloorState;
    private FSMState OnAirState;
    private FSMState ClimbState;
    private FSMState ThrowArmState;
    private FSMState AttackState;
    #endregion
    #region Actions of FSM
    private IdleAction Idle;
    private MovementAction MovementFloor;
    private MovementAction MovementAir;
    private JumpAction JumpFloor;
    private JumpAction JumpAir;
    private SmashAction Smash;
    private ControlCharacterAction ControlCharacterIdle;
    private ControlCharacterAction ControlCharacterMoving;
    private ControlCharacterAction ControlCharacterAir;
    private ControlCharacterAction ControlCharacterClimbing;
    private ControlCharacterAction ControlCharacterThrowingArm;
    private AttackPlayer Attack;
    [HideInInspector] public CheckClimbAction CheckClimbOnFloor;
    [HideInInspector] public CheckClimbAction CheckClimbOnAir;
    [HideInInspector] public ClimbAction Climb;
    [HideInInspector] public ThrowArmAction ThrowArm;
    #endregion
    #endregion
    #region Private Variables
    private bool onAir;
    private Rigidbody2D myRigidbody;
    private BoxCollider2D myCollider;
    private Animator myAnimator;
    private Coroutine InteractionCoroutine;
    private string lastState;
    #endregion
    #region Public Variable
    [HideInInspector] public bool onControl;
    [HideInInspector] public bool onTransitionUnion;
    [HideInInspector] public Vector2 targetPosTransition;
    #endregion
    #region Gets And Sets
    public bool GetOnAir()
    {
        return onAir;
    }
    public bool GetCharacterUpOrDown()
    {
        return characterUpOrDown;
    }
    public MainCharacterFSM GetOtherCharacter()
    {
        return otherCharacter;
    }
    public FSMState GetCurrentState()
    {
        return fsmMC.GetCurrentState();
    }
    public FSMState GetMovementState()
    {
        return MovementFloorState;
    }
    public FSMState GetIdleState()
    {
        return IdleState;
    }
    public bool IsSmashing()
    {
        return Smash.GetIsSmashing();
    }
    public void SetOnAir(bool onAir)
    {
        this.onAir = onAir;
    }
    public void SetOnIdle()
    {
        lastState = GetCurrentState().Name;
        GetCurrentState().SendEvent("ToIdle");
    }
    public void SetOnMovement()
    {
        IdleState.SendEvent("ToMovementFloorState");
    }
    public void SetOnControl(bool onControl)
    {
        this.onControl = onControl;
    }
    #endregion
    private void Awake()
    {
        onAir = false;
        onControl = true;
        onTransitionUnion = false;
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        // Instantiate FSM
        fsmMC = new FSM("MCTest FSM");
        // Add States of FSM
        IdleState = fsmMC.AddState("IdleState");
        MovementFloorState = fsmMC.AddState("MovementState");
        OnAirState = fsmMC.AddState("OnAirState");
        ClimbState = fsmMC.AddState("ClimbState");
        ThrowArmState = fsmMC.AddState("ThrowArmState");
        AttackState = fsmMC.AddState("Attack");

        // Idle Actions
        Idle = new IdleAction(IdleState);
        ControlCharacterIdle = new ControlCharacterAction(IdleState);
        // MovementFloorState Actions
        MovementFloor = new MovementAction(MovementFloorState);
        JumpFloor = new JumpAction(MovementFloorState);
        CheckClimbOnFloor = new CheckClimbAction(MovementFloorState);
        ControlCharacterMoving = new ControlCharacterAction(MovementFloorState);
        // OnAirState Actions
        MovementAir = new MovementAction(OnAirState);
        JumpAir = new JumpAction(OnAirState);
        Smash = new SmashAction(OnAirState);
        CheckClimbOnAir = new CheckClimbAction(OnAirState);
        ControlCharacterAir = new ControlCharacterAction(OnAirState);
        // Climb Actions
        Climb = new ClimbAction(ClimbState);
        ControlCharacterClimbing = new ControlCharacterAction(ClimbState);
        // ThrowArm Actions
        ThrowArm = new ThrowArmAction(ThrowArmState);
        ControlCharacterThrowingArm = new ControlCharacterAction(ThrowArmState);
        // Attack State
        Attack = new AttackPlayer(AttackState);

        // Adds actions to the state
        IdleState.AddAction(Idle);
        IdleState.AddAction(ControlCharacterIdle);
        MovementFloorState.AddAction(MovementFloor);
        MovementFloorState.AddAction(JumpFloor);
        MovementFloorState.AddAction(CheckClimbOnFloor);
        MovementFloorState.AddAction(ControlCharacterMoving);
        OnAirState.AddAction(MovementAir);
        OnAirState.AddAction(JumpAir);
        OnAirState.AddAction(Smash);
        OnAirState.AddAction(CheckClimbOnAir);
        OnAirState.AddAction(ControlCharacterAir);
        ClimbState.AddAction(Climb);
        ClimbState.AddAction(ControlCharacterClimbing);
        ThrowArmState.AddAction(ThrowArm);
        ThrowArmState.AddAction(ControlCharacterThrowingArm);
        AttackState.AddAction(Attack);

        // Set transition to the states
        IdleState.AddTransition("ToMovement", MovementFloorState);
        IdleState.AddTransition("ToClimb", ClimbState);
        IdleState.AddTransition("ToAttack", AttackState);
        MovementFloorState.AddTransition("ToClimb", ClimbState);
        MovementFloorState.AddTransition("ToOnAir", OnAirState);
        MovementFloorState.AddTransition("ToThrowArm", ThrowArmState);
        MovementFloorState.AddTransition("ToIdle", IdleState);
        MovementFloorState.AddTransition("ToAttack", AttackState);
        OnAirState.AddTransition("ToMovement", MovementFloorState);
        OnAirState.AddTransition("ToClimb", ClimbState);
        OnAirState.AddTransition("ToIdle", IdleState);
        ClimbState.AddTransition("ToMovement", MovementFloorState);
        ClimbState.AddTransition("ToOnAir", OnAirState);
        ClimbState.AddTransition("ToIdle", IdleState);
        ThrowArmState.AddTransition("ToMovement", MovementFloorState);
        ThrowArmState.AddTransition("ToIdle", IdleState);
        AttackState.AddTransition("ToMovement", MovementFloorState);
        AttackState.AddTransition("ToOnAir", OnAirState);

        // Initializes the actions
        MovementFloor.Init(transform, speed);
        JumpFloor.Init(jumpForce, myRigidbody, Smash);
        CheckClimbOnFloor.Init(thresholdY, characterUpOrDown, false, myCollider, "ToClimb");
        MovementAir.Init(transform, speed * coefficientSpeedInAir);
        JumpAir.Init(jumpForce, myRigidbody, Smash);
        Smash.Init(smashForce, characterUpOrDown, myRigidbody, myCollider);
        CheckClimbOnAir.Init(thresholdY, characterUpOrDown, false, myCollider, "ToClimb");
        Climb.Init(climbSpeed, thresholdY, characterUpOrDown, myRigidbody, myCollider, transform, this, "ToMovement");
        ThrowArm.Init(speedTransition, myRigidbody, transform);
        ControlCharacterIdle.Init(minDistanceUnionX, this, otherCharacter, "ToMovement", "ToIdle");
        ControlCharacterMoving.Init(minDistanceUnionX, this, otherCharacter, "ToMovement", "ToIdle");
        ControlCharacterAir.Init(minDistanceUnionX, this, otherCharacter, "ToMovement", "ToIdle");
        ControlCharacterClimbing.Init(minDistanceUnionX, this, otherCharacter, "ToMovement", "ToIdle");
        ControlCharacterThrowingArm.Init(minDistanceUnionX, this, otherCharacter, "ToMovement", "ToIdle");

        // Starts FSM
        fsmMC.Start("MovementState");
    }
    // Update is called once per frame
    private void Update()
    {
        fsmMC.Update();
    }
    void FixedUpdate()
    {
        fsmMC.FixedUpdate();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Debug.Log("ON FLOOR");
            CheckClimbOnAir.SetOnAir(false);
            if(characterUpOrDown)
            {
                //if (Mathf.Abs(collision.collider.bounds.max.y - myCollider.bounds.min.y) < 0.05f)
                    ChangeStatesOnFloor();
            } else 
            {
                //if (Mathf.Abs(collision.collider.bounds.min.y - myCollider.bounds.max.y) < 0.05f)
                    ChangeStatesOnFloor();
            }
            onAir = false;
            return;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Debug.Log("ON AIR");
            CheckClimbOnAir.SetOnAir(true);
            if(!ThrowArm.GetInTransition() && !onAir && !onTransitionUnion){
                MovementFloorState.SendEvent("ToOnAir");
                onAir = true;
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable objectInteractable = collision.GetComponent<IInteractable>();
        if (objectInteractable != null)
            InteractionCoroutine = StartCoroutine(PressKeyInteraction(objectInteractable));
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IInteractable objectInteractable = collision.GetComponent<IInteractable>();
        if (objectInteractable != null) 
            StopCoroutine(InteractionCoroutine);
    }
    private void ChangeStatesOnFloor()
    {
        if (ThrowArm.GetInTransition())
        {
            if (onControl)
                ThrowArmState.SendEvent("ToMovement");
            else
                ThrowArmState.SendEvent("ToIdle");
        }
        else if (onAir && !onTransitionUnion)
        {
            if (onControl && GetCurrentState().Name != "IdleState")
                OnAirState.SendEvent("ToMovement");
            onAir = false;
        }
    }
    #region Coroutines
    private IEnumerator PressKeyInteraction(IInteractable objectInteractable)
    {
        yield return new WaitUntil(() => Input.GetButtonDown("Interact"));
        Debug.Log("HAZ LA INTERACCION");
        objectInteractable.Use();
        if (!objectInteractable.canMove)
        {
            onControl = false;
            otherCharacter.onControl = false;
            /*GetCurrentState().SendEvent("ToIdle");
            if(otherCharacter.GetCurrentState().Name != "IdleState")
                otherCharacter.GetCurrentState().SendEvent("ToIdle");*/
            SetOnIdle();
            otherCharacter.SetOnIdle();

        } else 
        {
            MovementFloor.SetSpeed(speed*objectInteractable.coefficientSpeed);
        }
    }
    private IEnumerator MoveAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        MoveAgain();
    }
    #endregion
    public void Kill()
    {
        myRigidbody.velocity = Vector2.zero;
        otherCharacter.myRigidbody.velocity = Vector2.zero;
        transform.position = new Vector3(Spawn.position.x, Spawn.position.y, transform.position.z);
        otherCharacter.transform.position = new Vector3(otherCharacter.Spawn.position.x, otherCharacter.Spawn.position.y, otherCharacter.transform.position.z);
    }
    #if UNITY_EDITOR
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        BoxCollider2D myCollider;
        Vector3 origin;
        float boundX;
        myCollider = GetComponent<BoxCollider2D>();
        boundX = myCollider.bounds.min.x + myCollider.size.x/2;
        origin = characterUpOrDown ? new Vector3(boundX, myCollider.bounds.min.y, transform.position.z) : new Vector3(boundX, myCollider.bounds.max.y, transform.position.z);
        Gizmos.DrawRay(new Ray(origin, Vector3.down * GetComponent<Rigidbody2D>().gravityScale));
    }
#endif
    #region Public Functions
    private void FinishAttackState()
    {
        if (onAir)
            AttackState.SendEvent("ToOnAir");
        else
            AttackState.SendEvent("ToMovement");
    }
    public void MoveAgain()
    {
        if (GetCurrentState().Name == "IdleState" && lastState != "IdleState")
        {
            IdleState.SendEvent("ToMovement");
            onControl = true;
        }
           
    }
    public void MoveAfterInteractions(float time)
    {
        StartCoroutine(MoveAfterTime(time));
    }
    #endregion
}