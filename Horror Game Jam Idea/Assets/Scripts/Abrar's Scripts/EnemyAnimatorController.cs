using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{

    [SerializeField] private Animator enemyAnimator;

    private string isWalking = "isWalking";
    private string isIdling = "isIdling";
    private string isRunning = "isRunning";

    // Start is called before the first frame update
    void Start()
    {
        //isWalkingHash = Animator.StringToHash("isWalking");
        //isIdling = Animator.StringToHash("isIdling");
        //isRunning = Animator.StringToHash("isRunningHash");
    }

    public void SetAnimatorToWalk()
    {
        Debug.Log("SET ISWALKING TO   TRUE");
        enemyAnimator.SetBool(isWalking, true);

        enemyAnimator.SetBool(isIdling, false);

        enemyAnimator.SetBool(isRunning, false);
    }

    public void SetAnimatorToIdle()
    {
        enemyAnimator.SetBool(isWalking, false);

        enemyAnimator.SetBool(isIdling, true);

        enemyAnimator.SetBool(isRunning, false);
    }

    public void SetAnimatorToRun()
    {
        enemyAnimator.SetBool(isWalking, false);

        enemyAnimator.SetBool(isIdling, false);

        enemyAnimator.SetBool(isRunning, true);
    }
}
