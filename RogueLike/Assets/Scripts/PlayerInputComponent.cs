using UnityEngine;
using System.Collections;

public class PlayerInputComponent : InputComponent {

    public override void ReadInputs(ActorComponent actor)
    {
        Rigidbody2D rigidBody = actor.GetComponent<Rigidbody2D>();

        // move
        float dx = 0;
        float dy = 0;
        if (Input.GetKey(KeyCode.W))
            dy += 1;
        if (Input.GetKey(KeyCode.A))
            dx -= 1;
        if (Input.GetKey(KeyCode.S))
            dy -= 1;
        if (Input.GetKey(KeyCode.D))
            dx += 1;
        actor.MovementIntent = (new Vector2(dx, dy)).normalized;

        // look
        actor.LookTowards = Input.mousePosition;

        // attack/act
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetMouseButtonDown(0))
            {
                actor.Attack();
            }
            else if (Input.GetMouseButtonDown(1))
            {

            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {

            }
            else if (Input.GetMouseButtonDown(1))
            {

            }
        }

    }
}
