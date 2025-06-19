using System.Collections;
using UnityEngine;

public class Peremeshenie_cuba : MonoBehaviour
{
    [Header("Настройки перемещения")]
    public float maxMoveDistance = 1.09f;
    public string[] cubeTags;
    public bool[] moveOnXAxis;     // true — движение по X, false — по Z

    [Header("Ссылки")]
    public MoveCounter moveCounter;
    public WinChecker winChecker;  // назначить через инспектор

    private Rigidbody[] rigidbodies;
    private bool[] isDragging;
    private Vector3[] startPositions;
    private Vector3[] initialMousePositions;
    private bool canMove;

    private bool gameplayStarted = false; // <--- флаг для старта геймплея

    void Start()
    {
        int numCubes = cubeTags.Length;
        if (numCubes != moveOnXAxis.Length || numCubes == 0)
        {
            Debug.LogError("Несоответствие размеров массивов cubeTags и moveOnXAxis или они пусты!");
            enabled = false;
            return;
        }

        rigidbodies = new Rigidbody[numCubes];
        isDragging = new bool[numCubes];
        startPositions = new Vector3[numCubes];
        initialMousePositions = new Vector3[numCubes];

        for (int i = 0; i < numCubes; i++)
        {
            GameObject cube = GameObject.FindGameObjectWithTag(cubeTags[i]);
            if (cube == null)
            {
                Debug.LogError($"Не найден куб с тэгом '{cubeTags[i]}'!");
                enabled = false;
                return;
            }
            rigidbodies[i] = cube.GetComponent<Rigidbody>();
            if (rigidbodies[i] == null)
            {
                Debug.LogError($"У объекта '{cubeTags[i]}' отсутствует Rigidbody!");
                enabled = false;
                return;
            }
        }
    }

    void Update()
    {
        if (!canMove)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                int cubeIndex = FindCubeIndex(hit.collider.gameObject);
                if (cubeIndex != -1)
                {
                    isDragging[cubeIndex] = true;
                    startPositions[cubeIndex] = rigidbodies[cubeIndex].position;
                    initialMousePositions[cubeIndex] = Input.mousePosition;

                    // <--- Вызов SignalGameplayStart только при первом клике по кубу
                    if (!gameplayStarted)
                    {
#if UNITY_WEBGL && !UNITY_EDITOR
                        var ygapi = FindObjectOfType<my>();
                        if (ygapi != null)
                            ygapi.SignalGameplayStart();
#endif
                        gameplayStarted = true;
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            for (int i = 0; i < isDragging.Length; i++)
            {
                if (isDragging[i])
                {
                    MoveCube(i);
                    isDragging[i] = false;
                }
            }
        }
    }

    void MoveCube(int cubeIndex)
    {
        float delta = moveOnXAxis[cubeIndex]
            ? Input.mousePosition.x - initialMousePositions[cubeIndex].x
            : Input.mousePosition.y - initialMousePositions[cubeIndex].y;

        Vector3 direction = moveOnXAxis[cubeIndex]
            ? Vector3.right * Mathf.Sign(delta) * maxMoveDistance
            : Vector3.forward * Mathf.Sign(delta) * maxMoveDistance;

        Vector3 targetPosition = startPositions[cubeIndex] + direction;

        // Ограничения для движения по X
        if (moveOnXAxis[cubeIndex])
        {
            // X ограничен, Z — только две линии
            targetPosition.x = Mathf.Clamp(targetPosition.x, -2.67f, 2.67f);
            targetPosition.z = Mathf.Abs(startPositions[cubeIndex].z) > 0.3f ? Mathf.Sign(startPositions[cubeIndex].z) * 0.54f : 0.54f * Mathf.Sign(direction.z);
        }
        // Ограничения для движения по Z
        else
        {
            // Z ограничен, X — только две линии
            targetPosition.z = Mathf.Clamp(targetPosition.z, -2.72f, 2.72f);
            targetPosition.x = Mathf.Abs(startPositions[cubeIndex].x) > 0.3f ? Mathf.Sign(startPositions[cubeIndex].x) * 0.53f : 0.53f * Mathf.Sign(direction.x);
        }

        // Перемещаем куб
        rigidbodies[cubeIndex].MovePosition(targetPosition);

        moveCounter.DecMove();
        if (moveCounter.moves == 0)
        {
            SetMoveEnabled(false);
            StartCoroutine(CheckEndGame());
        }

        AudioManager.Instance.PlayCubeClick();
    }

    private IEnumerator CheckEndGame()
    {
        // ждём следующего физического шага, чтобы успели отработать OnTriggerEnter для последнего куба
        yield return new WaitForFixedUpdate();

        // проверяем, все ли триггеры заняты
        bool allOccupied = true;
        foreach (var trig in winChecker.triggers)
        {
            if (!trig.isOccupied)
            {
                allOccupied = false;
                break;
            }
        }

        UIManager.Instance.OnGameFinished(allOccupied);
    }

    public void SetMoveEnabled(bool moveEnabled)
    {
        canMove = moveEnabled;
    }

    private int FindCubeIndex(GameObject obj)
    {
        for (int i = 0; i < cubeTags.Length; i++)
        {
            if (obj.CompareTag(cubeTags[i]))
                return i;
        }
        return -1;
    }
}
