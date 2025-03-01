using UnityEngine;
using TMPro;
using TouchScript.Gestures;

public class InactivityHandlerOld : MonoBehaviour
{
    public float inactivityThreshold = 60f; // Seconds before warning
    public float returnToMenuTime = 30f;    // Seconds before message disappears
    private float lastInteractionTime;
    private float countdownTime;
    private bool countdownStarted = false;

    public GameObject inactivityWarning;
    public TextMeshProUGUI countdownText;

    public ShopManager shopManager;
    public GameObject hudObject; // Reference to the HUD object
    public GameObject Screen;

    private void Start()
    {
        lastInteractionTime = Time.time;

        if (inactivityWarning != null)
        {
            inactivityWarning.SetActive(false);
        }

        RegisterTouchGestures();
    }

    private void Update()
    {
        // Check inactivity per instance
        if (!countdownStarted && Time.time - lastInteractionTime > inactivityThreshold)
        {
            ShowInactivityWarning();
        }

        // Handle countdown per instance
        if (countdownStarted)
        {
            countdownTime -= Time.deltaTime;
            countdownText.text = "Returning to Main Menu in: " + Mathf.Ceil(countdownTime) + "s";

            if (countdownTime <= 0)
            {
                inactivityWarning.SetActive(false);
                countdownStarted = false; // Reset so it can trigger again later
                shopManager.ResetGame();
            }
        }
    }

    private void RegisterTouchGestures()
    {
        if (Screen != null)
        {
            TapGesture tapGesture = Screen.GetComponent<TapGesture>();
            PressGesture pressGesture = Screen.GetComponent<PressGesture>();

            if (tapGesture != null)
            {
                tapGesture.Tapped += OnUserInteraction;
            }

            if (pressGesture != null)
            {
                pressGesture.Pressed += OnUserInteraction;
            }
        }

        if (hudObject != null)
        {
            TapGesture tapGesture = hudObject.GetComponent<TapGesture>();
            PressGesture pressGesture = hudObject.GetComponent<PressGesture>();

            if (tapGesture != null)
            {
                tapGesture.Tapped += OnUserInteraction;
            }

            if (pressGesture != null)
            {
                pressGesture.Pressed += OnUserInteraction;
            }
        }
        
    }

    private void OnUserInteraction(object sender, System.EventArgs e)
    {
        Debug.Log("here");
        ResetInactivityTimer();
    }

    private void ResetInactivityTimer()
    {
        lastInteractionTime = Time.time;

        if (inactivityWarning != null && inactivityWarning.activeSelf)
        {
            inactivityWarning.SetActive(false);
            countdownStarted = false;
        }
    }

    private void ShowInactivityWarning()
    {
        if (inactivityWarning != null && !inactivityWarning.activeSelf)
        {
            inactivityWarning.SetActive(true);
            countdownStarted = true;
            countdownTime = returnToMenuTime;
        }
    }
}
