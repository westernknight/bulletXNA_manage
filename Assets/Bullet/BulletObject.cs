using UnityEngine;
using System.Collections;
using BulletXNA.BulletDynamics;
using BulletXNA.BulletCollision;
using BulletXNA.LinearMath;
using BulletXNA;

public class BulletObject : MonoBehaviour
{

    protected CollisionObject collisionObject;
    
    public float mass = 1;
    public float friction = 1;

    public bool isKinematic = false;
    
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

#if false
            //rigidBody如果不设置MotionState，是不会产生碰撞效果，但如果设置了MotionState，及时设置了CollisionFlags.CF_KINEMATIC_OBJECT，rigidBody会被其他碰撞体影响
            DefaultMotionState myMotionState = new DefaultMotionState(startTransform, IndexedMatrix.Identity);
            RigidBodyConstructionInfo rbInfo = new RigidBodyConstructionInfo(mass, null, shape, localInertia);
            rigidBody = new RigidBody(rbInfo);
            rigidBody.SetUserPointer(gameObject);
            rigidBody.SetActivationState(ActivationState.DISABLE_DEACTIVATION);
            //rigidBody.SetCollisionFlags(BulletXNA.BulletCollision.CollisionFlags.CF_KINEMATIC_OBJECT);
            BulletWorld.Instance.DynamicsWorld.AddRigidBody(rigidBody);
#else
            collisionObject = new CollisionObject();
            collisionObject.SetWorldTransform(startTransform);
            collisionObject.SetCollisionShape(shape);
            collisionObject.SetFriction(friction);
            //如果没有CollisionFlags.CF_KINEMATIC_OBJECT，与collisionObject碰撞的碰撞体会静止，并且当移走collisionObject，碰撞体会静止在空气上            
            collisionObject.SetCollisionFlags(BulletXNA.BulletCollision.CollisionFlags.CF_KINEMATIC_OBJECT);
            BulletWorld.Instance.DynamicsWorld.AddCollisionObject(collisionObject);

#endif


            renderer.material.color = Color.green;
        }
        else
        {
            DefaultMotionState myMotionState = new DefaultMotionState(startTransform, IndexedMatrix.Identity);
            RigidBodyConstructionInfo rbInfo = new RigidBodyConstructionInfo(mass, myMotionState, shape, localInertia);
            collisionObject = new RigidBody(rbInfo);
            collisionObject.SetUserPointer(gameObject);
            collisionObject.SetFriction(friction);

            BulletWorld.Instance.DynamicsWorld.AddRigidBody(collisionObject as RigidBody);

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
             collisionObject.SetWorldTransform(worldransform);

        }
        else
        {
            if (((RigidBody)collisionObject).GetActivationState() == ActivationState.ISLAND_SLEEPING)
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
