using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    #region Potion
    
    public void ActivatePotion_Queen()
    {
        Game_Manager.instance.piece_reference.GetComponent<Chessman>().queen_potion = true;
        Game_Manager.instance.piece_reference.GetComponent<Chessman>().DestroyMovePlates();
        Game_Manager.instance.piece_reference.GetComponent<Chessman>().InitiateMovePlates();
        Destroy(gameObject);
    }
    
    public void ActivatePotion_Double()
    {
        Game_Manager.instance.piece_reference.GetComponent<Chessman>().duble_potion = true;
        Game_Manager.instance.piece_reference.GetComponent<Chessman>().DestroyMovePlates();
        Game_Manager.instance.piece_reference.GetComponent<Chessman>().InitiateMovePlates();
        Destroy(gameObject);
    }
    
    public void ActivatePotion_Die()
    {
        Game_Manager.instance.piece_reference.GetComponent<Chessman>().die_potion = true;
        Game_Manager.instance.piece_reference.GetComponent<Chessman>().DestroyMovePlates();
        Game_Manager.instance.piece_reference.GetComponent<Chessman>().InitiateMovePlates();
        Destroy(gameObject);
    }

    #endregion
}
