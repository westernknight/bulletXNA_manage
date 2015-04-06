using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using BulletXNA.BulletCollision;
using BulletXNA.BulletDynamics;
using BulletXNA.LinearMath;
using BulletXNA;


public class BulletWaveObject : BulletObject
{


    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            Mesh mesh = meshFilter.mesh;
            Vector3[] vertices = mesh.vertices;

            List<IndexedVector3> vertices3dList = new List<IndexedVector3>();
            foreach (var item in vertices)
            {
                IndexedVector3 v3d = new IndexedVector3();
                v3d.X = item.x;
                v3d.Y = item.y;
                v3d.Z = item.z;
                vertices3dList.Add(v3d);
            }

       

            TriangleMesh trimesh = new TriangleMesh();

            int[] indices = mesh.GetIndices(0);

            for (int i = 0; i < indices.Length / 3; i++)
            {
                int index0 = indices[i * 3];
                int index1 = indices[i * 3 + 1];
                int index2 = indices[i * 3 + 2];

                IndexedVector3 vertex0 = new IndexedVector3(vertices[index0].x, vertices[index0].y, vertices[index0].z);
                IndexedVector3 vertex1 = new IndexedVector3(vertices[index1].x, vertices[index1].y, vertices[index1].z);
                IndexedVector3 vertex2 = new IndexedVector3(vertices[index2].x, vertices[index2].y, vertices[index2].z);

                trimesh.AddTriangle(vertex0, vertex1, vertex2,false);

            }

            ConvexShape tmpConvexShape = new ConvexTriangleMeshShape(trimesh, true);
            ShapeHull hull = new ShapeHull(tmpConvexShape);
            float margin = tmpConvexShape.GetMargin();
            hull.BuildHull(margin);
            LocalCreateRigidBody(tmpConvexShape);


        }
    }


}
