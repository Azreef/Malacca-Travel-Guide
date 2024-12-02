//This script is used to manage video player.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;
using System.Linq;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    [Header("Buttons")]
    public Button playButton;
    public Button pauseButton;
    public Button nextButton;
    public Button previousButton;

    private LocationInformation locationInformation;

    private List<VideoClip> currentVideoClipList;
    private int currentTrack;


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

        }
        else
        {
            Debug.LogError("ERROR: Video Not Found");

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

}
