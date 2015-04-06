using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using BulletXNA.BulletCollision;
using BulletXNA.BulletDynamics;
using BulletXNA.LinearMath;
using BulletXNA;


public class BulletWaveObject : MonoBehaviour
{

    public float mass = 1;
    private bool isDynamic = false;
    RigidBody rigidBody;
    public MeshFilter meshFilter;
    Mesh mesh;

    // Use this for initialization


    private void LocalCreateRigidBody(CollisionShape shape)
    {

        bool isDynamic = mass != 0f;
        IndexedVector3 localInertia = IndexedVector3.Zero;

        if (isDynamic)
        {
            shape.CalculateLocalInertia(mass, out localInertia);
        }


        IndexedMatrix startTransform = IndexedMatrix.Identity;
        startTransform.SetRotation(new IndexedQuaternion(transform.rotation.x,
            transform.rotation.y, transform.rotation.z, transform.rotation.w));
        startTransform._origin = (new IndexedVector3(transform.position.x, transform.position.y, transform.position.z));

        DefaultMotionState myMotionState = new DefaultMotionState(startTransform, IndexedMatrix.Identity);
        RigidBodyConstructionInfo rbInfo = new RigidBodyConstructionInfo(mass, myMotionState, shape, localInertia);
        rigidBody = new RigidBody(rbInfo);
        rigidBody.SetUserPointer(gameObject);

        BulletWorld.Instance.DynamicsWorld.AddRigidBody(rigidBody);

        renderer.material.color = Color.white;

    }

    void Start()
    {
        if (meshFilter != null)
        {
            mesh = meshFilter.mesh;
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

                //trimesh.AddTriangle(vertex0, vertex1, vertex2);

                trimesh.AddTriangle(vertex0, vertex1, vertex2,false);

            }

            ConvexShape tmpConvexShape = new ConvexTriangleMeshShape(trimesh, true);
            ShapeHull hull = new ShapeHull(tmpConvexShape);
            float margin = tmpConvexShape.GetMargin();
            hull.BuildHull(margin);
            LocalCreateRigidBody(tmpConvexShape);


        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidBody.GetActivationState() == ActivationState.ISLAND_SLEEPING)
        {
            renderer.material.color = Color.red;
        }
        else
        {
            renderer.material.color = Color.white;
        }
    }
}
