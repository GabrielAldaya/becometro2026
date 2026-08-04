using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.Video;

public class CounterManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI counterText;
    [SerializeField] GameObject endPanelGO;
    [SerializeField] GameObject counterGO;
    [SerializeField] GameObject titleGO;
    [SerializeField] GameObject becasGO;
    [SerializeField] Animation counterAnimation;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject bgImage;
    [SerializeField] RawImage videoTexture;

    private int counterCurrent;

    void OnEnable()
    {
        // Subscribe to the event
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        videoPlayer.loopPointReached -= OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer source)
    {
        bgImage.SetActive(true);
        //videoPlayer.gameObject.SetActive(false);
        videoTexture.gameObject.SetActive(false);
        videoPlayer.Pause();
        videoPlayer.time = 0;
        counterGO.SetActive(true);
    }

    void Start()
    {
        counterCurrent = 0;
        videoPlayer.playOnAwake = false;
        videoPlayer.waitForFirstFrame = true;

        videoPlayer.Prepare();
        //videoTexture.color = new Color(1, 1, 1, 0);
        //videoPlayer.sendFrameReadyEvents = true;

        //videoPlayer.frameReady += OnFrameReady;
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayIntro();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StopAll();
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

    private void PlayIntro()
    {
        if (!videoPlayer.isPlaying)
        {
            videoPlayer.time = 0;
            bgImage.SetActive(false);
            counterGO.SetActive(false);
            //videoPlayer.gameObject.SetActive(true);
            videoTexture.color = new Color(1, 1, 1, 1);
            videoTexture.gameObject.SetActive(true);
            videoPlayer.Play();
        }
    }

    private void StopAll()
    {
        if (!videoPlayer.isPlaying)
        {
            bgImage.SetActive(false);
            counterGO.SetActive(false);
            videoTexture.gameObject.SetActive(false);
            videoPlayer.Pause();
            videoPlayer.time = 0;
            videoTexture.color = new Color(1, 1, 1, 0);
            //videoPlayer.gameObject.SetActive(false);

        }
    }

    void OnFrameReady(VideoPlayer source, long frameIdx)
    {
        videoTexture.color = new Color(1, 1, 1, 1);
        source.frameReady -= OnFrameReady;
    }

}
