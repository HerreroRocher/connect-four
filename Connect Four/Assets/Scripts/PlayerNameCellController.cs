using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerNameCellController : MonoBehaviour
{
    private string playerName;
    private Color cellColor;
    public Image cellImage;
    public TextMeshProUGUI playerNameTMP;
    public FlexibleColorPicker colorPicker;
    public TMP_InputField playerNameInputField;
    public int player;
    private RectTransform textBoxRectTransform;

    public void Start()
    {
        textBoxRectTransform = playerNameTMP.GetComponent<RectTransform>();
    }
    private void setCellColor(Color color)
    {
        cellImage.color = color;
    }

    private void setName(string name)
    {
        playerNameTMP.text = name == "" ? "Player " + player : name;
        updateTextBoxSize();
    }

    private void setTextBoxWidth(float width){
        Vector2 dimensions = textBoxRectTransform.sizeDelta;
        dimensions.x = width;
        textBoxRectTransform.sizeDelta = dimensions;
    }

    private void updateTextBoxSize(){
        float preferredWidth = playerNameTMP.preferredWidth;
        setTextBoxWidth(preferredWidth + 50);
    }

    public void Update()
    {
        setCellColor(colorPicker.color);
        setName(playerNameInputField.text);
    }
}