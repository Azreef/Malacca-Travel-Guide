//This script is used to manage video player.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.Video;
using System.Linq;
using UnityEngine.UI;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoPlayer tutorialVideoPlayer;

    [Header("Buttons")]
    public Button playButton;
    public Button pauseButton;
    public Button nextButton;
    public Button previousButton;
    public Button closeButton;

    private LocationInformation locationInformation;

    private List<VideoClip> currentVideoClipList;
    private int currentTrack;

    public VideoClip tutorialVideo;

    public Slider videoSlider;
    public Slider tutorialVideoSlider;
    public bool isDraggingVideo = false;

    void Start()
    {
        tutorialVideoPlayer.clip = tutorialVideo;
        
        videoSlider.onValueChanged.AddListener(HandleVideoSliderValueChanged);
        tutorialVideoSlider.onValueChanged.AddListener(HandleVideoSliderValueChanged);
    }

    void Update()
    {
        UpdateVideoSlider();
    }

    public void SetPlaylist(LocationInformation info )
    {
        if (info != locationInformation || locationInformation == null)
        {
            videoPlayer.Stop();
        }
            
        if (info != null && info.videoClipList.Count > 0)
        {
            locationInformation = info;
            currentVideoClipList = locationInformation.videoClipList;

            currentTrack = 0;
            videoPlayer.clip = currentVideoClipList[currentTrack];

            videoPlayer.Play();

        }
        else
        {
            Debug.LogError("ERROR: Video Not Found");

        }
             
    }
    public void ReplayTutorialVideo()
    {
        tutorialVideoPlayer.Stop();
        tutorialVideoPlayer.clip = tutorialVideo;
        tutorialVideoPlayer.Play();
    }
    public void PauseTutorialVideo()
    {
        tutorialVideoPlayer.Pause();
    }
    public void ResumeTutorialVideo()
    {
        if (tutorialVideoPlayer.clip == null || tutorialVideoPlayer.clip == tutorialVideo)
        {
            tutorialVideoPlayer.clip = tutorialVideo;
            tutorialVideoPlayer.Play();
        }
        else
        {
            tutorialVideoPlayer.Play();
        }
    }


    public void PlayVideo(int selectedTrack)
    {
        videoPlayer.Stop();
        videoPlayer.clip = currentVideoClipList[selectedTrack];
        videoPlayer.Play();
    }


    public void NextVideo()
    {
        if (currentTrack < currentVideoClipList.Count - 1)
        {
            currentTrack++;
        }
        else
        {
            currentTrack = 0;
        }

        PlayVideo(currentTrack);
    }


    public void PrevVideo()
    {
        if (currentTrack > 0)
        {
            currentTrack--;
        }
        else
        {
            currentTrack = currentVideoClipList.Count - 1;
        }

        PlayVideo(currentTrack);
    }


    public void PauseVideo()
    {
        videoPlayer.Pause();
    }

    public void ResumeVideo()
    {
        if(videoPlayer.clip == null)
        {
            PlayVideo(currentTrack);
        }
        else
        {
            videoPlayer.Play();
        }
    }

    public void StopVideo()
    {
        videoPlayer.Stop();
    }

    public void BeginDrag()
    {
        isDraggingVideo = true;
    }

    public void EndDrag()
    {
        isDraggingVideo = false;
    }
    
    public void UpdateVideoSlider()
    {
        if (!isDraggingVideo)
        {
            if (videoPlayer.isActiveAndEnabled)
            {
                videoSlider.value = (float)videoPlayer.time;

                if (videoSlider.maxValue != (float)videoPlayer.length)
                {
                    videoSlider.maxValue = (float)videoPlayer.length;
                }
            }
            else if (tutorialVideoPlayer.isActiveAndEnabled)
            {
                tutorialVideoSlider.value = (float)tutorialVideoPlayer.time;

                if (tutorialVideoSlider.maxValue != (float)tutorialVideoPlayer.length)
                {
                    tutorialVideoSlider.maxValue = (float)tutorialVideoPlayer.length;
                }
            }


        }
    }

    public void HandleVideoSliderValueChanged(float value)
    {
        if (isDraggingVideo)
        {
            if(videoPlayer.isActiveAndEnabled)
            {          
                videoPlayer.time = value;
            }
            else if (tutorialVideoPlayer.isActiveAndEnabled)
            {       
                tutorialVideoPlayer.time = value;
            }
            
        }
    }
}
