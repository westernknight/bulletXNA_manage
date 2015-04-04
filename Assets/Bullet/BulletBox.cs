using UnityEngine;
using System.Collections;
using BulletXNA;
using BulletXNA.BulletCollision;
using BulletXNA.BulletDynamics;
using BulletXNA.LinearMath;
using System.Collections.Generic;
public class BulletBox : MonoBehaviour
{

    BoxShape colShape;
    public float mass = 1;

    private Vector3 size;
    IndexedVector3 bulletScale = new IndexedVector3();
    // Use this for initialization
    void Start()
    {
        BoxCollider col = GetComponent<BoxCollider>();
        colShape = new BoxShape(new IndexedVector3(col.size.x / 2, col.size.y / 2, col.size.z / 2));
        bulletScale.X = transform.localScale.x;
        bulletScale.Y = transform.localScale.y;
        bulletScale.Z = transform.localScale.z;
        colShape.SetLocalScaling(ref bulletScale);
        bool isDynamic = mass != 0f;
        IndexedVector3 localInertia = IndexedVector3.Zero;

        if (isDynamic)
        {
            colShape.CalculateLocalInertia(mass, out localInertia);
        }
            
        
        IndexedMatrix startTransform = IndexedMatrix.Identity;
        startTransform.SetRotation(new IndexedQuaternion(transform.rotation.x,
            transform.rotation.y, transform.rotation.z, transform.rotation.w));
        startTransform._origin = (new IndexedVector3(transform.position.x, transform.position.y, transform.position.z));
        
        DefaultMotionState myMotionState = new DefaultMotionState(startTransform, IndexedMatrix.Identity);
        RigidBodyConstructionInfo rbInfo = new RigidBodyConstructionInfo(mass, myMotionState, colShape, localInertia);
        RigidBody body = new RigidBody(rbInfo);
        body.SetUserPointer(gameObject);
 
        BulletWorld.Instance.DynamicsWorld.AddRigidBody(body);
      
    }

    // Update is called once per frame
    void Update()
    {

    }
}
