using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;
    
    public GameObject chesspiece;

    [Header("Pieces Arrays")]
    //  Posicions i equip per cada fitxa (Array)
    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerRed = new GameObject[16];
    
    [Header("Game Variables")]
    public bool gameOver = false;
    private float speed;

    public GameObject piece_reference;
    
    [SerializeField] private GameObject gameover_panel;
    [SerializeField] private TMP_Text red_gameover_txt;
    [SerializeField] private TMP_Text blue_gameover_txt;

    [Header("Player Variables")]
    public string currentPlayer = "red";

    public TMP_Text winnerText;
    public TMP_Text player_2;
    public TMP_Text player_1;

    [SerializeField] private GameObject player_red_selected;
    [SerializeField] private GameObject player_blue_selected;

    [Header("Shop Variables")]
    private int coins_player_1;
    private int coins_player_2;
    private int cost_potions;
    [SerializeField] private TMP_Text coins_player_1_txt;
    [SerializeField] private TMP_Text coins_player_2_txt;
    [SerializeField] private TMP_Text cost_txt;

    [Header("Potions")]
    private GameObject grid;
    [SerializeField] private GameObject red_grid;
    [SerializeField] private GameObject blue_grid;

    private GameObject potion;
    [SerializeField] private GameObject queen_potion;
    [SerializeField] private GameObject double_potion;
    [SerializeField] private GameObject die_potion;
    
    [SerializeField] private TMP_Text queen_potion_txt;
    [SerializeField] private TMP_Text double_potion_txt;
    [SerializeField] private TMP_Text die_potion_txt;


    public void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player_1.color = Color.white;
        player_2.color = Color.grey;
        player_red_selected.SetActive(true);

        playerRed = new GameObject[]
        {
            Create("red_rook", 0, 0), Create("red_knight", 0, 1), Create("red_bishop", 0, 2),
            Create("red_king", 0, 3), Create("red_queen", 0, 4), Create("red_bishop", 0, 5),
            Create("red_knight", 0, 6), Create("red_rook", 0, 7), Create("red_pawn", 1, 0),
            Create("red_pawn", 1, 1), Create("red_pawn", 1, 2), Create("red_pawn", 1, 3),
            Create("red_pawn", 1, 4), Create("red_pawn", 1, 5), Create("red_pawn", 1, 6),
            Create("red_pawn", 1, 7),
        };
        
        playerBlack = new GameObject[]
        {
            Create("blue_rook", 7, 0), Create("blue_knight", 7, 1), Create("blue_bishop", 7, 2),
            Create("blue_king", 7, 3), Create("blue_queen", 7, 4), Create("blue_bishop", 7, 5),
            Create("blue_knight", 7, 6), Create("blue_rook", 7, 7), Create("blue_pawn", 6, 0),
            Create("blue_pawn", 6, 1), Create("blue_pawn", 6, 2), Create("blue_pawn", 6, 3),
            Create("blue_pawn", 6, 4), Create("blue_pawn", 6, 5), Create("blue_pawn", 6, 6),
            Create("blue_pawn", 6, 7),
        };

        //  Col·locar les peces en el tauler de posicions
        for (int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerRed[i]);
        }
    }

    public void Update()
    {
        if (gameOver)
        {
            winnerText.gameObject.SetActive(true);
            winnerText.text = $"Winner is {currentPlayer}";
            
            if (Input.GetMouseButtonDown(0))
            {
                gameOver = false;
                SceneManager.LoadScene("Main_Scene");
            }
        }
        
    }

    #region Pieces

    public GameObject Create(string name, int x, int y)
    {
        // Fer instancia del Game Object
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
        // Entrar dins del component del script
        Chessman cm = obj.GetComponent<Chessman>();
        // Agafar noms i coordenades
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        // Agafa el nom i posa el sprite correcte
        cm.Activate();
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();

        // Accedir a les posicions i afegir-les al array
        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;

    }

    /// <summary>
    /// Per moure la fitxa primer necessites buidar la posició en la qual està
    /// Simplement, agafes els seus paràmetres x i y i els tornes null
    /// </summary>
    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
        
    }

    /// <summary>
    /// Que et torni les posicions x i y del Array
    /// </summary>
    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }
    
    /// <summary>
    /// Comprobar que les posicions de la fitxa siguin al taulell
    /// </summary>
    public bool PositionOnBoardBool(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    #endregion

    #region Game

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public void NextTurn()
    {
        if (currentPlayer == "red")
        {
            currentPlayer = "blue";
            player_1.color = Color.grey;
            player_2.color = Color.white;
            player_red_selected.SetActive(false);
            player_blue_selected.SetActive(true);
        }
        else
        {
            currentPlayer = "red";
            player_1.color = Color.white;
            player_2.color = Color.grey;
            player_red_selected.SetActive(true);
            player_blue_selected.SetActive(false);
        }
    }

    public void EndGame()
    {
        if (currentPlayer == "red")
        {
            red_gameover_txt.gameObject.SetActive(true);
        }
        else
        {
            blue_gameover_txt.gameObject.SetActive(true);
        }
        gameover_panel.SetActive(true);
    }


    #endregion

    #region Coins

    public void AddCoins(int num_toadd)
    {
        if (currentPlayer == "red")
        {
            coins_player_1 += num_toadd;
        }
        else
        {
            coins_player_2 += num_toadd;
        }
    }
    
    public void Coins()
    {
        coins_player_1_txt.text = $"{coins_player_1}";
        coins_player_2_txt.text = $"{coins_player_2}";
    }

    #endregion

    #region Potions

    public void AddPotionGrid()
    {
        if (currentPlayer == "red")
        {
            if (coins_player_1 >= cost_potions)
            {
                grid = red_grid;
                coins_player_1 -= cost_potions;
                Instantiate(potion, grid.transform);
            }
        }
        else
        {
            if (coins_player_2 >= cost_potions)
            {
                grid = blue_grid;
                coins_player_2 -= cost_potions;
                Instantiate(potion, grid.transform);
            }
        }

        Coins();
    }
    
    public void BuyPotion_Queen()
    {
        potion = queen_potion;
        cost_potions = 5;
        queen_potion_txt.gameObject.SetActive(true);
        double_potion_txt.gameObject.SetActive(false);
        die_potion_txt.gameObject.SetActive(false);
        RefreshCost_Potion();
    }

    public void BuyPotion_Double()
    {
        potion = double_potion;
        cost_potions = 2;
        queen_potion_txt.gameObject.SetActive(false);
        double_potion_txt.gameObject.SetActive(true);
        die_potion_txt.gameObject.SetActive(false);
        RefreshCost_Potion();
    }
    
    public void BuyPotion_Die()
    {
        potion = die_potion;
        cost_potions = 4;
        queen_potion_txt.gameObject.SetActive(false);
        double_potion_txt.gameObject.SetActive(false);
        die_potion_txt.gameObject.SetActive(true);
        RefreshCost_Potion();
    }

    private void RefreshCost_Potion()
    {
        cost_txt.text = $"{cost_potions}";
    }

    #endregion
}
