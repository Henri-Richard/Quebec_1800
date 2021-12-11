using Cinemachine;
using UnityEngine;

public class toolChanging : MonoBehaviour
{
    Controller playerController;
    Animator animator;
    CinemachineFreeLook cam;
    GameObject Light;

    bool isIn = false;

    void Start()
    {
        animator = GameObject.Find("Sélection d'outils").GetComponent<Animator>();
        playerController = GetComponent<Controller>();
        cam = GameObject.Find("TPs").GetComponent<CinemachineFreeLook>();
    }

    void Update()
    {        
        if(Input.GetButtonDown(playerController.ctrlName))
            isIn = true;
        if (Input.GetButtonUp(playerController.ctrlName))
            isIn = false;
        animator.SetBool("in", isIn);

        if (isIn)
        {
            cam.m_XAxis.m_MaxSpeed = 0;
            cam.m_YAxis.m_MaxSpeed = 0;
            playerController.canMove = false;
            if(!playerController.playWithGamepad)
                Cursor.visible = true;
        }
        else
        {
            cam.m_XAxis.m_MaxSpeed = 100;
            cam.m_YAxis.m_MaxSpeed = 1;
            playerController.canMove = true;
            if (Cursor.visible)
                Cursor.visible = false;
        }
    }

    public void ChangeTool(int toolNb)
    {
        Light = transform.GetChild(1).gameObject;
        if (toolNb == 0)
            Debug.Log("open");
        if (toolNb == 1)
            Debug.Log("close");
    }
}
