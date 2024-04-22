using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    /// <summary>
    /// Quan cliquis en qualsevol peça es crearà un move plate i aquest necessita una referència de per quina peça s'ha creat
    /// </summary>
    private GameObject reference = null;

    public Sprite attack_sprite;

    /// <summary>
    /// Posicions del move plate dins el taulell
    /// </summary>
    private int mp_xBoard;
    private int mp_yBoard;

    private int delay = 2;
    private float speed_movement = 1f;
    private const float minDistance = 0.2f;

    private float speed;
    private bool move;

    private float time = 5;

    /// <summary>
    /// false: moviment
    /// true: atac
    /// </summary>
    public bool attack = false;

    private Vector3 old_position;
    private Vector3 target;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip walk_sound;
    [SerializeField] private AudioClip attack_sound;
    [SerializeField] private AudioClip win_sound;

    public void Start()
    {
        if (attack)
        {
            // Canviar el color a vermell per marcar que és un atac
            gameObject.GetComponent<SpriteRenderer>().sprite = attack_sprite;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);  
        }
        speed = 1;
    }

    private void Update()
    {
        if (move)
        {
            reference.transform.position = Vector3.MoveTowards(reference.transform.position, gameObject.transform.position, speed*Time.deltaTime);
        }

        if (gameObject.transform.position == reference.transform.position)
        {
            move = false;
            reference.GetComponent<Chessman>().DestroyMovePlates();
            _audioSource.Stop();
            PieceMovement();
        }
    }

    public void OnMouseUp()
    {
        reference.GetComponent<Animator>().SetTrigger("Walk");
        _audioSource.PlayOneShot(walk_sound);
        
        Debug.Log("reference" + reference.transform.position);
        Debug.Log("move plate" + gameObject.transform.position);
        move = true;
    }

    private void PieceMovement()
    {
        // Accedim al SetPositionEmpty per buidar la posicio x i y de la referencia que hem clicat
        Game_Manager.instance.SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(), reference.GetComponent<Chessman>().GetYBoard());
        GameObject cp = Game_Manager.instance.GetPosition(mp_xBoard, mp_yBoard);
                
        if (!move)
        {
            if (attack)
            {
                // Passem la posició del gameobject del qual sigui el get position a la x i y del move plate
                cp.GetComponent<Animator>().SetTrigger("Die");
                _audioSource.PlayOneShot(attack_sound);
                reference.GetComponent<Animator>().SetTrigger("Attack");
                Debug.Log("attack");
                
                if (cp.name == "blue_king" || cp.name == "red_king")
                {
                    Game_Manager.instance.gameOver = true;
                    _audioSource.PlayOneShot(win_sound);
                    Game_Manager.instance.EndGame();
                }
            
                if (cp.name == "blue_queen" || cp.name == "red_queen")
                {
                    Game_Manager.instance.AddCoins(5);
                }
                if (cp.name == "blue_bishop" || cp.name == "red_bishop" || cp.name == "black_rook" || cp.name == "red_rook")
                {
                    Game_Manager.instance.AddCoins(3);
                }
                if (cp.name == "blue_knight" || cp.name == "red_knight")
                {
                   Game_Manager.instance.AddCoins(2);
                }
                if (cp.name == "blue_pawn" || cp.name == "red_pawn")
                { 
                    Game_Manager.instance.AddCoins(1);
                }
                
                Destroy(cp, cp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
            }
            
            reference.GetComponent<Animator>().SetTrigger("Idle");

            // Guardem la nova posició de la peça, ho fem comparant la posicio del move plate com a nova posició de la peça que estem movent
            reference.GetComponent<Chessman>().SetXBoard(mp_xBoard);
            reference.GetComponent<Chessman>().SetYBoard(mp_yBoard);
            reference.GetComponent<Chessman>().SetCoords();

            // També les guardem el el game manager
            Game_Manager.instance.SetPosition(reference);
            
            Game_Manager.instance.Coins();
            
            if (!reference.GetComponent<Chessman>().duble_potion)
            {
                Game_Manager.instance.NextTurn();
            }

            reference.GetComponent<Chessman>().Turn1 = false;
            
            reference.GetComponent<Chessman>().queen_potion = false;
            reference.GetComponent<Chessman>().duble_potion = false;
            reference.GetComponent<Chessman>().die_potion = false;
        }
    }

    public void SetCoords(int x, int y)
    {
        mp_xBoard = x;
        mp_yBoard = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }
    
    public GameObject GetReference()
    {
        return reference;
    }
}
