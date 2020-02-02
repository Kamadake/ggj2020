using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Acid : AInteractiveObject
{
    public float effectDurationSeconds = 2f;
    public Transform spawnLocation;

    private float _startTime = 0f;
    public float _deathDuration = 2f;
    private float _deathTime = 0f;
    private GameObject _player;
    private Animator _pAnimator;

    public override void TriggerEnterAction(GameObject gameObject)
    {
        _startTime = Time.time;
        _player = gameObject;
        this.gameObject.GetComponent<AudioSource>().Play();
    }

    public override void TriggerExitAction(GameObject gameObject)
    {
        _startTime = 0f;
    }

    private void Update()
    {
        if (_startTime != 0f && (Time.time - _startTime) < effectDurationSeconds)
        {
            // character is suffering from acid effect
            Debug.Log("OUCH!");
        }
        else if (_startTime != 0f && (Time.time - _startTime) >= effectDurationSeconds)
        {
            // stop timer and spawn character at spawnLocation
            _startTime = 0f;
            _player.GetComponentInParent<Movement>().PlaySoundDeath();
            _pAnimator = _player.GetComponent<Animator>();
            _pAnimator.SetTrigger("death");
            _deathTime = Time.time;
            _player.GetComponent<Rigidbody2D>().isKinematic = true;
            // wait for animation clip time
        }

        if (_deathTime != 0f && (Time.time - _deathTime) >= _deathDuration)
        {
            _deathTime = 0f;
            _pAnimator.ResetTrigger("death");
            _player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            _player.transform.position = spawnLocation.position;
            _player.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }
}
