using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [SerializeField] public Entity entity;
    [SerializeField] protected float baseMoveSpeed = 5f;
    [SerializeField] protected bool canMove = true;
    [SerializeField] protected bool isActing = false;
    [SerializeField] public GameObject PlayerModel;
    [SerializeField] public Transform AttackOffset;
    [SerializeField] public EntityAttack AttackAbility;

    Vector3 AnimationDirection;
    protected Rigidbody rigidbody;
    protected PlayerInput playerInput;
    [HideInInspector]
    Cooldown AttackCooldown = new Cooldown();

    // Start is called before the first frame update


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        AttackCooldown.Set(0f);
        entity = PlayerData.instance.PlayerEntity;
        entity.EntityObject = this.gameObject;
    }

    private void FixedUpdate()
    {
        if (!entity.Dead)
        {
            SetPlayerModelRotation();
            Move();
        }
    }

    void Update()
    {
        AttackCooldown.Tick();
        entity.FacingPos = AttackOffset.position;
        entity.FacingRot = PlayerModel.transform.rotation;
        if (canMove && !isActing)
        {
            if (playerInput.actions["Attack1"].IsPressed()) Attack();
            if (playerInput.actions["Dodge"].IsPressed()) Dodge();
        }

    }

    private void Move()
    {
        if (!canMove)
        {
            rigidbody.velocity = Vector3.zero;
            return;
        }
        //GET INPUT
        Vector3 mov = new Vector3(
            playerInput.actions["Move"].ReadValue<Vector2>().x,
            Mathf.Clamp(rigidbody.velocity.y,-Mathf.Infinity,0), 
            playerInput.actions["Move"].ReadValue<Vector2>().y);
        mov = mov.normalized * baseMoveSpeed;
        mov = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up) * mov;
        //SET VELOCITY
        if (Mathf.Abs(mov.x) >= 0.25f || Mathf.Abs(mov.z) >= 0.25f) GetComponent<Rigidbody>().velocity = new Vector3(mov.x + (mov.x * entity.stats.MoveIncrease), Mathf.Clamp(rigidbody.velocity.y, -Mathf.Infinity, 0),mov.z + (mov.z * entity.stats.MoveIncrease));
        else GetComponent<Rigidbody>().velocity = new Vector3(0,Mathf.Clamp(rigidbody.velocity.y, -Mathf.Infinity, 0), 0);
    }

    public void Attack()
    {
        if(AttackCooldown.Up())
        {
            entity.ProcOnAttack();
            AttackCooldown.Set(entity.stats.GetAttackCooldown(AttackAbility.Cooldown));
            AttackAbility.Execute(entity);
            SoundManager.PlaySound(SoundManager.Sound.PlayerAttack, Vector3.zero);
        }
    }

    public void Dodge()
    {

    }
    public void SetPlayerModelRotation()
    {
        if (PlayerModel == null) return;

        Vector3 direction = Vector3.zero;

        //GET INPUT
        if (Mathf.Abs(playerInput.actions["Aim"].ReadValue<Vector2>().x) > 0.25 || Mathf.Abs(playerInput.actions["Aim"].ReadValue<Vector2>().y) > 0.25)
            direction = new Vector3(playerInput.actions["Aim"].ReadValue<Vector2>().x, 0, playerInput.actions["Aim"].ReadValue<Vector2>().y);
        else
        {
            direction = new Vector3(playerInput.actions["Move"].ReadValue<Vector2>().x, 0, playerInput.actions["Move"].ReadValue<Vector2>().y).normalized;

            if (true)//check for mouse
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;

                if(Physics.Raycast(ray, out hit, Mathf.Infinity, 31))
                {
                    direction = new Vector3(transform.position.x - hit.point.x, 0,transform.position.z - hit.point.z);
                }
    
            }        
        }
        direction = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up) * direction;
        if (direction.magnitude > 0.25f)
        {
            Quaternion rotGoal = Quaternion.LookRotation(direction);
            PlayerModel.transform.rotation = Quaternion.Slerp(PlayerModel.transform.rotation, rotGoal, 0.25f);
        }

        Vector3 mov = rigidbody.velocity;

        //Rotate the velocity so that the player faces where they are looking
        //This is some fucked up shit, but it gets the correct results for some reason
        mov = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up) * mov;
        mov = Quaternion.AngleAxis(-PlayerModel.transform.eulerAngles.y, Vector3.up) * mov;
        AnimationDirection = Vector3.Lerp(AnimationDirection, mov, 0.25f);
        //anim.SetFloat("MoveZ", -AnimationDirection.z);
        //anim.SetFloat("MoveX", -AnimationDirection.x);
    }
    //Allows outsiders to be able to set wether this player can move
    public void SetMovable(bool move)
    {
        canMove = move;
    }
    //Allows outsiders to be able to get wether this player can move
    public bool GetMovable()
    {
        return canMove;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hitbox_Enemy" && other.GetComponent<EntityHitbox>() != null)
        {
            other.GetComponent<EntityHitbox>().ApplyDamage(entity);
        }
    }
}
