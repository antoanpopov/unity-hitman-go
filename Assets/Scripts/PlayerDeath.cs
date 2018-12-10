using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour {

    public Animator playerAnimatorController;

    public string playerDeathtrigger = "IsDead";

    public void Die() {
        if(playerAnimatorController != null) {
            playerAnimatorController.SetTrigger(playerDeathtrigger);
        }
    }
}
