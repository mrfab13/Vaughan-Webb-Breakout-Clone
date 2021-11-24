using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Brick : NetworkBehaviour
{
    private int _HP = 1;
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
                CmdDestroy();
            }
        }
    }

    [Command(requiresAuthority = false)]
    void CmdDestroy()
    {
        GameObject.Find("Canvas").gameObject.GetComponent<UIManager>().Score += 100;
        NetworkServer.Destroy(this.gameObject);
    }
}
