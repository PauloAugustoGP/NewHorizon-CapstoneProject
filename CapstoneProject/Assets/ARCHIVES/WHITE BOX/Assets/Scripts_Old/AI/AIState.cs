using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{

    [SerializeField]
    List<Transform> milestones;

    int index = 0;
    int direction = 0;

    float timeInIdle;

    [SerializeField][Range(0, 10)]int moveSpeed;
    [SerializeField][Range(0, 10)]int chaseSpeed;
    [SerializeField][Range(0, 10)]int rotateSpeed;

    Transform target;

    enum state : int
    {
        Idle,
        Walk,
        Chase,
        Return
    }
    int currentState;

    void Start()
    {
        index = 0;
        direction = 0;

        currentState = (int)state.Idle;
    }

    void Update()
    {
        switch (currentState)
        {
            case 0: /* IDLE STATE */
                {
                    timeInIdle += Time.deltaTime;

                    if (timeInIdle > 2f)
                        currentState = (int)state.Walk;
                }
                break;

            case 1: /* WALK STATE */
                {
                    Vector3 _direction = (new Vector3(milestones[index].position.x, 0f, milestones[index].position.z) - new Vector3(transform.position.x, 0f, transform.position.z)).normalized;
                    Quaternion _lookRotation = Quaternion.LookRotation(_direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotateSpeed);

                    transform.position = Vector3.MoveTowards(transform.position, milestones[index].position, moveSpeed * Time.deltaTime);

                    if (transform.position == milestones[index].position)
                    {
                        if (direction == 0)
                            index++;
                        else
                            index--;

                        if (index == milestones.Count && direction == 0)
                        {
                            direction = 1;
                            index--;
                        }
                        if (index == -1 && direction == 1)
                        {
                            direction = 0;
                            index++;
                        }

                        currentState = (int)state.Idle;
                        timeInIdle = 0;
                    }
                }
                break;

            case 2: /* CHASE STATE */
                {
                    Vector3 _direction = (new Vector3(target.position.x, 0f, target.position.z) - new Vector3(transform.position.x, 0f, transform.position.z)).normalized;
                    Quaternion _lookRotation = Quaternion.LookRotation(_direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotateSpeed);

                    transform.position = Vector3.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);
                }
                break;

            case 3: /* RETURN STATE */
                {

                }
                break;
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetState(int newState)
    {
        currentState = newState;
    }
}
