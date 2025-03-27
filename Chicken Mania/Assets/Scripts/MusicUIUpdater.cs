/*using UnityEngine;
using TMPro;

public class MusicUIUpdater : MonoBehaviour
{
    public MusicHandler musicHandler; // Reference to the MusicHandler
    public TextMeshProUGUI[] trackInfoTexts; // All UI text objects that will display the music info
    void Start()
    {
        UpdateUI(); // Update UI when the game starts
    }

    void Update()
    {
        UpdateUI(); // Update UI to reflect current track
    }

    void UpdateUI()
    {
        if (musicHandler != null && trackInfoTexts.Length > 0)
        {
            string trackInfo = musicHandler.GetCurrentTrackInfo();
            foreach (var textUI in trackInfoTexts)
            {
                if (textUI != null)
                {
                    textUI.text = trackInfo;
                }
            }
        }
    }
}
*/

using UnityEngine;
using TMPro;

public class MusicUIUpdater : MonoBehaviour
{
    public MusicHandler musicHandler; // Reference to the MusicHandler
    public TextMeshProUGUI[] trackTitleText; // UI Text for the track title
    public TextMeshProUGUI[] trackCreditText; // UI Text for the track credit (artist/website)

    void Start()
    {
        UpdateUI(); // Update UI when the game starts
    }

    void Update()
    {
        UpdateUI(); // Update UI to reflect current track
    }

    void UpdateUI()
    {
        if (musicHandler != null)
        {
            // Get track title and credits
            string trackTitle = musicHandler.GetCurrentTrackTitle();
            string trackCredits = musicHandler.GetCurrentTrackCredit();

            // Update the UI text components for track titles
            if (trackTitleText != null && trackTitleText.Length > 0)
            {
                foreach (var titleText in trackTitleText)
                {
                    if (titleText != null)
                    {
                        titleText.text = trackTitle;
                    }
                }
            }

            // Update the UI text components for track credits
            if (trackCreditText != null && trackCreditText.Length > 0)
            {
                foreach (var creditText in trackCreditText)
                {
                    if (creditText != null)
                    {
                        creditText.text = trackCredits;
                    }
                }
            }
        }
    }
}
