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
    private string defaultScene = "meat_10";
    private string testString;

    void Awake() {
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
        ui.ShowDamageVignette();
        playerHealth.isImmune = true;
        StartCoroutine(RestorePlayerVulnerability(1));
        if (knockout && !isRespawning && playerHealth.health > 0) {
            StartCoroutine(RespawnPlayer(1));
        } else if (!knockout && !isRespawning) {
            StartCoroutine(HideDamageVignette(2));
        }
    }

    public void GoToScene(string name, string targetObject = "JUSTANAVERAGEOBJECT", string entranceMode = "") {
        Persistence.Instance.FetchPersistableData();
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

    public void LoadGame() {
        PlayerPersistenceData data = Persistence.Instance.ReadFromDisk<PlayerPersistenceData>(GamePlayerPrefs.PLAYER_DATA);
        if (data.scene == "" || data.scene == null) {
            Director.Instance.GoToScene(defaultScene);
        } else {
            Director.Instance.GoToScene(data.scene, data.portal, data.entranceMode);
        }
    }

    public void PauseGame() {
        isPaused = !isPaused;
        O_pause.Next(isPaused);

        if (isPaused) {
            ui.ShowPauseMenu();
        } else {
            ui.HidePauseMenu();
        }
    }

    public void SaveGame(bool playAnimation = false) {
        ui.SaveFade();
        playerHealth.RestoreHealth();
        Persistence.Instance.FetchPersistableData();
        Persistence.Instance.PersistToDisk();
    }

    public void StartGame() {
        SceneManager.LoadScene(defaultScene);
    }

    public void SetString() {
        testString = "Some test value";
        SceneManager.LoadScene("_test_room");
    }

    private IEnumerator HideDamageVignette(float timeout) {
        float respawnCountdown = timeout;

        while (respawnCountdown > 0) {
            respawnCountdown -= Time.deltaTime;
            yield return null;
        }

        ui.HideDamageVignette();
    }

    private IEnumerator RespawnPlayer(float timeout) {
        isRespawning = true;
        float respawnCountdown = timeout;
        ui.FadeOut();

        while (respawnCountdown > 0) {
            respawnCountdown -= Time.deltaTime;
            yield return null;
        }

        if (respawnPosition != null) {
            playerMovement.MovePlayer(respawnPosition);
        }
        ui.HideDamageVignette();
        ui.FadeIn();
        isRespawning = false;
    }

    private IEnumerator RestorePlayerVulnerability(float timeout) {
        float respawnCountdown = timeout;

        while (respawnCountdown > 0) {
            respawnCountdown -= Time.deltaTime;
            yield return null;
        }

        playerHealth.isImmune = false;
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
