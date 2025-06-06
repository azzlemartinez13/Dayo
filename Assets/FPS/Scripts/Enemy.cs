//using System;
//using Unity.MLAgents;
//using Unity.MLAgents.Actuators;
//using Unity.MLAgents.Sensors;
//using UnityEngine;
//using UnityEngine.AI;

//public class Enemy : Agent
//{
//    public Spider spider;


//    [SerializeField] private int _attackDamage;


//    [SerializeField] private float _attackSpeed;

//    private float _attackTimePassed;

//    public event Action OnDeactivate;

//    public override void Heuristic(in ActionBuffers actionsOut)
//    {
//        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
//        if (Input.GetKey(KeyCode.W))
//        {
//            continuousActions[0] = 1f;
//        }
//        else if (Input.GetKey(KeyCode.S))
//        {
//            continuousActions[0] = -1f;
//        }
//        else
//        {
//            continuousActions[0] = 0f;
//        }
//        if (Input.GetKey(KeyCode.D))
//        {
//            continuousActions[1] = 1f;
//        }
//        else if (Input.GetKey(KeyCode.A))
//        {
//            continuousActions[1] = -1f;
//        }
//        else
//        {
//            continuousActions[1] = 0f;
//        }
//    }

//    private void Update()
//    {
//        if (_attackTimePassed < _attackSpeed)
//        {
//            _attackTimePassed += Time.deltaTime;
//        }
//        if (base.transform.localPosition.x >= 100f || base.transform.localPosition.x <= -100f || base.transform.localPosition.z > 100f || base.transform.localPosition.z < -100f)
//        {
//            AddReward(-1f);
//            EndEpisode();
//        }
//    }

//    public void ResetEnemy()
//    {
//        _attackTimePassed = 0f;

//        Vector2 vector = UnityEngine.Random.insideUnitCircle * 30f;
//        transform.localPosition = new Vector3(vector.x, 1f, vector.y);

//        if (spider != null)
//        {
//            spider.ResetSpider(); // or another appropriate method
//        }
//    }

//    public override void Initialize()
//    {

//        spider = GetComponent<Spider>();
//    }

//    public override void OnEpisodeBegin()
//    {
//        ResetEnemy();
//    }

//    public override void CollectObservations(VectorSensor sensor)
//    {
//        sensor.AddObservation(spider.currentHP / spider.maxHP);

//        if ((bool)spider)
//        {
//            sensor.AddObservation(Vector3.Distance(base.transform.position, spider.transform.position) / 200f);
//        }
//        else
//        {
//            sensor.AddObservation(1);
//        }
//    }

//    public override void OnActionReceived(ActionBuffers actions)
//    {
//        float num = Vector3.Distance(base.transform.position, spider.transform.position);
//        AddReward(-0.001f * (num / 150f) * Time.deltaTime);
//        float z = actions.ContinuousActions[0];
//        float x = actions.ContinuousActions[1];
//        Vector3 vector = new Vector3(x, 0f, z);

//        base.OnActionReceived(actions);
//    }

//    //private void Attack(Base b)
//    //{
//    //    b.TakeDamage(_attackDamage);
//    //    _attackTimePassed = 0f;
//    //    AddReward(0.25f);
//    //}
//}