using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.IO;

public class GameManager : MonoBehaviour
{
    List<int> normalDrinkingsCuesTime;
    List<int> hardcoreDrinkingsCuesTime;

    public Camera cam;

    public AudioSource normalBeep;
    public AudioSource hardcoreBeep;

    /*public GameObject video;
    VideoPlayer vp;*/

    public GameObject hintText;
    public GameObject endOfGameText;
    public GameObject instructions;

    int normalDrinkingCuesIndex;
    int hardcoreDrinkingCuesIndex;

    //bool isMovieOn;

    bool hasHintBeenActivated;

    private void Start()
    {
        //Desktop version
        //SetUpVideo();

        SetUpCues();
    }

    /*void SetUpVideo() {
        vp = video.GetComponent<VideoPlayer>();
    }*/

    void SetUpCues() {
        int hardcoreCounter = 0;
        int maxHardcoreCounter = 16;
        int currentCue = 0;

        normalDrinkingsCuesTime = new List<int>();
        hardcoreDrinkingsCuesTime = new List<int>();

        for (int i = 0; i < 50; i++) {
            currentCue += Random.Range(5, 15);

            if (hardcoreCounter == 3 || Random.Range(0, 3) == 2) {
                hardcoreDrinkingsCuesTime.Add(currentCue);
                hardcoreCounter = 0;
            } 
            else normalDrinkingsCuesTime.Add(currentCue);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Desktop version
        /*if (normalDrinkingCuesIndex < normalDrinkingsCuesTime.Length && vp.time >= normalDrinkingsCuesTime[normalDrinkingCuesIndex]) {
            StartCoroutine(FlashScreen(normalBeep, Color.red));
            normalDrinkingCuesIndex++;
            Debug.Log(vp.time);
        }
        else if (hardcoreDrinkingCuesIndex < hardcoreDrinkingsCuesTime.Length && vp.time >= hardcoreDrinkingsCuesTime[hardcoreDrinkingCuesIndex])
        {
            StartCoroutine(FlashScreen(hardcoreBeep, Color.blue));
            hardcoreDrinkingCuesIndex++;
        }

        if (!hasHintBeenActivated && vp.time >= 60) ActivateHint();

        if (Input.GetKeyDown(KeyCode.M) || Input.touches.Length == 4) ToggleMovie();
        else if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeSpeed(1);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeSpeed(2);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) ChangeSpeed(4);
        else if (Input.GetKeyDown(KeyCode.Alpha4)) ChangeSpeed(8);*/

        if (normalDrinkingCuesIndex < normalDrinkingsCuesTime.Count && Time.time >= normalDrinkingsCuesTime[normalDrinkingCuesIndex])
        {
            StartCoroutine(FlashScreen(normalBeep, Color.red));
            normalDrinkingCuesIndex++;

            if (normalDrinkingCuesIndex == normalDrinkingsCuesTime.Count && hardcoreDrinkingCuesIndex == hardcoreDrinkingsCuesTime.Count) {
                EndOfGame();
            }
        }
        else if (hardcoreDrinkingCuesIndex < hardcoreDrinkingsCuesTime.Count && Time.time >= hardcoreDrinkingsCuesTime[hardcoreDrinkingCuesIndex])
        {
            StartCoroutine(FlashScreen(hardcoreBeep, Color.blue));
            hardcoreDrinkingCuesIndex++;

            if (normalDrinkingCuesIndex == normalDrinkingsCuesTime.Count && hardcoreDrinkingCuesIndex == hardcoreDrinkingsCuesTime.Count)
            {
                EndOfGame();
            }
        }

        if (!hasHintBeenActivated && Time.time >= 90) ActivateHint();
    }

    IEnumerator FlashScreen(AudioSource sound, Color color) {
        cam.backgroundColor = color;
        sound.Play();
        yield return new WaitForSeconds(.2f);
        cam.backgroundColor = Color.white;
        
    }

    void EndOfGame() {
        endOfGameText.SetActive(true);
        instructions.SetActive(false);
    }

    /*void ToggleMovie() {
        isMovieOn = !isMovieOn;

        video.GetComponent<MeshRenderer>().enabled = isMovieOn;
        vp.SetDirectAudioMute(0, !isMovieOn);
        vp.SetDirectAudioVolume(0, isMovieOn ? 1 : 0);

        hintText.SetActive(false);
        hasHintBeenActivated = true;
    }*/

    /*void ChangeSpeed(int mult) {
        vp.playbackSpeed = mult;
    }*/

    void ActivateHint() {
        hintText.SetActive(true);
        hasHintBeenActivated = true;
    }
}
