using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{

    private Vector2 startTouchPosition, endTouchPosition;

    private Vector3 currentPosition;

    [SerializeField] Transform raycastStart;
    [SerializeField] LayerMask wallLayerMask;
    public float moveSpeed;

    private Vector3 target;


    // Start is called before the first frame update
    void Start()
    {
       target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;

            Vector2 swipeDist = endTouchPosition - startTouchPosition;

            if (swipeDist.magnitude > 10)
            {
                if(Mathf.Abs(swipeDist.x) > Mathf.Abs(swipeDist.y))
                {
                    swipeDist.y = 0;
                }
                else
                {   
                    swipeDist.x = 0;
                }

                Vector3 moveDir = (new Vector3(swipeDist.x, 0f, swipeDist.y)).normalized;
                RaycastHit hit;
                if(Physics.Raycast(raycastStart.position, moveDir, out hit, Mathf.Infinity, wallLayerMask))
                {
                    target = hit.collider.transform.position - moveDir;
                    target.y = this.transform.position.y;
                }
            }
        }

        if(Vector3.Distance(transform.position, target) > 0.2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }
        else
        {
            //int xTarget = Mathf.FloorToInt(target.x);
            //int zTarget = Mathf.FloorToInt(target.z);
            //target = new Vector3(xTarget, target.y, zTarget);
            transform.position = target;
        }
    }

    public void SetTarget(Vector3 position)
    {
        target = position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, target);
    }

    //void MoveCharacter(Vector2 direction)
    //{
    //    Vector3 swipeDirection = new Vector3(direction.x, 0, direction.y).normalized;
    //    transform.position += swipeDirection * moveSpeed * Time.deltaTime;
    //}
}
