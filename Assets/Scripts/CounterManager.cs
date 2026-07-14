using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class CounterManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI counterText;
    [SerializeField] GameObject endPanelGO;
    [SerializeField] GameObject counterGO;
    [SerializeField] GameObject titleGO;
    [SerializeField] GameObject becasGO;
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
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            counterCurrent++;
            UpdateCounter();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (counterCurrent == 0)
            {
                return;
            }
            counterCurrent--;
            counterText.text = counterCurrent.ToString();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndPanel();
        }
    }

    private void UpdateCounter()
    {
        counterAnimation.Play();
        counterText.text=counterCurrent.ToString();
    }

    private void EndPanel()
    {
        if (endPanelGO.activeInHierarchy)
        {
            endPanelGO.SetActive(false);
            titleGO.SetActive(true);
            becasGO.SetActive(true);
            counterGO.SetActive(true);
        }
        else
        {
            endPanelGO.SetActive(true);
            titleGO.SetActive(false);
            becasGO.SetActive(false);
            counterGO.SetActive(false);
        }
    }
}
