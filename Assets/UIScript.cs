using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Font uiLabelFont;

    public Image manaBarImage;
    public Image dragonHealthBarImage;
    public Image dragonHealthBarImageOuter;

    private Texture2D heartTexture;
    private readonly float heartDimensions = 50f; // width & height
    private readonly float upperLeftY = 34f;
    private readonly float upperLeftX = 50f;
    private readonly float distBetweenHearts = 65f;

    private float initialFontSize = 20f;

    public static bool showDragonHealthBar = false;

    public void Start()
    {
         heartTexture = Resources.Load<Texture2D>("heart");
    }

    private void Update()
    {
        
    }

    private void DisplayUILabelText(string text, Color textColor) 
    {
        GUIStyle style = new GUIStyle();
        style.normal.background = null;
        style.normal.textColor = textColor;
        style.font = uiLabelFont;
        style.fontSize = 100;

        GUI.Label(new Rect(500, Screen.height / 2, 500, 500), text, style);
    }

    private void UpdateHitpointsDisplay()
    {
        int numOfHearts = PlayerScript.currentHp;

        for (int i = 0; i < numOfHearts; i++)
        {
            Rect position = new Rect(
                upperLeftX + i * distBetweenHearts, 
                upperLeftY, 
                heartDimensions, 
                heartDimensions);
            GUI.DrawTexture(position, heartTexture);
        }
    }

    private void UpdateManaBar() 
    {
        manaBarImage.fillAmount = PlayerScript.mana / PlayerScript.MAX_MANA;
    }

    private void UpdateDragonHealthBar()
    {
        dragonHealthBarImage.fillAmount = (float)DragonScript.hitsToDie / DragonScript.MAX_HP;
    }

    void OnGUI()
    {
        UpdateHitpointsDisplay();
        UpdateManaBar();
        UpdateDragonHealthBar();

        if (PlayerScript.gameOver && !PlayerScript.gameWon)
        {
            DisplayUILabelText("GAME OVER !!!", new Color(1, 0, 0));
        }
        else if (PlayerScript.gameOver && PlayerScript.gameWon) 
        {
            DisplayUILabelText("GAME WON !!!", new Color(0, 1, 0));
        }
    }


}
