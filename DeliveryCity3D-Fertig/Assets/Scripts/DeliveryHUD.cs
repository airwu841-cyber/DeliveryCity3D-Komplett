using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// Person 2: Texte anzeigen und Knöpfe verbinden. Die Regeln stehen in DeliveryGame.
public class DeliveryHUD : MonoBehaviour
{
    [Header("Spiel und Bildschirme")]
    public DeliveryGame game;
    public GameObject mainMenuPanel, hudPanel, pausePanel, resultPanel;

    [Header("Anzeigen")]
    public Text cityText, deliveriesText, timeText, messageText;
    public Text resultTitleText, resultDetailsText, stepText;
    public Text[] orderTexts = new Text[3];
    public Text[] truckTexts = new Text[2];
    public Image deliveryProgress;

    [Header("Hauptmenü")]
    public Button startButton, city1Button, city2Button, quitButton;
    public Button fullscreenButton, pauseFullscreenButton;

    [Header("Spiel und Pause")]
    public Button pauseButton, resumeButton, restartButton, pauseMenuButton;
    public Button[] orderButtons = new Button[3];

    [Header("Ergebnis")]
    public Button retryButton, resultMenuButton, nextCityButton;

    void Start()
    {
        // AddListener bedeutet: Beim Klicken wird diese Methode aufgerufen.
        startButton.onClick.AddListener(game.StartGame);
        city1Button.onClick.AddListener(OpenFirstCity);
        city2Button.onClick.AddListener(OpenSecondCity);
        quitButton.onClick.AddListener(Quit);
        pauseButton.onClick.AddListener(game.TogglePause);
        resumeButton.onClick.AddListener(game.TogglePause);
        restartButton.onClick.AddListener(game.Restart);
        pauseMenuButton.onClick.AddListener(game.ReturnToMenu);
        retryButton.onClick.AddListener(game.Restart);
        resultMenuButton.onClick.AddListener(game.ReturnToMenu);
        nextCityButton.onClick.AddListener(OpenSecondCity);
        fullscreenButton.onClick.AddListener(ToggleFullscreen);
        pauseFullscreenButton.onClick.AddListener(ToggleFullscreen);
        orderButtons[0].onClick.AddListener(ChooseFirstOrder);
        orderButtons[1].onClick.AddListener(ChooseSecondOrder);
        orderButtons[2].onClick.AddListener(ChooseThirdOrder);
        Refresh();
    }

    void Update()
    {
        // Auch in der Pause können wir Knöpfe bedienen und Texte aktualisieren.
        if (Keyboard.current != null && Keyboard.current.f11Key.wasPressedThisFrame)
            ToggleFullscreen();
        Refresh();
    }

    void Refresh()
    {
        mainMenuPanel.SetActive(!game.Running && !game.Finished);
        hudPanel.SetActive(game.Running);
        pausePanel.SetActive(game.Running && game.Paused);
        resultPanel.SetActive(game.Finished);

        cityText.text = game.cityName;
        deliveriesText.text = game.Delivered + " / " + game.deliveryGoal + " Pakete";
        deliveryProgress.fillAmount = (float)game.Delivered / game.deliveryGoal;
        messageText.text = game.Message;

        int seconds = Mathf.CeilToInt(game.TimeLeft);
        timeText.text = (seconds / 60).ToString("00") + ":" + (seconds % 60).ToString("00");
        timeText.color = seconds < 45 ? new Color(1f, .58f, .4f) : Color.white;

        // Die Liste ist nur eine zweite Möglichkeit, ein Zielhaus anzuklicken.
        for (int i = 0; i < orderTexts.Length; i++)
        {
            bool exists = i < game.Orders.Count;
            orderTexts[i].text = exists ? game.Orders[i].stopName : "Kein Auftrag";
            orderButtons[i].interactable = exists && game.DepotSelected && !game.Paused;
        }

        bool freeTruck = false;
        for (int i = 0; i < truckTexts.Length; i++)
        {
            if (i < game.trucks.Length)
            {
                truckTexts[i].text = game.trucks[i].truckName + "\n" + game.trucks[i].Status;
                if (!game.trucks[i].IsBusy) freeTruck = true;
            }
            else
                truckTexts[i].text = "Zweiter Wagen\nIn Harbor Side verfügbar";
        }
        stepText.text = game.DepotSelected ? "Jetzt ein Lieferziel wählen" : "Zuerst das Lager anklicken";
        if (!freeTruck) stepText.text = "Deine Wagen sind unterwegs";

        resultTitleText.text = game.Won ? "Alles zugestellt!" : "Die Zeit ist um";
        resultDetailsText.text = game.Score + " Punkte\n"
            + game.Delivered + " / " + game.deliveryGoal + " Pakete  ·  " + game.Stars + " / 3 Sterne\n"
            + "Bestwert: " + PlayerPrefs.GetInt("BestScore_City" + game.cityNumber, 0);
        nextCityButton.gameObject.SetActive(game.Finished && game.Won && game.cityNumber == 1);
    }

    void OpenFirstCity() { game.LoadCity(1); }
    void OpenSecondCity() { game.LoadCity(2); }
    void ChooseFirstOrder() { ChooseOrder(0); }
    void ChooseSecondOrder() { ChooseOrder(1); }
    void ChooseThirdOrder() { ChooseOrder(2); }

    void ChooseOrder(int index)
    {
        if (index < game.Orders.Count) game.SelectStop(game.Orders[index]);
    }

    void ToggleFullscreen()
    {
        // Im Unity-Editor wird die Game-Ansicht separat vergrößert (Shift + Leertaste).
        if (!Application.isEditor) Screen.fullScreen = !Screen.fullScreen;
    }

    void Quit() { Application.Quit(); }
}
