using UnityEngine;

[RequireComponent(typeof(PlatformNavigator), typeof(Rigidbody))]
public class NPC : MonoBehaviour
{
    // Inspector
    [SerializeField] private NPCData settings;
    public NPCData Data { get { return settings; } }

    public PlatformNavigator Navigator { get; private set; }
    public Collider NPCCollider { get; private set; }
    public Rigidbody rb { get; private set; }

    StateMachine<NPC> _stateMachine = new StateMachine<NPC>();

    public IdleState Idle { get; private set; }
    public MoveState Move { get; private set; }
    public JumpState Jump { get; private set; }
    public FallState Fall { get; private set; }

    void Awake()
    {
        Navigator = GetComponent<PlatformNavigator>();
        NPCCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    void Start() => InitStateMachine();

    private void InitStateMachine()
    {
        Idle = new IdleState(_stateMachine, this);
        Move = new MoveState(_stateMachine, this);
        Jump = new JumpState(_stateMachine, this);
        Fall = new FallState(_stateMachine, this);

        _stateMachine.InitState(Idle);
    }

    void Update() => _stateMachine.CurrentState.LogicUpdate();

    void FixedUpdate() => _stateMachine.CurrentState.PhysicsUpdate();
}
