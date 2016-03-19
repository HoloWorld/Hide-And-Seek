using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System;
using System.Runtime.InteropServices;

public class CustomProjectionController : MonoBehaviour
{
    public bool enable = false;
    UdpClient udpClient;
    IPEndPoint endPoint;
    Vector3 headPosition;
    Vector3 sensorOffset;

    Camera theCam;

    Vector3 pa, pb, pc;
    Vector3 vr, vu, vn;
    float n, f;

    // Use this for initialization
    void Start()
    {
        theCam = GetComponent<Camera>();
        Application.runInBackground = true;

        enable = true;

        float screenWidth = 0.406f; //Dimensions are in meters
        float screenHeight = 0.254f;
        float halfWidth = screenWidth / 2;
        float halfHeight = screenHeight / 2;

        n = 0.05f; //what is the right value for this??????
        f = 100; //what is the right value for this?????
        headPosition = new Vector3(0.0f, -0.005f, -3.14f);
        sensorOffset = new Vector3(-0.08f, 0.17f, 0.14f);

        pa = new Vector3(-halfWidth, -halfHeight, 0);
        pb = new Vector3(halfWidth, -halfHeight, 0);
        pc = new Vector3(-halfWidth, halfHeight, 0);

        vr = new Vector3(1, 0, 0);
        vu = new Vector3(0, 1, 0);
        vn = new Vector3(0, 0, -1);

        IPAddress ip = IPAddress.Parse("127.0.0.1");
        endPoint = new IPEndPoint(ip, 11000);
        udpClient = new UdpClient(endPoint);
        listenForMessages();
    }

    void listenForMessages()
    {
        udpClient.BeginReceive(new AsyncCallback(messageRecieved), null);
    }

    void messageRecieved(IAsyncResult ar)
    {
        Byte[] data = udpClient.EndReceive(ar, ref endPoint);
        int size = Marshal.SizeOf(headPosition);
        IntPtr ptr = Marshal.AllocHGlobal(size);
        Marshal.Copy(data, 0, ptr, size);
        headPosition = (Vector3)Marshal.PtrToStructure(ptr, typeof(Vector3)) - sensorOffset;
        Marshal.FreeHGlobal(ptr);

        //Debug.Log(headPosition.x + " " + headPosition.y + " " + headPosition.z);
        enable = true;

        listenForMessages();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enable) return;

        Debug.Log("Setting camera position: " + headPosition.x + " " + headPosition.y + " " + headPosition.z);
        this.transform.position = headPosition;

        Vector3 va = pa - headPosition;
        Vector3 vb = pb - headPosition;
        Vector3 vc = pc - headPosition;

        float d = -Vector3.Dot(vn, va);

        float l = Vector3.Dot(vr, va) * n / d;
        float r = Vector3.Dot(vr, vb) * n / d;
        float b = Vector3.Dot(vu, va) * n / d;
        float t = Vector3.Dot(vu, vc) * n / d;

        Matrix4x4 frustum = glFrustum(l, r, b, t, n, f);
        Matrix4x4 translate = glTranslatef(-headPosition.x, -headPosition.y, headPosition.z);
        Matrix4x4 projection = frustum * translate;

        theCam.projectionMatrix = projection;
    }

    void LateUpdate()
    {

    }

    Matrix4x4 glFrustum(float l, float r, float b, float t, float n, float f)
    {
        Matrix4x4 m = new Matrix4x4();
        m[0, 0] = (2 * n) / (r - l);
        m[0, 1] = 0;
        m[0, 2] = (r + l) / (r - l);
        m[0, 3] = 0;

        m[1, 0] = 0;
        m[1, 1] = (2 * n) / (t - b);
        m[1, 2] = (t + b) / (t - b);
        m[1, 3] = 0;

        m[2, 0] = 0;
        m[2, 1] = 0;
        m[2, 2] = -(f + n) / (f - n);
        m[2, 3] = -(2 * f * n) / (f - n);

        m[3, 0] = 0;
        m[3, 1] = 0;
        m[3, 2] = -1;
        m[3, 3] = 0;

        return m;
    }

    Matrix4x4 glTranslatef(float x, float y, float z)
    {
        Matrix4x4 m = new Matrix4x4();
        m[0, 0] = 1;
        m[0, 1] = 0;
        m[0, 2] = 0;
        m[0, 3] = x;

        m[1, 0] = 0;
        m[1, 1] = 1;
        m[1, 2] = 0;
        m[1, 3] = y;

        m[2, 0] = 0;
        m[2, 1] = 0;
        m[2, 2] = 1;
        m[2, 3] = z;

        m[3, 0] = 0;
        m[3, 1] = 0;
        m[3, 2] = 0;
        m[3, 3] = 1;
        return m;
    }
}