using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text scoreLabel;

    [SerializeField]
    private SettingPopup popup;

    // Start is called before the first frame update
    void Start()
    {
        popup.Close();
    }

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = Time.realtimeSinceStartup.ToString();
    }

    public void OpenSettings()
    {
        popup.Open();
    }
}
