using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpPowerCharger : MonoBehaviour {

    public Slider forceSlider;
    //public Slider timeSlider;
    public Image timeImage;
    public float initJumpTime = 10f;
    private bool charge = false;
    private const float MIN_FORCE = -1f;
    private const float MAX_FORCE = 1f;
    private float normalizedForce;
    private float normalizedTime;
    private float jumpTime;
    private const float MIN_JUMP_TIME = 3f;
    private float decresedJumpTime = 0f;

    private PlayerMovement player;


    public void DisplayCharger(PlayerMovement player)
    {
        gameObject.SetActive(true);
        jumpTime = initJumpTime- decresedJumpTime;
        charge = true;
        this.player = player;
        Score.SCORE = -(Score.SCORE);
        Score.SCOREVELOCITY = Score.SCOREVELOCITY * 2;

    }

    private void Update()
    {
        if (charge)
        {
            SetForceSlider();
            SetTimeSlider();
            if (Input.GetMouseButtonUp(0))
            {
                StopCharge();

            }

        }
    }

    private void SetTimeSlider()
    {
        jumpTime -= Time.deltaTime;
        normalizedTime = (jumpTime) / (initJumpTime-decresedJumpTime);
        normalizedTime = Mathf.Clamp(normalizedTime, 0f, 1f);
        timeImage.transform.localScale = new Vector3(timeImage.transform.localScale.x,normalizedTime);
        //timeSlider.value = normalizedTime;
        if (normalizedTime <= 0.005f)
        {
            StopCharge();
        }

    }

    private void SetForceSlider()
    {
        float force = Mathf.Sin(Time.time);
        normalizedForce = (force - MIN_FORCE) / (MAX_FORCE  - MIN_FORCE);
        normalizedForce = Mathf.Clamp(normalizedForce, 0f, 1f);
        forceSlider.value = normalizedForce;

    }

    private void StopCharge()
    {
        if (initJumpTime-decresedJumpTime >= MIN_JUMP_TIME)
        {
            decresedJumpTime += 1f;
        }
        charge = false;
        gameObject.SetActive(false);
        player.Jump(normalizedForce);
        Score.SCORE = -(Score.SCORE);
        Score.SCOREVELOCITY = Score.SCOREVELOCITY / 2;

    }

    public void ResetDecreasedTime()
    {
        decresedJumpTime = 0f;
        if (charge)
        {
                    StopCharge();

        }
    }

    public bool GetCharge()
    {
        return charge;
    }

}
