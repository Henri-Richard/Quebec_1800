using Mirror;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField] Behaviour[] componentsToDisable;

    Camera sceneCam;

    void Start()
    {
        //d�sactiver le "Controller" de l'autre joueur
        if(!isLocalPlayer)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        else
        {
            //syst�me de cam�ra dynamique
            sceneCam = Camera.main;
            if(sceneCam != null)
            {
                sceneCam.gameObject.SetActive(false);
            }
        }
    }

    void OnDisable()
    {
        if(sceneCam != null)
        {
            sceneCam.gameObject.SetActive(true);
        }
    }
}
