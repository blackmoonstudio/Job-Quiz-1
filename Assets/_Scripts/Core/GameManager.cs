using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] GameObject PlayerGO,GameStickUI;

    [SerializeField] GameObject[] CameraPos;
    [SerializeField] WASD_Movment wASD_Movment;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] CharacterController characterController;
    [SerializeField] GameStick_Movement gameStick_Movement;
    [SerializeField] Click_MoveMent click_MoveMent;
    public CameraChanager CamSW,CamSE,CamNE,CamNW,CamUP;
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] string[] Options;
    Transform plt;
    // Start is called before the first frame update
    void Awake()
    {
        plt = GameObject.Find("opp").transform;
        InitDropDown();
        
        
        CamSW.CallCameraChanger += HandleTriggerEntered;
        CamSE.CallCameraChanger += HandleTriggerEntered;
        CamNE.CallCameraChanger += HandleTriggerEntered;
        CamNW.CallCameraChanger += HandleTriggerEntered;
        CamUP.CallCameraChanger += HandleTriggerEntered;

        ActiveRequiredComponents(gameData);
    }

    async void ChangeCamera(int num)
    {
        await Task.Delay(1000);
        for (int i = 0; i < CameraPos.Length; i++)
        {
            CameraPos[i].SetActive(false);
        }
        CameraPos[num].SetActive(true);
    }

    void ActiveRequiredComponents(GameData gameData)
    {
        switch (gameData.GameView)
        {
            case GameData.GameViewType.WASD:
                wASD_Movment.enabled = true;
                navMeshAgent.enabled = false;
                characterController.enabled = true;
                gameStick_Movement.enabled = false;
                click_MoveMent.enabled = false;

                PlayerGO.transform.rotation = plt.transform.rotation;
                ChangeCamera(2);

                GameStickUI.SetActive(false);
                dropdown.value = 0;
                break;
            case GameData.GameViewType.Click:
                wASD_Movment.enabled = false;
                navMeshAgent.enabled = true;
                characterController.enabled = false;
                gameStick_Movement.enabled = false;
                click_MoveMent.enabled = true;

                ChangeCamera(1);

                GameStickUI.SetActive(false);
                dropdown.value = 1;
                break;
            case GameData.GameViewType.Stick:
                wASD_Movment.enabled = false;
                navMeshAgent.enabled = false;
                characterController.enabled = true;
                gameStick_Movement.enabled = true;
                click_MoveMent.enabled = false;

                ChangeCamera(0);

                GameStickUI.SetActive(true);
                dropdown.value = 2;
                break;
        }
    }
    

    private void OnEnable()
    {
        // Subscribe to the event
        if (CamSW)
        {
            CamSW.CallCameraChanger += HandleTriggerEntered;
            CamSE.CallCameraChanger += HandleTriggerEntered;
            CamNE.CallCameraChanger += HandleTriggerEntered;
            CamNW.CallCameraChanger += HandleTriggerEntered;
            CamUP.CallCameraChanger += HandleTriggerEntered;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from the event
        if (CamSW)
        {
            CamSW.CallCameraChanger -= HandleTriggerEntered;
            CamSE.CallCameraChanger -= HandleTriggerEntered;
            CamNE.CallCameraChanger -= HandleTriggerEntered;
            CamNW.CallCameraChanger -= HandleTriggerEntered;
            CamUP.CallCameraChanger -= HandleTriggerEntered;
        }
    }

    // This method will be called when the event is triggered
    private void HandleTriggerEntered(Collider other, string pos)
    {
        Debug.Log($"{pos} {other.gameObject.name}");

        if (gameData.GameView != GameData.GameViewType.Click)
            return;

        var cameraMappings = new Dictionary<string, int>
    {
        { "SW", 3 },
        { "SE", 4 },
        { "NW", 5 },
        { "NE", 6 },
        { "UP", 1 }
    };

        if (cameraMappings.TryGetValue(pos, out int cameraIndex))
        {
            ChangeCamera(cameraIndex);
        }
    }
    void InitDropDown()
    {
        dropdown.ClearOptions();
        List<string> op = new List<string>();
        for (int i = 0; i < Options.Length; i++)
        {
            op.Add(Options[i]);
        }
        dropdown.AddOptions(op);

        
    }
    public void ChangeDropDownValue()
    {
        var gameViewMappings = new Dictionary<int, GameData.GameViewType>
    {
        { 0, GameData.GameViewType.WASD },
        { 1, GameData.GameViewType.Click },
        { 2, GameData.GameViewType.Stick }
    };

        if (gameViewMappings.TryGetValue(dropdown.value, out GameData.GameViewType gameView))
        {
            gameData.GameView = gameView;
        }
        else
        {
            gameData.GameView = GameData.GameViewType.WASD;
        }

        ActiveRequiredComponents(gameData);
    }

    public void ResetPlayerPos()
    {
        PlayerGO.SetActive(false);
        PlayerGO.transform.position = plt.position;
        PlayerGO.transform.rotation = plt.rotation;
        PlayerGO.SetActive(true);
    }

}
