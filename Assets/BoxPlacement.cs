using UnityEngine;
using System.Collections;
using BulletXNA.LinearMath;
using System.IO;

public class BoxPlacement : MonoBehaviour {

    const int ArraySizeX = 5, ArraySizeY = 5, ArraySizeZ = 5;

    const float StartPosX = -5;
    const float StartPosY = -5;
    const float StartPosZ = -3;
	// Use this for initialization
	void Start () {
        
        Vector3 scale = new Vector3(1.2f, 0.5f, 1.1f);
        Quaternion q = new Quaternion(0.2271f, 0.1864f, -0.0115f, 0.9558f);
        Matrix4x4 m = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

        IndexedVector3 scales = new IndexedVector3(1.2f, 0.5f, 1.1f);
        IndexedQuaternion q2 = new IndexedQuaternion(0.2271f, 0.1864f, -0.0115f, 0.9558f);
        IndexedBasisMatrix m2 = new IndexedBasisMatrix(m.m00, m.m01, m.m02,
            m.m10, m.m11, m.m12,
            m.m20, m.m21, m.m22);
        

        Debug.Log(m);
        transform.rotation = Quaternion.LookRotation(m.GetColumn(2), m.GetColumn(1));


        Debug.Log(m2._el0.X + " " + m2._el0.Y + " " + m2._el0.Z);
        Debug.Log(m2._el1.X + " " + m2._el1.Y + " " + m2._el1.Z);
        Debug.Log(m2._el2.X + " " + m2._el2.Y + " " + m2._el2.Z);

        Debug.Log(transform.rotation.ToString("f4"));
        Debug.Log(m2.GetRotation().X + " " + m2.GetRotation().Y + " " + m2.GetRotation().Z + " " + m2.GetRotation().W);


        Debug.Log("here");

        string file = @"c:/test.txt";
        ///how to create file       
           
        const float start_x = StartPosX - ArraySizeX / 2;
        const float start_y = StartPosY;
        const float start_z = StartPosZ - ArraySizeZ / 2;
        int k, i, j;
        for (k = 0; k < ArraySizeY; k++)
        {
            for (i = 0; i < ArraySizeX; i++)
            {
                for (j = 0; j < ArraySizeZ; j++)
                {
                    float x = 2 * i + start_x;
                    float y = 2 * k + start_y;
                    float z = 2 * j + start_z;

                    GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    go.transform.position = new Vector3(x, y, z);
                    go.AddComponent<BulletBox>();
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
