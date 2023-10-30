using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject alejandro;
    [SerializeField] private GameObject frosty;

    private AlejandroPlayerController alejandroController;
    private FrostyPlayerController frostyController;
    private float frostyTotalTime = 20f;
    private float frostDownTime = 2f;

    public CharacterState currentCharacterState = CharacterState.Alejandro;

    private void Start()
    {
        alejandroController = alejandro.GetComponent<AlejandroPlayerController>();
        frostyController = frosty.GetComponentInChildren<FrostyPlayerController>();
    }
    public enum CharacterState
    {
        Frosty,
        Alejandro
    }

    public void FrostyCoin()
    {
        switch (currentCharacterState)
        {
            case CharacterState.Alejandro:
                Vector3 currentPositionA = alejandroController.GetPosition();
                float currentVelocityA = alejandroController.GetVelocity();
                alejandroController.OnDisable();
                alejandro.SetActive(false);
                frosty.SetActive(true);
                frostyController.TakeFlight();
                frostyController.SetVelocity(currentVelocityA);
                frostyController.SetWormPositions(currentPositionA);
                currentCharacterState = CharacterState.Frosty;
                StartCoroutine(EndOfFrosty());
                break;
            case CharacterState.Frosty:
                Vector3 currentPositionF = frostyController.GetPosition();
                float currentVelocityF = frostyController.GetVelocity();
                frosty.SetActive(false);
                alejandro.SetActive(true);
                alejandroController.OnEnable();
                alejandroController.SetVelocity(currentVelocityF);
                alejandro.transform.position = currentPositionF;
                currentCharacterState = CharacterState.Alejandro;
                break;
        }
    }

    IEnumerator EndOfFrosty()
    {
        Debug.Log("Frosty timer started");
        yield return new WaitForSeconds(frostyTotalTime);
        //Frosty moving back down
        frostyController.SinkDown();
        yield return new WaitForSeconds(frostDownTime);
        FrostyCoin();
        Debug.Log("Frosty time has ended");
    }
}
