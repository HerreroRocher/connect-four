using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public string[] players;
    public string[] colours;
    public int rows;
    public int columns;
    public int inARowRequirements;

    public TMP_InputField player1NameIF;
    public TMP_InputField player2NameIF;
    public TMP_InputField rowsReq;
    public TMP_InputField columnsReq;
    public TMP_InputField connectReq;


    // This method is called when the button is clicked

    public void Start()
    {

        players = new string[] { "Daniel", "Natasja" }; ;
        colours = new string[] { "red", "yellow" };
        rows = 6;
        columns = 6;
        inARowRequirements = 4;
    }
    public void OnPlayButtonClicked()
    {
        players = new string[] { player1NameIF.text, player2NameIF.text }; 
        rows = int.Parse(rowsReq.text);
        columns = int.Parse(columnsReq.text);
        inARowRequirements = int.Parse(connectReq.text);

        // Load the gameboard scene
        setGameData();
        SceneManager.LoadScene("GameScene");
    }

    public void setGameData()
    {
        GameData.players = this.players;
        GameData.colours = this.colours;
        GameData.rows = this.rows;
        GameData.columns = this.columns;
        GameData.inARowRequirements = this.inARowRequirements;
    }
}
