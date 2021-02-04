using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject currentTarget;
    [SerializeField] private float runSpeed = 1f;

    private Animator animator;

    public int KilledEnemy { get; set; } = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        Debug.Log("OnDestroy");
        SummonButton summonButton = FindObjectOfType<SummonButton>();
        if (summonButton != null)
        {
            summonButton.EnemyKillded();
        }
    }
 
    void Update()
    {
        transform.Translate(Vector2.left * runSpeed * Time.deltaTime);
        UpdateAnimationState();
    }


    private void UpdateAnimationState()
    {
        if (!currentTarget)
        {
            animator.SetBool("Attack", false);
            runSpeed = 1f;
        }
    }

    private void Attack(GameObject target)
    {
        animator.SetBool("Attack", true);
        runSpeed = 0f;

        currentTarget = target;
    }

    // using from animation event
    public void StrikeCurrentTarget(int damage)
    {
        if (!currentTarget){ return; }
        Health health = currentTarget.GetComponent<Health>();
        if (health)
        {
            health.DealDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        GameObject otherObject = otherCollider.gameObject;

        // ignore if is enemy
        if (otherObject.GetComponent<Enemy>()) { return; }

        Attack(otherObject);
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        GameObject otherObject = otherCollider.gameObject;

        if (otherObject.GetComponent<Enemy>()) { return; }

        currentTarget = null;    
    }
}
