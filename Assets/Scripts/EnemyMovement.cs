using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent navMeshAgent;
    public AudioSource audioSource;
    private Animator anim;

    private bool hasPlayedAudio = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (EnemySpeedManager.SpeedLevel == EnemySpeed.Slow)
        {
            navMeshAgent.speed = 2f;
        }
        else if (EnemySpeedManager.SpeedLevel == EnemySpeed.Medium)
        {
            navMeshAgent.speed = 3.5f;
        }
        else if (EnemySpeedManager.SpeedLevel == EnemySpeed.Fast)
        {
            navMeshAgent.speed = 5f;
        }

        anim = GetComponentInChildren<Animator>();

        if (anim)
        {
            anim.SetFloat("speed_f", navMeshAgent.speed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {        
            navMeshAgent.SetDestination(player.position);
            hasPlayedAudio = false; // Reset the flag if player is found again
        }
        else if (!hasPlayedAudio)
        {
            hasPlayedAudio = true;
            
            AudioSource backgroundMusic = GameObject.Find("BackGround Music").GetComponent<AudioSource>();
            AudioSource deathSound = GameObject.Find("Death Sound").GetComponent<AudioSource>();
            AudioSource loseSound = GameObject.Find("Lose Sound").GetComponent<AudioSource>();

            if (backgroundMusic != null)
            {
                backgroundMusic.mute = true;
            }
            if (deathSound != null)
            {
                deathSound.Play();
            }
            if (loseSound != null)
            {
                loseSound.Play();
            }
        }
    }
}
