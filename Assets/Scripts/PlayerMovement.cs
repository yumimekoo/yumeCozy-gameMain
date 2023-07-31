using System.Collections;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.Animations;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    [SerializeField] private float speed = 5.0f;
    private Vector3 input;

    private bool isDashing = false;
    [SerializeField] private bool canDash = true;
    [SerializeField] private float dashCD = 1.0f;
    [SerializeField] private float dashPower = 0.1f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float jumpHeight = 20;




    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        GatherInput();
        Look();
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GatherInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    void Look()
    {
        if (input != Vector3.zero && !isDashing)
        {
            var relative = (transform.position + input.ToIso()) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = rot;
        }
    }

    void Move()
    {
        if (input != Vector3.zero)
        {
            playerRb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        }
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;
        canDash = false;
        isDashing = true;
        while (Time.time < startTime + dashTime)
        {
            transform.Translate(Vector3.forward * dashPower);

            yield return null;
        }
        isDashing = false;
        yield return new WaitForSeconds(dashCD);
        canDash = true;

    }

    void Jump()
    {
        playerRb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
    }
}