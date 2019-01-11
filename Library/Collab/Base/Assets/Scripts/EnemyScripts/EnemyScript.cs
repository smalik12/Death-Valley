using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    Animator animator;
    public int animState;
    public int health = 100;
    private GameController gameManager;
    public GameObject healthDrop;
    public GameObject ammoDrop;
    private GameObject pickUpParent;

    private const int IDLE_STATE = 0;
    private const int WALKING_STATE = 1;
    private const int RUNNING_STATE = 2;
    private const int DYING_STATE = 3;
    private const int ATTACK_STATE = 4;

    Transform player;
    GameObject playerObject;
    //PlayerHealth playerHealth;
    UnityEngine.AI.NavMeshAgent nav;

    float timer;
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;
    bool playerInRange;
    bool killsUpdated;
    public float damangeTime = 2f;
    private float currTime = 0f;
    private Random r;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        animState = IDLE_STATE;
        r = new Random();

        gameManager = GameObject.Find("GameManager").GetComponent<GameController>();
        killsUpdated = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerObject = GameObject.FindGameObjectWithTag("MainCamera");
        //playerHealth = player.GetComponent<PlayerHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        pickUpParent = GameObject.Find("Pickups");

        //ammoDrop = GameObject.FindWithTag("AmmoBox");
        //healthDrop = GameObject.FindWithTag("Health");
    }


    // Update is called once per frame
    void Update () {
     

        //if (enemyHealth.health > 0 && playerHealth.currentHealth > 0) {
        if (health > 0) {
            if (nav.desiredVelocity.magnitude > 0 && nav.desiredVelocity.magnitude < 3 && !playerInRange) {
                animState = WALKING_STATE;
            } else if (nav.desiredVelocity.magnitude > 3 && !playerInRange) {
                animState = RUNNING_STATE;
            }

            CheckPlayerRange();

            timer += Time.deltaTime;

            if (timer >= timeBetweenAttacks && playerInRange) {
                animState = ATTACK_STATE;
                currTime += Time.deltaTime;
                if(currTime > damangeTime){
                    playerObject.GetComponent<PlayerHealth>().DealDamage(10f);
                    currTime = 0;
                }

            }else if(!playerInRange){
                nav.SetDestination(player.position);
            }
        } else {
            nav.enabled = false;

            if(!killsUpdated){
                killsUpdated = true;
                gameManager.updateKill();
            }

            if(animState != DYING_STATE){
                DropItem();
                animState = DYING_STATE;
                animator.SetTrigger("Death");
                Destroy(this.gameObject,5);

            }

            return;
        }

        animator.SetInteger("State", animState);
    }

    public void takeDamage() {
        if (health > 0) {
            health -= 10;
        }
    }

    public string printHealth() {
        return health.ToString();
    }

    private void Attack() {
        animator.SetBool("Attack", true);
    }

    //void onTriggerEnter(Collider other) {
    //    if (other.gameObject == player) {
    //        playerInRange = true;
    //    }
    //}

    //void onTriggerExit(Collider other) {
    //    if (other.gameObject == player) {
    //        playerInRange = false;
    //    }
    //}

    void CheckPlayerRange(){
        if((player.transform.position - this.transform.position).sqrMagnitude < (3*3)){
            playerInRange = true;
        }else{
            playerInRange = false;
        }
    }

    private void DropItem() {
        int rand = Random.Range(0,4)+1;
        if(true){
            rand = Random.Range(0, 2) + 1;
            if(rand == 1){
                Instantiate(ammoDrop, this.transform.position, this.transform.rotation, pickUpParent.transform);
            }else{
                Instantiate(healthDrop, this.transform.position, this.transform.rotation, pickUpParent.transform);
            }
        }

    }
}
