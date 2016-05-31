using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct CameraConsts
{
    public System.UInt16 width;
    public System.UInt16 height;
    public System.Single chipWidth;
    public System.Single chipHeight;
}


[System.Serializable]
public struct CameraParams
{
    public System.UInt64 counter;
    public System.Single time;
    public System.Single fov;
    public System.Single centerX;
    public System.Single centerY;
    public System.Single k1;
    public System.Single k2;
    public System.Single zoom;
    public System.Single focus;
    public System.Single tx;
    public System.Single ty;
    public System.Single tz;
    public System.Single rx;
    public System.Single ry;
    public System.Single rz;
}


[System.Serializable]
public class CameraData
{
    public CameraConsts constants;
    public List<CameraParams> frames;


    public CameraData()
    {
        CurrentIndex = 0;
    }


    public int CurrentIndex
    {
        get;
        set;
    }


    public CameraParams CurrentFrame
    {
        get { return frames[CurrentIndex]; }
    }


    public CameraParams NextFrame
    {
        get 
        {
            if (CurrentIndex < frames.Count - 1)
                CurrentIndex++;
            
            return frames[CurrentIndex]; 
        }
    }


    public CameraParams PrevFrame
    {
        get
        {
            if (CurrentIndex > 0)
                CurrentIndex--;

            return frames[CurrentIndex];
        }
    }


    /// <summary>
    /// Calculates the lenght in bytes of an object 
    /// and returns the size 
    /// </summary>
    /// <returns></returns>
    public long GetSizeInBytes()
    {
        int size_consts = System.Runtime.InteropServices.Marshal.SizeOf(typeof(CameraConsts));
        int size_params = System.Runtime.InteropServices.Marshal.SizeOf(typeof(CameraParams));
        return size_consts + size_params * frames.Count;
    }


    public void AddFrame(ref Camera cam)
    {
        CameraParams frame = new CameraParams();
        frame.counter = (System.UInt64) frames.Count;
        frame.time = Time.realtimeSinceStartup;
        frame.fov = cam.fieldOfView;
        frame.centerX = 0.5f;
        frame.centerY = 0.5f;
        frame.k1 = 0.0f;
        frame.k2 = 0.0f;
        frame.zoom = 0.0f;
        frame.focus = 0.0f;
        frame.tx = cam.transform.localPosition.x;
        frame.ty = cam.transform.localPosition.y;
        frame.tz = cam.transform.localPosition.z;
        frame.rx = cam.transform.eulerAngles.x;
        frame.ry = cam.transform.eulerAngles.y;
        frame.rz = cam.transform.eulerAngles.z;
        frames.Add(frame);
    }
}




[System.Serializable]
public class CameraFile 
{

    public static void Load(string fileName, ref CameraData camData)
    {
        using (System.IO.BinaryReader reader = new System.IO.BinaryReader(System.IO.File.Open(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read)))
        {
            camData = new CameraData();

            camData.constants.width = reader.ReadUInt16();
            camData.constants.height = reader.ReadUInt16();
            camData.constants.chipWidth = reader.ReadSingle();
            camData.constants.chipHeight = reader.ReadSingle();
            System.UInt64 count = reader.ReadUInt64();
            camData.frames = new List<CameraParams>((int)count);
            for (System.UInt64 i = 0; i < count; ++i)
            {
                CameraParams p = new CameraParams();
                p.counter = reader.ReadUInt64();
                p.time = reader.ReadSingle();
                p.fov = reader.ReadSingle();
                p.centerX = reader.ReadSingle();
                p.centerY = reader.ReadSingle();
                p.k1 = reader.ReadSingle();
                p.k2 = reader.ReadSingle();
                p.zoom = reader.ReadSingle();
                p.focus = reader.ReadSingle();
                p.tx = reader.ReadSingle();
                p.ty = reader.ReadSingle();
                p.tz = reader.ReadSingle();
                p.rx = reader.ReadSingle();
                p.ry = reader.ReadSingle();
                p.rz = reader.ReadSingle();
                camData.frames.Add(p);
            }
            reader.Close();
        }
    }

    public static void Save(string fileName, ref CameraData camData)
    {
        using (System.IO.BinaryWriter writer = new System.IO.BinaryWriter(System.IO.File.Open(fileName, System.IO.FileMode.Create)))
        {
            writer.Write(camData.constants.width);
            writer.Write(camData.constants.height);
            writer.Write(camData.constants.chipWidth);
            writer.Write(camData.constants.chipHeight);
            writer.Write((System.UInt64)camData.frames.Count);
            foreach (CameraParams p in camData.frames)
            {
                writer.Write(p.counter);
                writer.Write(p.time);
                writer.Write(p.fov);
                writer.Write(p.centerX);
                writer.Write(p.centerY);
                writer.Write(p.k1);
                writer.Write(p.k2);
                writer.Write(p.zoom);
                writer.Write(p.focus);
                writer.Write(p.tx);
                writer.Write(p.ty);
                writer.Write(p.tz);
                writer.Write(p.rx);
                writer.Write(p.ry);
                writer.Write(p.rz);
            }
            writer.Close();
        }
    }

}
