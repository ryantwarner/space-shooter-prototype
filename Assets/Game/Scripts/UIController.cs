using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public GameObject player;
    public GameObject enemyController;

    public Text shotsRemainingText;
    public Text enemiesRemainingText;
    public Text boostRemainingText;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        shotsRemainingText.text = player.GetComponent<ShmupController>().ShotsRemaining().ToString();
        enemiesRemainingText.text = enemyController.GetComponent<EnemyController>().EnemiesRemaining().ToString();
        boostRemainingText.text = player.GetComponent<ShmupController>().BoostRemaining().ToString();
    }
}
