using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject ScoreText;
    public GameObject LifeText;
    public ParticleSystem explosion;
    public float respawnInv = 3.0f;
    public int lives = 3;
    public float respawnTime = 3f;
    public int score = 0;

    private void Awake() {
        ScoreText.GetComponent<TextMeshProUGUI>().text = this.score.ToString() + " $";
        LifeText.GetComponent<TextMeshProUGUI>().text = this.lives.ToString() + " ♥";
    }
    public void AsteroidDestroyed(Asteroid asteroid){
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();
        if(asteroid.size < 0.75f){
            this.score += 100;
        }else if(asteroid.size < 1.2f){
            this.score += 50;
        } else{
            this.score += 25;
        }

        ScoreText.GetComponent<TextMeshProUGUI>().text = this.score.ToString() + " $";
    }
    public void PlayerDied(){

        this.explosion.Play();
        this.explosion.transform.position = this.player.transform.position;

        this.lives--;       
        if(this.lives <= 0){
            GameOver();
        }else{
            Invoke(nameof(Respawn), this.respawnTime);
        }

        LifeText.GetComponent<TextMeshProUGUI>().text = this.lives.ToString() + " ♥";
    }

    private void Respawn(){
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collision");
        this.player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollision), respawnInv);
        
    }

    private void TurnOnCollision(){
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver(){
        this.lives = 3;
        this.score = 0;
        ScoreText.GetComponent<TextMeshProUGUI>().text = this.score.ToString() + " $";
        LifeText.GetComponent<TextMeshProUGUI>().text = this.lives.ToString() + " ♥";
    }
}
