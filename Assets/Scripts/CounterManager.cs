using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class CounterManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI counterText;
    [SerializeField] GameObject endPanelGO;
    [SerializeField] GameObject counterGO;

    [SerializeField] Animation counterAnimation;

    private int counterCurrent;

    void Start()
    {
        counterCurrent = 0;
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            counterCurrent++;
            UpdateCounter();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            counterCurrent--;
            UpdateCounter();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            endPanelGO.SetActive(true);
        }
    }

    private void UpdateCounter()
    {
        counterAnimation.Play();
        counterText.text=counterCurrent.ToString();
    }


}
