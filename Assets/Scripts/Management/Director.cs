using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Director : Singleton<Director> {
    public GameObject _Dynamic;
    public GameObject playerObject;
    [HideInInspector] public PauseObservable O_pause = new PauseObservable();
    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;
    [HideInInspector] public string sceneTransitionPlayerTarget;
    [HideInInspector] public string sceneTransitionPlayerEffect;
    [HideInInspector] public string sceneTransitionTarget;
    public UIManager ui;
    public Vector2 respawnPosition;

    private bool isPaused;
    private bool isRespawning;
    private float sceneTransitionTimeout = 0.5f;
    private string testString;

    void Start() {
        Scene scene = SceneManager.GetActiveScene();
        LoadDirectorProps();
    }

    void Update() {
        if (Input.GetButtonDown(GameInput.PAUSE_BUTTON)) {
            PauseGame();
        }
    }

    public void DamagePlayer(int damage, bool knockout) {
        playerHealth.DamagePlayer(damage);
        playerHealth.KnockoutPlayer();
        if (knockout && !isRespawning) {
            StartCoroutine(RespawnPlayer(1));
        }
    }

    public void GoToScene(string name, string targetObject = "JUSTANAVERAGEOBJECT", string entranceMode = "") {
        ui.FadeOut();
        StartCoroutine(TransitionPlayer(name, targetObject, entranceMode));
    }

    public void LoadDirectorProps() {
        _Dynamic = GameObject.Find("_Dynamic");
        playerObject = GameObject.Find("Player");
        if (playerObject) {
            playerHealth = playerObject.GetComponent<PlayerHealth>();
            playerMovement = playerObject.GetComponent<PlayerMovement>();
        }
        GameObject guiObject = GameObject.Find("GUI");
        if (guiObject) {
            ui = guiObject.GetComponent<UIManager>();
        }
    }

    public void PauseGame() {
        isPaused = !isPaused;
        O_pause.Next(true);

        if (isPaused) {
            ui.ShowPauseMenu();
        } else {
            ui.HidePauseMenu();
        }
    }

    public void SaveGame(bool playAnimation = false) {
        Persistence.Instance.FetchPersistableData();
        Persistence.Instance.PersistToDisk();
    }

    public void SetString() {
        testString = "Some test value";
        SceneManager.LoadScene("_test_room");
    }

    private IEnumerator RespawnPlayer(float timeout) {
        isRespawning = true;
        float respawnCountdown = timeout;

        while (respawnCountdown > 0) {
            respawnCountdown -= Time.deltaTime;
            yield return null;
        }

        if (respawnPosition != null) {
            playerMovement.MovePlayer(respawnPosition);
        }
        isRespawning = false;
    }

    private IEnumerator TransitionPlayer(string name, string targetObject, string entranceMode) {
        float time = 0f;
        while (time < sceneTransitionTimeout) {
            time += Time.deltaTime;
            yield return null;
        }
        sceneTransitionTarget = name;
        sceneTransitionPlayerTarget = targetObject;
        sceneTransitionPlayerEffect = entranceMode != null && entranceMode != "" ? entranceMode : null;
        SceneManager.LoadScene(name);
    }
}
