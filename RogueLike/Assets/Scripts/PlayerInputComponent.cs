using UnityEngine;
using System.Collections;

public class PlayerInputComponent : InputComponent {

    public override void ReadInputs(GameObject gameObject)
    {
        ActorComponent actor = gameObject.GetComponent<ActorComponent>();
        Rigidbody2D rigidBody = gameObject.GetComponent<Rigidbody2D>();

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
    }
}
