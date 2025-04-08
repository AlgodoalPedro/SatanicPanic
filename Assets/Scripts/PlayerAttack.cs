using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour{

    [SerializeField] private float attackCooldown;
    private float cooldownTimer = Mathf.Infinity;

    public Transform AttackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;
    private Animator anim;

    private void Awake()
    {   
        anim = GetComponent<Animator>();
    }

    void Update(){
        if(Input.GetKey(KeyCode.Space) && cooldownTimer > attackCooldown){
            Attack();
        }
        cooldownTimer += Time.deltaTime;
    }

    private void Attack(){
        anim.SetTrigger("attacking");
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(AttackPos.position, attackRange, whatIsEnemies);
        for(int i = 0; i < enemiesToDamage.Length; i++){
            //enemiesToDamage[i].GetComponent<MeleeEnemy>().TakeDamage(damage);
            enemiesToDamage[i].GetComponent<Health>().TakeDamage(damage);
        }
        cooldownTimer = 0;
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPos.position, attackRange);
    }
}