using UnityEngine;
using System.Collections;
using BulletXNA;
using BulletXNA.BulletCollision;
using BulletXNA.BulletDynamics;
using BulletXNA.LinearMath;
using System.Collections.Generic;
public class BulletBox : MonoBehaviour
{

    
    public float mass = 1;
    RigidBody rigidBody;

    // Use this for initialization
    void Start()
    {

        BoxShape colShape = new BoxShape(new IndexedVector3(0.5f, 0.5f, 0.5f));
        IndexedVector3 bulletScale = new IndexedVector3();
        bulletScale.X = transform.localScale.x;
        bulletScale.Y = transform.localScale.y;
        bulletScale.Z = transform.localScale.z;
        colShape.SetLocalScaling(ref bulletScale);

        LocalCreateRigidBody(colShape);
    }

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
