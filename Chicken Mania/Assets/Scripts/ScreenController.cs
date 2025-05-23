using UnityEngine;
using TouchScript.Gestures;
using System;

public class ScreenController : MonoBehaviour
{
    public GameObject startUI;  // Assign Canvas StartUI in the Inspector
    public GameObject gameUI;   // Assign Canvas GameUI in the Inspector
    public GameObject gameObjects; // Assign GameObjects in the Inspector
    public GameObject shopManager; // Assign ShopManager in the Inspector
    public string screenName;   // For debugging purposes

    [Header("Game Start Buttons")]
    public GameObject GameUI_TimerMode;
    public GameObject ManiaModeButton; //Assign button for Mania Mode in the Inspector
    public GameObject TimerModeButton; //Assign button for Timed Mode in the Inspector
    public GameObject TycoonModeButton; //Assign button for Tycoon Mode in the Inspector
    public GameObject DefendModeButton; //Assign button for Defend Mode in the Inspector

    [Header("Platform")]
    public GameObject NormalGround; //Assign the normal polyobject for normal mode
    public GameObject TimedGround;  //Assign the timed polyobject for timed mode

    private TapGesture tapGesture; // TouchScript's Tap Gesture

    public ShopManager shopManagerScript; // Reference to ShopManager script

    public GameObject TimeMiddleLeft;
    public GameObject ProtectMiddleLeft;
    public GameObject Middle_Left_Mania;
    public GameObject Middle_Left_Tycoon;

    public GameObject RedBarn;
    public GameObject BlueBarn;

    private bool gameModeStarted = false;

    public float timetoGrow = 10f;
    public float timetoSpawn = 10f;

    private void OnEnable()
    {
        // Get the TapGesture component and subscribe to the event
        tapGesture = GetComponent<TapGesture>();
        if (tapGesture != null)
        {
            tapGesture.Tapped += OnTap;
            tapGesture.StateChanged += OnGestureStateChanged; // Listen to state changes
        }
    }

    // Handle the gesture state change(for more advanced gesture tracking)
    private void OnGestureStateChanged(object sender, GestureStateChangeEventArgs e)
    {
        // For debugging, check the gesture's state
        //Debug.Log($"Gesture state changed from {e.PreviousState} to {e.State} on {screenName}");
    }

    private void OnDisable()
    {
        // Unsubscribe from eventsks
        if (tapGesture != null)
        {
            tapGesture.Tapped -= OnTap;
            tapGesture.StateChanged -= OnGestureStateChanged;
        }
    }

    private void OnTap(object sender, System.EventArgs e)
    {
        GameObject tappedObject = ((Component)sender).gameObject;

        if (tappedObject == ManiaModeButton)
        {
            StartGame();
        }
        else if (tappedObject == TimerModeButton)
        {
            TGameMode();
        }
        else if (tappedObject == TycoonModeButton)
        {
            StartTycoonGameMode();
        }
        else if (tappedObject == DefendModeButton)
        {
            ProtectGameMode();
        }
    }

    public void StartGame()
    {
        shopManagerScript.FoxDir.CancelInvoke("UpdateChickenList");
        shopManagerScript.FoxDir.InvokeRepeating("UpdateChickenList", shopManagerScript.FoxDir.graceTime = 10, shopManagerScript.FoxDir.spawnTick = 10);
        startUI.SetActive(false);  // Hide start UI
        gameUI.SetActive(true);    // Show in-game UI

        CanvasGroup canvasGroup = gameUI.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameUI.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 255;  // Make it visible
        canvasGroup.interactable = true;  // enable interaction
        canvasGroup.blocksRaycasts = true; // blocks clicks

        gameObjects.SetActive(true); // Activate game objects
        NormalGround.SetActive(true);
        TimedGround.SetActive(false);
        RedBarn.SetActive(true);
        BlueBarn.SetActive(false);
        shopManager.SetActive(true); // Activate shop manager
        Middle_Left_Mania.SetActive(true);
        Middle_Left_Tycoon.SetActive(false);

        shopManagerScript.inactivityThreshold = 60f; //Time set to higher than the game's time mode
        shopManagerScript.timeToGrow = 10f;
        shopManagerScript.timeToSpawn = 10f;

        shopManagerScript.EggValue = 0.25f;
        shopManagerScript.ChickValue = 0.5f;
        shopManagerScript.ChickenValue = 0.6f;
        shopManagerScript.TycoonMode = false;

        // base price for mania
        shopManagerScript.Inventory[2, 1] = 20;
        shopManagerScript.Inventory[2, 2] = 30;
        shopManagerScript.Inventory[2, 3] = 75;
        shopManagerScript.Inventory[2, 4] = 113;
        shopManagerScript.Inventory[2, 5] = 281;
        shopManagerScript.Inventory[2, 6] = 600;
        shopManagerScript.Inventory[2, 7] = 50000;
        // Upgrades for mania
        shopManagerScript.Inventory[2, 8] = 30; // 30 60 120
        shopManagerScript.Inventory[2, 9] = 25;  //25 64 262
        shopManagerScript.Inventory[2, 10] = 25; // 25 50 75
        shopManagerScript.Inventory[2, 11] = 50; // 50 128 524

        shopManagerScript.FoxDir.spawnTick = 10;
        shopManagerScript.FoxDir.maxFoxes = 99;
        shopManagerScript.GoldEggChance = 100;
    }

    public void StartTycoonGameMode()
    {
        shopManagerScript.FoxDir.CancelInvoke("UpdateChickenList");
        shopManagerScript.FoxDir.InvokeRepeating("UpdateChickenList", shopManagerScript.FoxDir.graceTime = 10, shopManagerScript.FoxDir.spawnTick = 10);
        startUI.SetActive(false);  // Hide start UI
        gameUI.SetActive(true);    // Show in-game UI

        CanvasGroup canvasGroup = gameUI.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameUI.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 255;  // Make it visible
        canvasGroup.interactable = true;  // enable interaction
        canvasGroup.blocksRaycasts = true; // blocks clicks

        gameObjects.SetActive(true); // Activate game objects
        NormalGround.SetActive(true);
        TimedGround.SetActive(false);
        RedBarn.SetActive(false);
        BlueBarn.SetActive(true);
        shopManager.SetActive(true); // Activate shop manager
        Middle_Left_Mania.SetActive(false);
        Middle_Left_Tycoon.SetActive(true);

        shopManagerScript.inactivityThreshold = 60f; //Time set to higher than the game's time mode
        shopManagerScript.timeToGrow = 10f;
        shopManagerScript.timeToSpawn = 6f;

        shopManagerScript.EggValue = 0.1f;
        shopManagerScript.ChickValue = 0.0025f;
        shopManagerScript.ChickenValue = 0.005f;
        shopManagerScript.TycoonMode = true;

        // base price for tycoon
        shopManagerScript.Inventory[2, 1] = 100;
        shopManagerScript.Inventory[2, 2] = 200;
        shopManagerScript.Inventory[2, 3] = 450;
        shopManagerScript.Inventory[2, 4] = 600;
        shopManagerScript.Inventory[2, 5] = 1000;
        shopManagerScript.Inventory[2, 6] = 5000;
        shopManagerScript.Inventory[2, 7] = 25000;
        // Upgrades for tycoon
        shopManagerScript.Inventory[2, 8] = 50; //75, 100
        shopManagerScript.Inventory[2, 9] = 100; //250, 600
        shopManagerScript.Inventory[2, 10] = 25; //50, 75
        shopManagerScript.Inventory[2, 11] = 300; //800, 3000

        shopManagerScript.FoxDir.spawnTick = 10;
        shopManagerScript.FoxDir.maxFoxes = 5;
        shopManagerScript.GoldEggChance = 100;
    }

    public void ReturnToTitlePage()
    {
        GameUI_TimerMode.SetActive(false);
        gameUI.SetActive(false);
        gameObjects.SetActive(false);
        shopManager.SetActive(false);
        startUI.SetActive(true);
        gameModeStarted = false;
    }

    public void TGameMode()
    {
        shopManagerScript.FoxDir.CancelInvoke("UpdateChickenList");
        shopManagerScript.FoxDir.InvokeRepeating("UpdateChickenList", shopManagerScript.FoxDir.graceTime = 15, shopManagerScript.FoxDir.spawnTick = 10);
        if (!gameModeStarted)
        {
            startUI.SetActive(false);  // Hide start UI
            gameObjects.SetActive(true); // Activate game objects
            NormalGround.SetActive(false);
            TimedGround.SetActive(true);
            RedBarn.SetActive(true);
            BlueBarn.SetActive(false);
            shopManager.SetActive(true); // Activate shop manager
            gameUI.SetActive(false);
            CanvasGroup canvasGroup = gameUI.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameUI.AddComponent<CanvasGroup>();
            }
            canvasGroup.alpha = 0;  // Make it fully transparent
            canvasGroup.interactable = false;  // Disable interaction
            canvasGroup.blocksRaycasts = false; // Prevents blocking clicks

            GameUI_TimerMode.SetActive(true);
            TimeMiddleLeft.SetActive(true);
            ProtectMiddleLeft.SetActive(false);
            shopManagerScript.StartCountdown();

            //shopManagerScript.Inventory[3, 9] = 2;  //DOESN'T SET INITIALLY
            shopManagerScript.timeToGrow = 7f;
            shopManagerScript.timeToSpawn = 7f;
            shopManagerScript.GoldEggChance = 1000000;
            shopManagerScript.FoxDir.spawnTick = 10;
            shopManagerScript.FoxDir.graceTime = 15;
            shopManagerScript.FoxDir.maxFoxes = 99;
            shopManagerScript.inactivityThreshold = 6000f; //Time set to higher than the game's time mode
            gameModeStarted = true;
        }
        /*
        // If somehow they lose all chickens/chicks/eggs then.. spawn 1 so doesn't trigger the normal Gamemode's gameover
        // Probably not needed because if money=20, they will stay until timer reaches 0.. for proper exit
        if (shopManagerScript.chickensCount + shopManagerScript.chicksCount + shopManagerScript.eggsCount <= 0)
        {
            int itemId = Random.Range(1, 7); // Randomly select an item ID between 1 and 6
            if (itemId >= 1 && itemId <= 6)
            {
                shopManagerScript.SpawnChicken(itemId);
                shopManagerScript.AddChicken();
            }
        }
        */

    }
    public void ProtectGameMode()
    {
        if (!gameModeStarted)
        {
            startUI.SetActive(false);  // Hide start UI
            gameObjects.SetActive(true); // Activate game objects
            NormalGround.SetActive(false);
            TimedGround.SetActive(true);
            RedBarn.SetActive(true);
            BlueBarn.SetActive(false);
            shopManager.SetActive(true); // Activate shop manager
            gameUI.SetActive(false);
            CanvasGroup canvasGroup = gameUI.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameUI.AddComponent<CanvasGroup>();
            }
            canvasGroup.alpha = 0;  // Make it fully transparent
            canvasGroup.interactable = false;  // Disable interaction
            canvasGroup.blocksRaycasts = false; // Prevents blocking clicks

            GameUI_TimerMode.SetActive(true);
            TimeMiddleLeft.SetActive(false);
            ProtectMiddleLeft.SetActive(true);
            shopManagerScript.StartCountdownPGM();
            shopManagerScript.timeToSpawn = 10000f;
            shopManagerScript.FoxDir.spawnTick = 10;
            shopManagerScript.FoxDir.maxFoxes = 99;
            shopManagerScript.inactivityThreshold = 6000f; //Time set to higher than the game's time mode
            gameModeStarted = true;
        }


    }
}
