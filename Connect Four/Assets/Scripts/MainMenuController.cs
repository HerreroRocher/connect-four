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
    public void OnPlayButtonClicked()
    {
        players = new string[] { (player1NameIF.text != "" ? player1NameIF.text : "Daniel"), (player2NameIF.text != "" ? player2NameIF.text : "Natasja") }; 
        rows = (int.TryParse(rowsReq.text, out rows) ? rows: 7 );
        columns = (int.TryParse(columnsReq.text, out columns) ? columns : 7 );
        inARowRequirements = (int.TryParse(connectReq.text, out inARowRequirements) ? inARowRequirements : 4 );
        colours = new string[] { "red", "yellow" };

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
