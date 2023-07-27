using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{

    private struct PointInSpace
    {
        public Vector3 Position;
        public float Time;
    }

    [SerializeField] private Transform target;
    private float speed = 7;
    private float delay = 0.1f;
    public Vector3 offset = new Vector3(-8, 7, -8);

    // Start is called before the first frame update
    void Start()
    {

    }
    private Queue<PointInSpace> pointsInSpace = new Queue<PointInSpace>();
    // Update is called once per frame
    void LateUpdate()
    {
        pointsInSpace.Enqueue(new PointInSpace() { Position = target.position, Time = Time.time });
        while (pointsInSpace.Count > 0 && pointsInSpace.Peek().Time <= Time.time - delay + Mathf.Epsilon)
        {
            transform.position = Vector3.Lerp(transform.position, pointsInSpace.Dequeue().Position + offset, Time.deltaTime * speed);
        }
        //transform.position = player.transform.position + offset;
    }
}


