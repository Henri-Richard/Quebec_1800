using Cinemachine;
using UnityEngine;

public class Controller : MonoBehaviour
{
    CharacterController controller;
    CinemachineFreeLook camFreeLook;

    Transform cam;
    Transform lightTr;
    Transform groundCheck;

    Vector3 velocity;
    [SerializeField] LayerMask groundMask;

    string HorizontalName;
    string VerticalName;
    [HideInInspector] public string ctrlName;
    [HideInInspector] public string MouseXName;
    [HideInInspector] public string MouseYName;

    float speed = 6;
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    float groundDist = .4f;
    float gravity = -9.81f;
    float targetAngle;

    public bool canMove = true;
    public bool playWithGamepad;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        cam = GameObject.Find("Player Camera").transform;
        groundCheck = GameObject.Find("groundCheck").transform;
        lightTr = transform.GetChild(1);
        camFreeLook = GameObject.Find("TPs").GetComponent<CinemachineFreeLook>();
    }

    void Update()
    {
        //input settings
        if (playWithGamepad)
        {
            HorizontalName = "HorizontalPad";
            VerticalName = "VerticalPad";
            ctrlName = "ctrlPad";
            camFreeLook.m_YAxis.m_InputAxisName = "MouseYPad";
            camFreeLook.m_XAxis.m_InputAxisName = "MouseXPad";
        }
        if (!playWithGamepad)
        {
            HorizontalName = "Horizontal";
            VerticalName = "Vertical";
            ctrlName = "ctrl";
            camFreeLook.m_YAxis.m_InputAxisName = "MouseY";
            camFreeLook.m_XAxis.m_InputAxisName = "MouseX";
        }

        //calculs
        float Horizontal = Input.GetAxisRaw(HorizontalName);
        float Vertical = Input.GetAxisRaw(VerticalName);
        bool isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);
        Vector3 direction = new Vector3(Horizontal, 0f, Vertical).normalized;

        //angle du joueur
        targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        lightTr.rotation = Quaternion.Euler(cam.eulerAngles.x, angle, 0f);

        //mouvements du joueur
        if (direction.magnitude >= 0.1f && canMove)
        {
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        //gravité
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if(isGrounded && velocity.y < 0f)
            velocity.y = -2f;
    }
}
