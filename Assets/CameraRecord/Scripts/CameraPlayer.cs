using UnityEngine;
using System.Collections;
using System.IO;


namespace UI
{
    public class CameraPlayer : MonoBehaviour
    {
        public UnityEngine.UI.InputField clipNameUI;
        public UnityEngine.UI.Button playButton;
        public UnityEngine.UI.Button stopButton;
        public UnityEngine.UI.Text frameCountText;
        public UnityEngine.UI.Text clipSizeText;
        public UnityEngine.UI.Text msgText;

        public Camera dstCamera = null;
        public string clipFilename;
        public CameraData clip;

        public bool playing = false;
        public float lastFrameTime = 0.0f;

      
        void OnEnable()
        {
            msgText.text = "";

            clipNameUI.text = clipFilename;
            OnClipNameEnter(clipFilename);
        }

        void OnDisable()
        {
            if (playing)
                OnStopButtonPressed();
        }



        void Update()
        {

            if (playing)
            {
                if (Time.realtimeSinceStartup < lastFrameTime + 0.33f)
                {
                    CameraParams frame = clip.NextFrame;
                    dstCamera.fieldOfView = frame.fov;
                    dstCamera.transform.localPosition = new Vector3(frame.tx, frame.ty, frame.tz);
                    dstCamera.transform.localRotation = Quaternion.Euler(new Vector3(frame.rx, frame.ry, frame.rz));

                    msgText.text = "Frame: " + clip.CurrentIndex.ToString();
                    lastFrameTime = Time.realtimeSinceStartup;
                }
            }
        }


        public void OnPlayButtonPressed()
        {
            frameCountText.text = clip.frames.Count.ToString();
            clipSizeText.text = (clip.GetSizeInBytes() / 1024).ToString() + "kb";

            playing = true;
            playButton.interactable = false;
            stopButton.interactable = true;
            lastFrameTime = Time.realtimeSinceStartup;

            msgText.text = "Playing...";
            clip.CurrentIndex = 0;
        }


        public void OnStopButtonPressed()
        {
            playing = false;
            playButton.interactable = true;
            stopButton.interactable = false;
        }

        public void OnClipNameEnter(string clip_name)
        {
            clipFilename = clip_name;

            try
            {
                clip = new CameraData();
                CameraFile.Load(clipFilename, ref clip);

                msgText.text = "Clip loaded succesfully";
            }
            catch(System.Exception e)
            {
                playing = false;
                playButton.interactable = true;
                stopButton.interactable = false;

                msgText.text = "Error: Clip not loaded";
            }
        }
    }
}   // end namespace UI