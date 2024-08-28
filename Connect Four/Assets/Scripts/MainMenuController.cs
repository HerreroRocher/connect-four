using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    // This method is called when the button is clicked
    public void OnPlayButtonClicked()
    {
        // Load the gameboard scene
        setPlayers();
        SceneManager.LoadScene("GameScene");
    }

    public void setPlayers()
    {
        GameData.players = new string[] { "Daniel", "Natasja" };
    }
}
