using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabComponentsShortcut : MonoBehaviour
{
    public Rigidbody2D Rigidbody;
    public BoxCollider2D Collider;
    public SpriteRenderer SpriteRenderer;
    public Animator Animator;
    public Text NPCNameText;

    public bool AnimatorActionRun { get; private set; }
    public bool AnimatorActionOnAttack { get; private set; }
    public float AnimatorActionTime { get; private set; }

    private void Update()
    {
        if (Animator != null)
        {
            foreach (var i in Animator.parameters)
            {
                switch (i.name)
                {
                    case "ActionRun":
                        AnimatorActionRun = Animator.GetBool("ActionRun");
                        break;
                    case "ActionOnAttack":
                        AnimatorActionOnAttack = Animator.GetBool("ActionOnAttack");
                        break;
                    case "ActionTime":
                        AnimatorActionTime = Animator.GetFloat("ActionTime");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
