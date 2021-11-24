using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Brick : NetworkBehaviour
{
    [SerializeField] private int _HP = 1;
    public int HP
    {
        get { return _HP; }
        set
        {
            if (value != _HP)
            {
                _HP = value;
            }

            if (value <= 0)
            {
                //Brick hp is less then 1 call the fucntion to destroy
                CmdDestroy();
            }
        }
    }

    //Sends Network command to destroy the brick and update the total
    [Command(requiresAuthority = false)]
    void CmdDestroy()
    {
        GameObject.Find("Canvas").gameObject.GetComponent<UIManager>().Score += 100;
        NetworkServer.Destroy(this.gameObject);
    }
}
