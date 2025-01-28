using UnityEngine;
using System;

namespace Movement
{
    public class IdleState : State
    {
        
        private Rigidbody2D rb;
        private PlayerAnim playerAnim;

        public IdleState(Rigidbody2D rb)
        {
            this.rb = rb;
        }

        public override void OnEnter(PlayerAnim playerAnim)
        {
            this.playerAnim = playerAnim;
            rb.linearVelocity = Vector2.zero; // Stop movement
            playerAnim.Stand();
        }

        public override State OnUpdate(InputHandler inputHandler)
        {
            if (inputHandler.JumpPressed && inputHandler.IsGrounded)
            {
                return new JumpState(rb, 50f); // Pass Rigidbody2D and jumpForce
            }
            if (inputHandler.MoveInput != Vector2.zero)
            {
                return new WalkState(rb, inputHandler.MoveInput, 2f, playerAnim); // Pass Rigidbody2D
            }

            return null; // Stay in IdleState
        }

        public override State OnUpdate(InputHandler inputHandler, EnemyHealth enemyHealth){ return null;}

        public override void OnExit()
        {
        }
    }

    public class SlideState : State
    {
        private Rigidbody2D rb;
        private Vector2 movement;
        private float moveSpeed;
        private PlayerAnim playerAnim;
        public SlideState(Rigidbody2D rb, Vector2 movement, float moveSpeed)
        {
            this.rb = rb;
            this.movement = movement;
            this.moveSpeed = moveSpeed;
        }
        public override void OnEnter(PlayerAnim playerAnim)
        {
            this.playerAnim = playerAnim;
            rb.AddForce(movement * moveSpeed, ForceMode2D.Impulse);
            playerAnim.Slide();
        }

        public override State OnUpdate(InputHandler inputHandler)
        {
            rb.AddForce(movement * moveSpeed, ForceMode2D.Impulse);
            if (inputHandler.MoveInput == Vector2.zero)
            {
                return new IdleState(rb);
            }
            if (inputHandler.MoveInput != Vector2.zero && !inputHandler.SlidePressed)
            {
                return new WalkState(rb, inputHandler.MoveInput, 5f, playerAnim);
            }
            return null; // Replace with state transition logic
        }

        public override State OnUpdate(InputHandler inputHandler, EnemyHealth enemyHealth){ return null;}

        public override void OnExit()
        {
        }
    }

    public class WalkState : State
    {
        private Rigidbody2D rb;
        private Vector2 movement;
        private float moveSpeed;
        private PlayerAnim playerAnim;
        public WalkState(Rigidbody2D rb, Vector2 movement, float moveSpeed,  PlayerAnim playerAnim)
        {
            this.rb = rb;
            this.movement = movement;
            this.moveSpeed = moveSpeed;
            this.playerAnim = playerAnim;
        }
        public override void OnEnter(PlayerAnim playerAnim)
        {
            rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocity.y);
            playerAnim.Walk();
            if (movement.x < 0)
            {
                playerAnim.Flip(true);
            }
            else
            {
                playerAnim.Flip(false);
            }
        }

        public override State OnUpdate(InputHandler inputHandler)
        {
             rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocity.y);
            if (inputHandler.MoveInput == Vector2.zero)
            {
                return new IdleState(rb);
            }
            else if (inputHandler.JumpPressed && inputHandler.IsGrounded)
            {
                return new JumpState(rb, 50f);
            }
/*             else if (gethurtstate)
            {
                return new GetHurtState();
            } */
            else if (inputHandler.SlidePressed)
            {
                return new SlideState(rb, inputHandler.MoveInput, 10f);
            }
            return null; // Replace with state transition logic
        }

        public override State OnUpdate(InputHandler inputHandler, EnemyHealth enemyHealth){ return null;}

        public override void OnExit()
        {
        }
    }
        public class JumpState : State
        {
            private Rigidbody2D rb;
            private float jumpForce;
            private PlayerAnim playerAnim;

            public JumpState(Rigidbody2D rb, float jumpForce)
            {
                this.rb = rb;
                this.jumpForce = jumpForce;
            }

            public override void OnEnter(PlayerAnim playerAnim)
            {
                this.playerAnim = playerAnim;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                playerAnim.Jump();
            }

            public override State OnUpdate(InputHandler inputHandler)
            {
                // Check for transitions (e.g., landing on the floor)
                
                if (inputHandler.MoveInput == Vector2.zero)
                {
                    return new IdleState(rb);
                }
                if (inputHandler.MoveInput != Vector2.zero)
                {
                    return new WalkState(rb, inputHandler.MoveInput, 5f, playerAnim);
                }

                return null; // Stay in JumpState
            }

            public override State OnUpdate(InputHandler inputHandler, EnemyHealth enemyHealth){ return null;}

            public override void OnExit()
            {
            }
        }

    public class GetHurtState : State
    {
        private Rigidbody2D rb;
        private float hitFactor;    
        public GetHurtState(Rigidbody2D rb, float hitFactor)
        {
            this.rb = rb;
            this.hitFactor = hitFactor;
        }
        public override void OnEnter(PlayerAnim playerAnim)
        {
            rb.AddForce(new Vector2(hitFactor, 1), ForceMode2D.Impulse); // hitfactor defines whether u were hit from right or left and how hard (knockback)
            GameManager.instance.GetPlayerStats().DecreaseHealth(5f);
            GameManager.instance.GetPlayerStats().DecreasePP(1f);
            playerAnim.GetHurt();
        }

        public override State OnUpdate(InputHandler inputHandler)
        {
            if (inputHandler.MoveInput == Vector2.zero)
            {
                return new IdleState(rb);
            }
            return null; // Replace with state transition logic
        }

        public override State OnUpdate(InputHandler inputHandler, EnemyHealth enemyHealth){ return null;}

        public override void OnExit()
        {
        }
    }
}