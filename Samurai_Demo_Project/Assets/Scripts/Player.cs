using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // Config
    [SerializeField] private float runSpeed = 1f;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask targetLayers;

    private bool isDie = false;
    private bool preventMultiClick = false;

    // Cached component references
    Animator myAnimator;
    Rigidbody2D myRigidbody;
    Health health;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
    }

    void Update()
    {
        isDie = health.isDie();
        if (!isDie) { return; }
        Run();
        FlipSprite();
        if (CrossPlatformInputManager.GetButtonDown("Fire1") && !preventMultiClick)
        {
            preventMultiClick = true;
            Attack();
            StartCoroutine(WaitForSeconds());
        }    
    }

    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(1);
        preventMultiClick = false;
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // value is between -1 to 1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Run", playerHasHorizontalSpeed);
    }

    private void Attack()
    {
        // play an attack animation
        myAnimator.SetTrigger("Attack");

        // detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, targetLayers);

        // damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit" + enemy.name);
            enemy.GetComponent<Health>().DealDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null) { return; }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void FlipSprite()
    {
        // if the player is moving horizontally
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            // reverse the cyrrent scaling of x axis
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
}
