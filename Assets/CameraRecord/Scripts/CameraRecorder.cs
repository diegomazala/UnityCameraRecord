using UnityEngine;
using System.Collections;
using System.IO;


namespace UI
{
    public class CameraRecorder : MonoBehaviour
    {
        public UnityEngine.UI.Button startButton;
        public UnityEngine.UI.Button stopButton;
        public UnityEngine.UI.Text frameCountText;
        public UnityEngine.UI.Text clipSizeText;
        public UnityEngine.UI.Text msgText;

        public Camera srcCamera = null;
        public CameraData clip;

        public bool recording = false;
        public float lastFrameTime = 0.0f;

      
        void OnEnable()
        {
            msgText.text = "";
        }

        void Update()
        {
            
            if (recording)
            {
                if (Time.realtimeSinceStartup < lastFrameTime + 0.33f)
                {
                    clip.AddFrame(ref srcCamera);
                    lastFrameTime = Time.realtimeSinceStartup;

                    frameCountText.text = clip.frames.Count.ToString();
                    clipSizeText.text = (clip.GetSizeInBytes() / 1024).ToString() + "kb";
                }
            }
        }


        public void OnStartButtonPressed()
        {
            clip.frames.Clear();
            recording = true;
            startButton.interactable = false;
            stopButton.interactable = true;
            lastFrameTime = Time.realtimeSinceStartup;

            msgText.text = "Recording...";
        }

        public void OnStopButtonPressed()
        {
            recording = false;
            startButton.interactable = true;
            stopButton.interactable = false;

            string filename = "cam_" + string.Format("{0:HHmmss}", System.DateTime.Now) + ".cam";
            CameraFile.Save(filename, ref clip);

            msgText.text = "Saved: " + filename;
        }

    }
}   // end namespace UI