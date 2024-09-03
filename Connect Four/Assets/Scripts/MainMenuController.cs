using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public string[] players;
    public Color[] colors;
    public int rows;
    public int columns;
    public int inARowRequirements;

    public TMP_InputField player1NameIF;
    public TMP_InputField player2NameIF;
    public TMP_InputField rowsReq;
    public TMP_InputField columnsReq;
    public TMP_InputField connectReq;

    public FlexibleColorPicker colorPickerPlayer1;
    public FlexibleColorPicker colorPickerPlayer2;


    // This method is called when the button is clicked
    public void OnPlayButtonClicked()
    {
        players = new string[] { (player1NameIF.text != "" ? player1NameIF.text : "Daniel"), (player2NameIF.text != "" ? player2NameIF.text : "Natasja") };
        rows = rowsReq != null ? (int.TryParse(rowsReq.text, out rows) ? rows : 7) : 7;
        columns = columnsReq != null ? (int.TryParse(columnsReq.text, out columns) ? columns : 7) : 7;
        inARowRequirements = connectReq != null ? (int.TryParse(connectReq.text, out inARowRequirements) ? inARowRequirements : 4) : 4;
        colors = new Color[] { colorPickerPlayer1.color, colorPickerPlayer2.color };

        // Debug.Log(colorPickerPlayer1.color.GetType());
        // Debug.Log(colorPickerPlayer2.color.GetType());
        // Debug.Log("Player 1 color: " + colorPickerPlayer1.color);
        // Debug.Log("Player 2 color: " + colorPickerPlayer2.color);

        // Load the gameboard scene
        setGameData();
        SceneManager.LoadScene("GameScene");
    }

    public void setGameData()
    {
        GameData.players = this.players;
        GameData.colors = this.colors;
        GameData.rows = this.rows;
        GameData.columns = this.columns;
        GameData.inARowRequirements = this.inARowRequirements;
    }
}
