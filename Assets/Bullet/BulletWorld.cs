using UnityEngine;
using System.Collections;
using BulletXNA;
using BulletXNA.BulletCollision;
using BulletXNA.BulletDynamics;
using BulletXNA.LinearMath;
using System.Collections.Generic;
public class BulletWorld : MonoBehaviour
{


    protected IBroadphaseInterface m_broadphase;
    protected CollisionDispatcher m_dispatcher;
    protected IConstraintSolver m_constraintSolver;
    protected DefaultCollisionConfiguration m_collisionConfiguration;
    private DynamicsWorld m_dynamicsWorld;
    private static BulletWorld instance;

    public static BulletWorld Instance
    {
        get { return instance; }
    }
    public DynamicsWorld DynamicsWorld
    {
        get { return m_dynamicsWorld; }
    }

    void Awake()
    {
        instance = this;

        m_collisionConfiguration = new DefaultCollisionConfiguration();
        m_dispatcher = new CollisionDispatcher(m_collisionConfiguration);

        IndexedVector3 worldMin = new IndexedVector3(-1000, -1000, -1000);
        IndexedVector3 worldMax = -worldMin;
        m_broadphase = new AxisSweep3Internal(ref worldMin, ref worldMax, 0xfffe, 0xffff, 16384, null, false);

        SequentialImpulseConstraintSolver m_constraintSolver = new SequentialImpulseConstraintSolver();

        m_dynamicsWorld = new DiscreteDynamicsWorld(m_dispatcher, m_broadphase, m_constraintSolver, m_collisionConfiguration);

        IndexedVector3 gravity = new IndexedVector3(0, -10, 0);
        m_dynamicsWorld.SetGravity(ref gravity);

    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        IList<CollisionObject> copyArray = m_dynamicsWorld.GetCollisionObjectArray();

        if (m_dynamicsWorld != null)
        {
            m_dynamicsWorld.StepSimulation(Time.deltaTime, 1);
        }
        foreach (var item in copyArray)
        {
            CollisionObject colObj = item;
            RigidBody body = RigidBody.Upcast(colObj);
         
            if (body != null)
            {
                if (body.GetMotionState() != null)
                {
                    DefaultMotionState myMotionState = (DefaultMotionState)body.GetMotionState();
                    IndexedMatrix centerOfMassWorldTrans;
                    myMotionState.GetWorldTransform(out centerOfMassWorldTrans);

                    GameObject go = body.GetUserPointer() as GameObject;

                    IndexedQuaternion iq = centerOfMassWorldTrans._basis.GetRotation();
                   


                    go.transform.position = new Vector3(centerOfMassWorldTrans._origin.X,
                        centerOfMassWorldTrans._origin.Y,
                        centerOfMassWorldTrans._origin.Z);
                    go.transform.rotation = new Quaternion(iq.X, iq.Y, iq.Z, iq.W);
                }
            }
        }
    }
}
