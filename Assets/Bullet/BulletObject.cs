using UnityEngine;
using System.Collections;
using BulletXNA.BulletDynamics;
using BulletXNA.BulletCollision;
using BulletXNA.LinearMath;
using BulletXNA;

public class BulletObject : MonoBehaviour {

    public float mass = 1;
    RigidBody rigidBody;
    SphereCollider collider;

    public bool isKinematic = false;

    GhostObject ghostObject;

    // Use this for initialization

    protected void LocalCreateRigidBody(CollisionShape shape)
    {

        bool isDynamic = mass != 0f;
        IndexedVector3 localInertia = IndexedVector3.Zero;

        IndexedMatrix startTransform = IndexedMatrix.Identity;
        startTransform.SetRotation(new IndexedQuaternion(transform.rotation.x,
            transform.rotation.y, transform.rotation.z, transform.rotation.w));
        startTransform._origin = (new IndexedVector3(transform.position.x, transform.position.y, transform.position.z));

        if (isDynamic)
        {
            shape.CalculateLocalInertia(mass, out localInertia);
        }


        if (isKinematic)
        {
            ghostObject = new GhostObject();
            ghostObject.SetWorldTransform(startTransform);
            ghostObject.SetCollisionShape(shape);
            ghostObject.SetCollisionFlags(BulletXNA.BulletCollision.CollisionFlags.CF_KINEMATIC_OBJECT);
            BulletWorld.Instance.DynamicsWorld.AddCollisionObject(ghostObject, CollisionFilterGroups.KinematicFilter, CollisionFilterGroups.AllFilter);

            renderer.material.color = Color.green;
        }
        else
        {
            DefaultMotionState myMotionState = new DefaultMotionState(startTransform, IndexedMatrix.Identity);
            RigidBodyConstructionInfo rbInfo = new RigidBodyConstructionInfo(mass, myMotionState, shape, localInertia);
            rigidBody = new RigidBody(rbInfo);
            rigidBody.SetUserPointer(gameObject);

            BulletWorld.Instance.DynamicsWorld.AddRigidBody(rigidBody);

            renderer.material.color = Color.white;
        }


    }
    void Update()
    {
        if (isKinematic)
        {

            IndexedMatrix worldransform = IndexedMatrix.Identity;
            worldransform.SetRotation(new IndexedQuaternion(transform.rotation.x,
                transform.rotation.y, transform.rotation.z, transform.rotation.w));
            worldransform._origin = (new IndexedVector3(transform.position.x, transform.position.y, transform.position.z));
            ghostObject.SetWorldTransform(worldransform);
        }
        else
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

}
