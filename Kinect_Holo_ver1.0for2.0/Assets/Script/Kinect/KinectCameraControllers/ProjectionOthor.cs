using UnityEngine;
using System.Collections;

//ver1.0을 완료함
//요구사항
//1. 벽걸이가 아니라 책상형
//2. 카메라와 헤드가 부드럽게 연결

//ver2.0을 시작
//책상형 구현
//사람과 책상의 캘리브레이션 목표
//카메라가 튀지 않도록(갑자기 포커싱 잃고 튀는 걸 잡아준다.)


public class ProjectionOthor : MonoBehaviour
{

    //public Transform[] Corners;

    public Transform corner_TL;
    public Transform corner_TR;
    public Transform corner_BL;
    public Transform corner_BR;
    public GameObject playerHead;

    public Transform lookTarget;
    public bool drawNearCone, drawFrustum;

    Camera theCam;

    void Start() { theCam = GetComponent<Camera>(); }
    private Vector3 beforeOffsetVect;
    private Vector3 vel = new Vector3(10.0f, 10.0f, 10.0f);

    void optimizeCameraPositionForHead(Camera cam, GameObject head, Matrix4x4 m, float heightForEye)
    {
        float NewOffsetX = playerHead.transform.position.x * m[0, 0];
        float NewOffsetZ = playerHead.transform.position.z * m[1, 1];
        //float NewOffsetZ = jointPos.z*MultiplyZ + OffsetZ;
        Vector3 nowOffsetVect = new Vector3(NewOffsetX, heightForEye, NewOffsetZ);

        theCam.transform.position = nowOffsetVect;

        //        theCam.transform.position = Vector3.SmoothDamp(beforeOffsetVect, nowOffsetVect, ref vel, 100.0f);

        beforeOffsetVect = nowOffsetVect;
        //      ㅎㅇ
        //      구글에서 kinect prjection holographic script 치고 나서 나온 헤드트래킹 스크립트에서 따온거임
        //      훨 씬 결과물이 예쁘게 나온다. 이제 진짜 홀로그램 같네.
    }

    /*
Vector3.SmoothDamp
서서히 시간이 지남에 따라 원하는 목표벡터로 변경

활용: 캐릭터 클릭시 오브젝트 이동시킵니다.

Vector3.SmoothDamp(시작벡터Vector3,끝벡터Vector3,속도Vector3,이동시간float);

var movetime = 0.3f;
var velocity = Vector3.zero;
var targetposition = new Vector3(3,2,5);

this.transform.positiion = Vector3.SmoothDamp(this.transform.position,targetposition,velocity,movetime);
[출처] Vector3.Lerp 와 Vector3.SmoothDamp|작성자 ㅇㅇ
*/




    void LateUpdate()
    {
        Vector3 pa, pb, pc, pd;
        //pa = Corners[0].position; //Bottom-Left
        //pb = Corners[1].position; //Bottom-Right
        //pc = Corners[2].position; //Top-Left
        //pd = Corners[3].position; //Top-Right

        pa = corner_BL.position;
        pb = corner_BR.position;
        pc = corner_TL.position;
        pd = corner_TR.position;

        Vector3 pe = theCam.transform.position;// eye position

        Vector3 vr = (pb - pa).normalized; // right axis of screen
        Vector3 vu = (pc - pa).normalized; // up axis of screen
        Vector3 vn = Vector3.Cross(vr, vu).normalized; // normal vector of screen

        Vector3 va = pa - pe; // from pe to pa
        Vector3 vb = pb - pe; // from pe to pb
        Vector3 vc = pc - pe; // from pe to pc
        Vector3 vd = pd - pe; // from pe to pd

        float n = -lookTarget.InverseTransformPoint(theCam.transform.position).y; // distance to the near clip plane (screen)
        float f = theCam.farClipPlane; // distance of far clipping plane
        float d = Vector3.Dot(va, vn); // distance from eye to screen
        float l = Vector3.Dot(vr, va) * n / d; // distance to left screen edge from the 'center'
        float r = Vector3.Dot(vr, vb) * n / d; // distance to right screen edge from 'center'
        float b = Vector3.Dot(vu, va) * n / d; // distance to bottom screen edge from 'center'
        float t = Vector3.Dot(vu, vc) * n / d; // distance to top screen edge from 'center'

        Matrix4x4 p = new Matrix4x4(); // Projection matrix




        float q = n; //ㅎㅇ 본래 q대신 n이 들어가야 하는 자리이다. 알 수 없는 오류로 near을 임의로 고정 시켜 버렸다. 결과는 성공적!!!!!
                         //오리지널 프로젝션에 입각해서 돌리고 싶으면 q=n하면 됨 어차피 요 밑에 메트릭스 대입에 밖에 안씀
                         //q=n or q=0.1f; 
        p[0, 0] = 2.0f / (r - l);
        p[0, 2] = -(r + l) / (r - l);
        p[1, 1] = 2.0f / (t - b);
        p[1, 2] = -(t + b) / (t - b);
        p[2, 2] = (f + q) / (q - f);
        p[2, 3] = 2.0f * f  / (q - f);
        p[3, 2] = -1.0f;
        // ㅎㅇ


        /*
        ㅎㅇ
        위에꺼랑 비교해보면 매트릭스의미가 무엇인지 참고하면 좋을 듯
        var m : Matrix4x4;
        m[0,0] = a;  m[0,1] = b;  m[0,2] = c;  m[0,3] = moveHorizontal;
        m[1,0] = e;  m[1,1] = f;  m[1,2] = g;  m[1,3] = moveVertical;
        m[2,0] = clipPlaneRotate?;  m[2,1] = clipPlaneRotate?;  m[2,2] = FarClip;  m[2,3] = NearClip;
        m[3,0] = perspectiveX;  m[3,1] = perspectiveY;  m[3,2] = perspectiveZ;  m[3,3] = moveDepth;
        return m;
        */


        // 위처럼 옵티마이징 안하고  그냥 하면 좀 이상하긴 하지만 되긴 됨
        //      theCam.transform.position = new Vector3(playerHead.transform.position.x, playerHead.transform.position.y, 0.0f);

        theCam.projectionMatrix = p; // Assign matrix to camera
        optimizeCameraPositionForHead(theCam, playerHead, p, 40.0f);

        if (drawNearCone)
        { //Draw lines from the camera to the corners f the screen
            Debug.DrawRay(theCam.transform.position, va, Color.blue);
            Debug.DrawRay(theCam.transform.position, vb, Color.blue);
            Debug.DrawRay(theCam.transform.position, vc, Color.blue);
            Debug.DrawRay(theCam.transform.position, vd, Color.blue);
        }

        if (drawFrustum) DrawFrustum(theCam); //Draw actual camera frustum
    }

    Vector3 ThreePlaneIntersection(Plane p1, Plane p2, Plane p3)
    { //get the intersection point of 3 planes
        return ((-p1.distance * Vector3.Cross(p2.normal, p3.normal)) +
                (-p2.distance * Vector3.Cross(p3.normal, p1.normal)) +
                (-p3.distance * Vector3.Cross(p1.normal, p2.normal))) /
            (Vector3.Dot(p1.normal, Vector3.Cross(p2.normal, p3.normal)));
    }

    void DrawFrustum(Camera cam)
    {
        Vector3[] nearCorners = new Vector3[4]; //Approx'd nearplane corners
        Vector3[] farCorners = new Vector3[4]; //Approx'd farplane corners
        Plane[] camPlanes = GeometryUtility.CalculateFrustumPlanes(cam); //get planes from matrix
        Plane temp = camPlanes[1]; camPlanes[1] = camPlanes[2]; camPlanes[2] = temp; //swap [1] and [2] so the order is better for the loop

        for (int i = 0; i < 4; i++)
        {
            nearCorners[i] = ThreePlaneIntersection(camPlanes[4], camPlanes[i], camPlanes[(i + 1) % 4]); //near corners on the created projection matrix
            farCorners[i] = ThreePlaneIntersection(camPlanes[5], camPlanes[i], camPlanes[(i + 1) % 4]); //far corners on the created projection matrix
        }

        for (int i = 0; i < 4; i++)
        {
            Debug.DrawLine(nearCorners[i], nearCorners[(i + 1) % 4], Color.red, Time.deltaTime, false); //near corners on the created projection matrix
            Debug.DrawLine(farCorners[i], farCorners[(i + 1) % 4], Color.red, Time.deltaTime, false); //far corners on the created projection matrix
            Debug.DrawLine(nearCorners[i], farCorners[i], Color.red, Time.deltaTime, false); //sides of the created projection matrix
        }
    }
    /*
    void OnDrawGizmos()
    {
        Matrix4x4 m = transform.localToWorldMatrix;
        Matrix4x4 m2 = Matrix4x4.identity;
        m2[1, 1] *= theCam.aspect;
        Gizmos.matrix = m * m2;
        Gizmos.DrawFrustum(transform.position, theCam.fieldOfView, theCam.farClipPlane, theCam.nearClipPlane, theCam.aspect);
    }
    */
}