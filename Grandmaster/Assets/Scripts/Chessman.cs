using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    public GameObject movePlate;

    public Sprite blue_queen, blue_king, blue_bishop, blue_knight, blue_rook, blue_pawn;
    public Sprite red_queen, red_king, red_bishop, red_knight, red_rook, red_pawn;
    
    public RuntimeAnimatorController pawn_red_controller, rook_red_controller, bishop_red_controller, king_red_controller, queen_red_controller, knight_red_controller;
    public RuntimeAnimatorController pawn_blue_controller, rook_blue_controller, bishop_blue_controller, king_blue_controller, queen_blue_controller, knight_blue_controller;

    private bool potion_doublemovement;
    
    private int xBoard = 0;
    private int yBoard = 0;

    private string player;
    
    private bool turn_1 = true;

    [Header("Potions Variables")] 
    public bool queen_potion;
    public bool duble_potion;
    public bool die_potion;

    public void Activate()
    {
        SetCoords();
        
        // Canviar el sprite depenen del seu nom
        switch (this.name)
        {
            case "blue_queen": this.GetComponent<SpriteRenderer>().sprite = blue_queen; player = "blue"; 
                gameObject.GetComponent<Animator>().runtimeAnimatorController = queen_blue_controller;
                break;
            case "blue_king": this.GetComponent<SpriteRenderer>().sprite = blue_king; player = "blue";
                gameObject.GetComponent<Animator>().runtimeAnimatorController = king_blue_controller;
                break;
            case "blue_bishop": this.GetComponent<SpriteRenderer>().sprite = blue_bishop; player = "blue"; 
                gameObject.GetComponent<Animator>().runtimeAnimatorController = bishop_blue_controller;
                break;
            case "blue_knight": this.GetComponent<SpriteRenderer>().sprite = blue_knight; player = "blue"; 
                gameObject.GetComponent<Animator>().runtimeAnimatorController = knight_blue_controller;
                break;
            case "blue_rook": this.GetComponent<SpriteRenderer>().sprite = blue_rook; player = "blue"; 
                gameObject.GetComponent<Animator>().runtimeAnimatorController = rook_blue_controller;
                break;
            case "blue_pawn": this.GetComponent<SpriteRenderer>().sprite = blue_pawn; player = "blue"; 
                gameObject.GetComponent<Animator>().runtimeAnimatorController = pawn_blue_controller;
                break;

            case "red_queen": this.GetComponent<SpriteRenderer>().sprite = red_queen; player = "red"; 
                gameObject.GetComponent<Animator>().runtimeAnimatorController = queen_red_controller;
                break;
            case "red_king": this.GetComponent<SpriteRenderer>().sprite = red_king; player = "red"; 
                gameObject.GetComponent<Animator>().runtimeAnimatorController = king_red_controller;
                break;
            case "red_bishop": this.GetComponent<SpriteRenderer>().sprite = red_bishop; player = "red"; 
                gameObject.GetComponent<Animator>().runtimeAnimatorController = bishop_red_controller;
                break;
            case "red_knight": this.GetComponent<SpriteRenderer>().sprite = red_knight; player = "red"; 
                gameObject.GetComponent<Animator>().runtimeAnimatorController = knight_red_controller;
                break;
            case "red_rook": this.GetComponent<SpriteRenderer>().sprite = red_rook; player = "red"; 
                gameObject.GetComponent<Animator>().runtimeAnimatorController = rook_red_controller;
                break;
            case "red_pawn": this.GetComponent<SpriteRenderer>().sprite = red_pawn; player = "red";
                gameObject.GetComponent<Animator>().runtimeAnimatorController = pawn_red_controller;
                break;
        }
    }

    #region Bool

    public bool Turn1
    {
        get => turn_1;
        set => turn_1 = value;
    }
    
    public string Player
    {
        get => player;
        set => player = value;
    }
    
    #endregion

    #region Position

    /// <summary>
    /// Mesures del taulell i posició
    /// </summary>
    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        x *= 0.91f;
        y *= 0.91f;

        x += -3.185f;
        y += -3.75f;

        this.transform.position = new Vector3(x, y, 0f);
    }

    /// <summary>
    /// Aconsegueix la posició x i y
    /// </summary>
    /// <returns></returns>
    public int GetXBoard()
    {
        return xBoard;
    }
    
    public int GetYBoard()
    {
        return yBoard;
    }

    /// <summary>
    /// Sobreescriu la posició x i y
    /// </summary>
    /// <returns></returns>
    public void SetXBoard(int x)
    {
        xBoard = x;
    }
    
    public void SetYBoard(int y)
    {
        yBoard = y;
    }

    #endregion
    
    /// <summary>
    /// Quan cliques a la peça amb el ratoli
    /// </summary>
    private void OnMouseUp()
    {
        if (!Game_Manager.instance.gameOver && Game_Manager.instance.GetCurrentPlayer() == player)
        {
            DestroyMovePlates();
            InitiateMovePlates();
        }

        Game_Manager.instance.piece_reference = gameObject;
    }

    #region Move Plates

        public void DestroyMovePlates()
    {
        // Cear un Array amb tots els move plates que existeixen
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        
        for (int i = 0; i < movePlates.Length; i++)
        {
            // Destruir tots els components del Array
            Destroy(movePlates[i]);
        }
    }

    public void InitiateMovePlates()
    {
        // Depenen del nom de la peça la funció canvia
        switch (this.name)
        {
            // Cada fitxa te els seus moviments per tant fem les funcions generals com moures en linea, de un en un o en el
            // i les apliques diferents cada cas
            case "blue_queen":
            case "red_queen":
                // Cridem a la funció
                // Els parametres son el numero de "cuadrats" dins el taulell es poden moure o la direcció
                if (die_potion)
                {
                    PawnMovePlate(xBoard +1, yBoard);
                    PawnMovePlate(xBoard -1, yBoard);
                    PawnMovePlate(xBoard, yBoard +1);
                    PawnMovePlate(xBoard, yBoard -1);
                    LineMovePlate(1, 0);
                    LineMovePlate(0, 1);
                    LineMovePlate(1, 1);
                    LineMovePlate(-1, 0);
                }
                else
                {
                    LineMovePlate(1, 0);
                    LineMovePlate(0, 1);
                    LineMovePlate(1, 1);
                    LineMovePlate(-1, 0);
                    LineMovePlate(0, -1);
                    LineMovePlate(-1, -1);
                    LineMovePlate(-1, 1);
                    LineMovePlate(1, -1);
                }
                break;
            case "blue_king":
            case "red_king":
                if (queen_potion)
                {
                    LineMovePlate(1, 0);
                    LineMovePlate(0, 1);
                    LineMovePlate(1, 1);
                    LineMovePlate(-1, 0);
                    LineMovePlate(0, -1);
                    LineMovePlate(-1, -1);
                    LineMovePlate(-1, 1);
                    LineMovePlate(1, -1);
                }
                else if (die_potion)
                {
                    PawnMovePlate(xBoard +1, yBoard);
                    PawnMovePlate(xBoard -1, yBoard);
                    PawnMovePlate(xBoard, yBoard +1);
                    PawnMovePlate(xBoard, yBoard -1);
                    LineMovePlate(1, 0);
                    LineMovePlate(0, 1);
                    LineMovePlate(1, 1);
                    LineMovePlate(-1, 0);
                }
                else
                {
                    SorroundMovePlate();
                }
                break;
            case "blue_bishop":
            case "red_bishop":
                if (queen_potion)
                {
                    LineMovePlate(1, 0);
                    LineMovePlate(0, 1);
                    LineMovePlate(1, 1);
                    LineMovePlate(-1, 0);
                    LineMovePlate(0, -1);
                    LineMovePlate(-1, -1);
                    LineMovePlate(-1, 1);
                    LineMovePlate(1, -1);
                }
                else if (die_potion)
                {
                    PawnMovePlate(xBoard +1, yBoard);
                    PawnMovePlate(xBoard -1, yBoard);
                    PawnMovePlate(xBoard, yBoard +1);
                    PawnMovePlate(xBoard, yBoard -1);
                    LineMovePlate(1, 0);
                    LineMovePlate(0, 1);
                    LineMovePlate(1, 1);
                    LineMovePlate(-1, 0);
                }
                else
                {
                    LineMovePlate(1, 1);
                    LineMovePlate(1, -1);
                    LineMovePlate(-1, 1);
                    LineMovePlate(-1, -1);
                }
                break;
            case "blue_knight":
            case "red_knight":
                if (queen_potion)
                {
                    LineMovePlate(1, 0);
                    LineMovePlate(0, 1);
                    LineMovePlate(1, 1);
                    LineMovePlate(-1, 0);
                    LineMovePlate(0, -1);
                    LineMovePlate(-1, -1);
                    LineMovePlate(-1, 1);
                    LineMovePlate(1, -1);
                }
                else if (die_potion)
                {
                    PawnMovePlate(xBoard +1, yBoard);
                    PawnMovePlate(xBoard -1, yBoard);
                    PawnMovePlate(xBoard, yBoard +1);
                    PawnMovePlate(xBoard, yBoard -1);
                    LineMovePlate(1, 0);
                    LineMovePlate(0, 1);
                    LineMovePlate(1, 1);
                    LineMovePlate(-1, 0);
                }
                else
                {
                    LMovePlate();
                }
                break;
            case "blue_rook":
            case "red_rook":
                if (queen_potion)
                {
                    LineMovePlate(1, 0);
                    LineMovePlate(0, 1);
                    LineMovePlate(1, 1);
                    LineMovePlate(-1, 0);
                    LineMovePlate(0, -1);
                    LineMovePlate(-1, -1);
                    LineMovePlate(-1, 1);
                    LineMovePlate(1, -1);
                }
                else if (die_potion)
                {
                    PawnMovePlate(xBoard +1, yBoard);
                    PawnMovePlate(xBoard -1, yBoard);
                    PawnMovePlate(xBoard, yBoard +1);
                    PawnMovePlate(xBoard, yBoard -1);
                    LineMovePlate(1, 0);
                    LineMovePlate(0, 1);
                    LineMovePlate(1, 1);
                    LineMovePlate(-1, 0);
                }
                else
                {
                    LineMovePlate(1, 0);
                    LineMovePlate(0, 1);
                    LineMovePlate(-1, 0);
                    LineMovePlate(0, -1);
                }
                break;
            // Els peons tenen moviments diferents ja que només avançen en davant i estàn en costats contraris del taulell
            case "blue_pawn":
                if (queen_potion)
                {
                    LineMovePlate(1, 0);
                    LineMovePlate(0, 1);
                    LineMovePlate(1, 1);
                    LineMovePlate(-1, 0);
                    LineMovePlate(0, -1);
                    LineMovePlate(-1, -1);
                    LineMovePlate(-1, 1);
                    LineMovePlate(1, -1);
                }
                else if (die_potion)
                {
                    PawnMovePlate(xBoard +1, yBoard);
                    PawnMovePlate(xBoard -1, yBoard);
                    PawnMovePlate(xBoard, yBoard +1);
                    PawnMovePlate(xBoard, yBoard -1);
                    LineMovePlate(1, 0);
                    LineMovePlate(0, 1);
                    LineMovePlate(1, 1);
                    LineMovePlate(-1, 0);
                }
                else if (turn_1)
                {
                    PawnMovePlate(xBoard -2, yBoard);
                    PawnMovePlate(xBoard -1, yBoard);
                }
                else
                {
                    PawnMovePlate(xBoard -1, yBoard);
                }                
                break;
            case "red_pawn":
                if (queen_potion)
                {
                    LineMovePlate(1, 0);
                    LineMovePlate(0, 1);
                    LineMovePlate(1, 1);
                    LineMovePlate(-1, 0);
                    LineMovePlate(0, -1);
                    LineMovePlate(-1, -1);
                    LineMovePlate(-1, 1);
                    LineMovePlate(1, -1);
                }
                else if (die_potion)
                {
                    PawnMovePlate(xBoard +1, yBoard);
                    PawnMovePlate(xBoard -1, yBoard);
                    PawnMovePlate(xBoard, yBoard +1);
                    PawnMovePlate(xBoard, yBoard -1);
                    LineMovePlate(1, 0);
                    LineMovePlate(0, 1);
                    LineMovePlate(1, 1);
                    LineMovePlate(-1, 0);
                }
                else if (turn_1)
                {
                    PawnMovePlate(xBoard +2, yBoard);
                    PawnMovePlate(xBoard +1, yBoard);
                }
                else
                {
                    PawnMovePlate(xBoard +1, yBoard);
                }                
                break;
        }
    }

    /// <summary>
    /// Moviment en linees
    /// </summary>
    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        // Mirem que sigui dins el taulell i que no hi hagui res en aquella posició
        while (Game_Manager.instance.PositionOnBoardBool(x, y) && Game_Manager.instance.GetPosition(x, y) == null)
        {
            // Reculls la posició de la peça i sumes per posició no ocupada un move plate
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }

        // Comprobar si es un atac
        // Comproves si hi ha alguna altre fitxa en les posicions en les que et pots moure i si aquestes no son del mateix colors que sigui un atac
        if (Game_Manager.instance.PositionOnBoardBool(x, y) && Game_Manager.instance.GetPosition(x, y).GetComponent<Chessman>().player != player)
        {
            // Detectar si es el rei
            if (Game_Manager.instance.GetPosition(x, y).GetComponent<Chessman>().name == "blue_king" || Game_Manager.instance.GetPosition(x, y).GetComponent<Chessman>().name ==  "red_king")
            {
                // Funció Checkmate
            }
            MovePlateAttackSpawn(x, y);
        }
    }

    /// <summary>
    /// Moviment en L del caball
    /// Et diu a quines coordenades del taulell en vers a la teva fitxa espot moure
    /// (Li suma les coordenades a la posició de la fitxa per crear els move plates de on es pot moure)
    /// </summary>
    public void LMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
    }

    /// <summary>
    /// Moviment singular del Rei
    /// Et diu a quines coordenades del taulell en vers a la teva fitxa espot moure
    /// (Li suma les coordenades a la posició de la fitxa per crear els move plates de on es pot moure)
    /// </summary>
    public void SorroundMovePlate()
    {
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard);
        PointMovePlate(xBoard - 1, yBoard);
        PointMovePlate(xBoard + 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard + 1);
    }

    /// <summary>
    /// Crear move plates (single) en el taulell
    /// </summary>
    public void PointMovePlate(int x, int y)
    {
        // Comprovem que la fitxa és en el taulell
        if (Game_Manager.instance.PositionOnBoardBool(x, y))
        {
            // Conseguim la posició de la fitxa
            GameObject cp = Game_Manager.instance.GetPosition(x, y);

            // Si la posició és buida és crea un move plate
            if (cp == null)
            {
                if (!die_potion)
                {
                    MovePlateSpawn(x, y);
                }
            }
            // Si hi ha una fitxa que no creei un move plate a no ser que sigui diferent al player de la fitxa que per tant és un atac
            else if (cp.GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    /// <summary>
    /// Moviment del pao
    /// Et diu a quines coordenades del taulell en vers a la teva fitxa espot moure
    /// (Li suma les coordenades a la posició de la fitxa per crear els move plates de on es pot moure)
    /// </summary>
    public void PawnMovePlate(int x, int y)
    {
        if (Game_Manager.instance.PositionOnBoardBool(x, y))
        {
            if (Game_Manager.instance.GetPosition(x, y) == null)
            {
                if (!die_potion)
                {
                    MovePlateSpawn(x, y);
                }
            }

            GameObject cp = Game_Manager.instance.GetPosition(x, y);
            
            // Comprovem si hi ha una fitxa, agafem les dades i mirem si son de les nostres
            if (Game_Manager.instance.PositionOnBoardBool(x, y +1) && Game_Manager.instance.GetPosition(x, y +1) != null && Game_Manager.instance.GetPosition(x, y +1).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x, y +1);
            }
            
            if (Game_Manager.instance.PositionOnBoardBool(x, y -1) && Game_Manager.instance.GetPosition(x, y -1) != null && Game_Manager.instance.GetPosition(x, y -1).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x, y -1);
            }
        }
    }

    /// <summary>
    /// Afaga les coordenades i crea un move plate
    /// </summary>
    public void MovePlateSpawn(int xWorld, int yWorld)
    {
        float x = xWorld;
        float y = yWorld;

        // Passem les dimensións del taulell
        x *= 0.91f;
        y *= 0.91f;

        x += -3.185f;
        y += -3.75f;

        // Per ensenyar en pantalla el prefab de move plate
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, 0f), Quaternion.identity);

        // Instanciem el script de move plate
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        // Per guardar les dades
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(xWorld, yWorld);
    }
    
    public void MovePlateAttackSpawn(int xWorld, int yWorld)
    {
        float x = xWorld;
        float y = yWorld;

        x *= 0.91f;
        y *= 0.91f;

        x += -3.185f;
        y += -3.75f;

        // Per ensenyar en pantalla
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, 0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        // Per guardar les dades
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(xWorld, yWorld);
    }

    #endregion

    #region Potions

    public void Activate_Potion(string potion_name)
    {
        potion_doublemovement = true;
    }

    #endregion

    public void IdleTrigger()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Idle");
    }
}
