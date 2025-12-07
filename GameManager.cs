using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Round Settings")]
    public float prestartDelay = 2f;

    [Header("References")]
    public PlayerController player;
    public DoorScreenManager doorScreen;

    private Vector3 spawnPos;

    void Start()
    {
        spawnPos = player.transform.position;
        StartCoroutine(GameStartSequence());
    }

    private IEnumerator GameStartSequence()
    {
        player.SetCanMove(false);

        // la început → fade + sunete
        yield return doorScreen.PlayStartSequence();

        // apoi prestart delay
        yield return new WaitForSeconds(prestartDelay);

        player.ResetToIdle();
        player.transform.position = spawnPos;
        player.SetCanMove(true);
    }

    public void EndGame()
    {
        StartCoroutine(ExitSequence());
    }

    private IEnumerator ExitSequence()
    {
        player.SetCanMove(false);
        yield return doorScreen.PlayExitSequence();
    }

    public void RoundEnd()
    {
        StartCoroutine(RoundEndRoutine());
    }

    private IEnumerator RoundEndRoutine()
    {
        player.SetCanMove(false);
        yield return doorScreen.ShowRoundEndMenu(this);
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void ContinueRound()
    {
        StartCoroutine(ContinueRoutine());
    }

    private IEnumerator ContinueRoutine()
    {
        yield return doorScreen.PlayContinueSequence();
        yield return new WaitForSeconds(prestartDelay);

        player.ResetToIdle();
        player.transform.position = spawnPos;
        player.SetCanMove(true);
    }
}
