using UnityEngine;
using System.Collections;
using System.IO;


public class CameraFileTest : MonoBehaviour 
{
    public Camera srcCamera = null;
    public string fileName = @"camera.cam";

    private BinaryReader reader;

    public System.Int32 frameCount = 0;
    public System.Single time = 0;

    public CameraData cam_to_file;
    public CameraData cam_from_file;

	void OnEnable () 
    {
        cam_to_file.constants.width = 1920;
        cam_to_file.constants.height = 1080;

        for (int i = 0; i < 3; ++i)
        {
            CameraParams p = new CameraParams();
            p.counter = (System.UInt64)i;
            p.rx = (System.Single)i * 10 + 1;
            p.ry = (System.Single)i * 10 + 2;
            p.rz = (System.Single)i * 10 + 3;
            cam_to_file.frames.Add(p);
        }
	}

    void OnDisable()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CameraFile.Save("cameradata.cam", ref cam_to_file);
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            CameraFile.Load("cameradata.cam", ref cam_from_file);
        }
    }

    
}
