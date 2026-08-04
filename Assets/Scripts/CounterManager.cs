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
    [SerializeField] Animation becasTextAnimation;
    [SerializeField] Animation becasTotalesAnimation;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject bgImage;
    [SerializeField] RawImage videoTexture;

    private int counterCurrent;

    void OnEnable()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnDisable()
    {
        videoPlayer.loopPointReached -= OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer source)
    {
        bgImage.SetActive(true);
        videoTexture.gameObject.SetActive(false);
        videoPlayer.Pause();
        videoPlayer.time = 0;
        counterGO.SetActive(true);
        becasTotalesAnimation.gameObject.SetActive(false);
        becasTextAnimation.gameObject.SetActive(true);
    }

    void Start()
    {
        counterCurrent = 0;
        videoPlayer.playOnAwake = false;
        videoPlayer.waitForFirstFrame = true;
        videoPlayer.Prepare();
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
            becasTotalesAnimation.gameObject.SetActive(false);
            becasTextAnimation.gameObject.SetActive(true);
            if (counterCurrent == 0)
            {
                return;
            }
            counterCurrent--;
            counterText.text = counterCurrent.ToString();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            PlayBecasAnimation();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)){
            PlayBecasTotalesAnimation();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //EndPanel();
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

    private void PlayBecasAnimation()
    {
        becasTotalesAnimation.gameObject.SetActive(false);
        becasTextAnimation.gameObject.SetActive(true);
        becasTextAnimation.Play();
    }

    private void PlayBecasTotalesAnimation()
    {
        becasTextAnimation.gameObject.SetActive(false);
        becasTotalesAnimation.gameObject.SetActive(true);
        becasTotalesAnimation.Play();
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
        }
    }

    void OnFrameReady(VideoPlayer source, long frameIdx)
    {
        videoTexture.color = new Color(1, 1, 1, 1);
        source.frameReady -= OnFrameReady;
    }

}
