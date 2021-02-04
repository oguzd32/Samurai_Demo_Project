using UnityEngine;

public class Allie : MonoBehaviour
{
    [SerializeField] private GameObject currentTarget;
    [SerializeField] private float runSpeed = 1f;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * runSpeed * Time.deltaTime);
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
        if (!currentTarget) { return; }
        Health health = currentTarget.GetComponent<Health>();
        if (health)
        {
            health.DealDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        GameObject otherObject = otherCollider.gameObject;

        // ignore if is allie, bullet or player
        if (otherObject.GetComponent<Player>() || otherObject.GetComponent<Allie>() || otherObject.GetComponent<Bullet>()) { return; }

        Attack(otherObject);
    }
}
