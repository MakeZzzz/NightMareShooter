using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealthController playerHealthController;

    private Animator _anim;

    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerHealthController.CurrentHealth <= 0)
        {
            _anim.SetTrigger("GameOver");
        }
    }
}