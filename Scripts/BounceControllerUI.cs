using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BounceControllerUI : MonoBehaviour
{
    [Header("Controller & System")]
    [SerializeField] BounceController bounceController;
    [SerializeField] SaveSystem saveSystem;

    [Header("Buttons")]
    [SerializeField] Button start;
    [SerializeField] Button stop;
    [SerializeField] Button save;
    [SerializeField] Button load;
    [SerializeField] TMPro.TextMeshProUGUI scoreText;




    private void OnEnable()
    {
        UpdateUI();

        bounceController.onBouncedOffGround.AddListener(UpdateUI);
        // 
        if (save != null) save.onClick.AddListener(saveSystem.Save);
        if (load != null) load.onClick.AddListener(saveSystem.Load);
    }
    private void OnDisable()
    {
        bounceController.onBouncedOffGround.RemoveListener(UpdateUI);
        //
        if (save != null) save.onClick.RemoveListener(saveSystem.Save);
        if (load != null) load.onClick.RemoveListener(saveSystem.Load);
    }




    // Updates the UI text score
    void UpdateUI()
    { 
        scoreText.text = bounceController.GetBounces().ToString();
    }




    // Start and Stop Buttons "Connected from Unity UI, not from code"
    public void onButtonStartPressed()
    {
        bounceController.TurnOnGravity();
    }
    public void onButtonStopPressed()
    {
        bounceController.TurnOffGravity();
    }
}
