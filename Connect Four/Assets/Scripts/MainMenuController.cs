using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public TMP_InputField Player1NameInputField;
    public TMP_InputField Player2NameInputField;
    public TMP_InputField RowsRequirementInputField;
    public TMP_InputField ColumnsRequirementInputField;
    public TMP_InputField InARowRequirementInputField;
    public FlexibleColorPicker Player1ColorPicker;
    public FlexibleColorPicker Player2ColorPicker;

    
    private string[] players;
    private Color[] colors;
    private int rows;
    private int columns;
    private int inARowRequirements;

    public void OnPlayButtonClicked()
    {
        players = new string[] { (Player1NameInputField.text != "" ? Player1NameInputField.text : "Daniel"), (Player2NameInputField.text != "" ? Player2NameInputField.text : "Natasja") };
        rows = RowsRequirementInputField != null ? (int.TryParse(RowsRequirementInputField.text, out rows) ? rows : 7) : 7;
        columns = ColumnsRequirementInputField != null ? (int.TryParse(ColumnsRequirementInputField.text, out columns) ? columns : 7) : 7;
        inARowRequirements = InARowRequirementInputField != null ? (int.TryParse(InARowRequirementInputField.text, out inARowRequirements) ? inARowRequirements : 4) : 4;
        colors = new Color[] { Player1ColorPicker.color, Player2ColorPicker.color };

        // Debug.Log(colorPickerPlayer1.color.GetType());
        // Debug.Log(colorPickerPlayer2.color.GetType());
        // Debug.Log("Player 1 color: " + colorPickerPlayer1.color);
        // Debug.Log("Player 2 color: " + colorPickerPlayer2.color);

        // Load the gameboard scene
        setGameData();
        SceneManager.LoadScene("GameScene");
    }

    private void setGameData()
    {
        GameData.Players = this.players;
        GameData.Colors = this.colors;
        GameData.Rows = this.rows;
        GameData.Columns = this.columns;
        GameData.InARowRequirements = this.inARowRequirements;
    }
}
