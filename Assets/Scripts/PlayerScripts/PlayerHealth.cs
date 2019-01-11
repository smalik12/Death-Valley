using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public float currentHealth { get; set; }
    public float maxHealth { get; set; }
    public Slider healthBar;
    private GameController gameController;

    private void Start() {
        maxHealth = 100f;
        gameController = GameObject.Find("GameManager").GetComponent<GameController>();
        currentHealth = maxHealth;

        healthBar.value = CalculateHealth();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.X)){
            DealDamage(10);
        }
    }

    public void DealDamage(float damageValue){
        currentHealth -= damageValue;
        healthBar.value = CalculateHealth();
        if(currentHealth <= 0){
            //Die
        }
        if(currentHealth <= 0) {
            gameController.Died();
        }
    }

    float CalculateHealth(){
        return currentHealth / maxHealth;
    }

    public void AddHealth(int health) {
       if(currentHealth + health < maxHealth) {
            currentHealth += health;
        } else {
            currentHealth = maxHealth;
        }
        healthBar.value = CalculateHealth();
    }
}
