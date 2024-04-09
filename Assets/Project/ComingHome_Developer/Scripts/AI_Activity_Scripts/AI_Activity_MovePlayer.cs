using UnityEngine;

public class AI_Activity_MovePlayer : MonoBehaviour
{
    public static AI_Activity_MovePlayer instance;

    public int colSize, rowSize;
    public Vector2 origin;
    public Transform aiBoard;
    public float cellSize = 1.0f;
    public float speed = 2;
    public GameObject highligter;

    internal Transform[,] aiBoardPosMatrix;
    internal int playerStep=5;
    internal bool isPlayerMoving;
    internal bool isRightRotate;
    internal bool isLeftRotate;

    int rowIndex, colIndex;
    int rowLength, colLength;
    int rowPosition, colPosition;
    int rowRandom, columnRandom;

    Transform targetPosition;

    Transform playerTransform;
    Vector3 playerForward ;
    Vector3 playerRight;

    int stepsToMove;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        rowIndex = 0;
        colIndex = 0;
        aiBoardPosMatrix = new Transform[rowSize,colSize];

        for (int i = 0; i < rowSize; i++)
        {
            for (int j = 0; j < colSize; j++)
            {
                GameObject obj = new GameObject();
                obj.name = "(" + i + "," + j + ")";
                obj.transform.SetParent(aiBoard);
                aiBoardPosMatrix[i, j] = obj.transform;
                aiBoardPosMatrix[i, j].localPosition = new Vector3(origin.x+j , 0 , origin.y+i);
            }
        }
        rowLength = aiBoardPosMatrix.GetLength(0);
        colLength = aiBoardPosMatrix.GetLength(1);
        RandomRangeTrigger();
    }

    public void InitDirection()
    {
        playerTransform = transform;
        // Get the forward and right directions of the player
        playerForward = playerTransform.forward;
        playerRight = playerTransform.right;
    }

    private void Update()
    {
        if (isPlayerMoving)
        {
            AI_Activity_UIHandler.instance.BtnInteractable(false);
            transform.position = Vector3.MoveTowards(transform.position , targetPosition.position , speed * Time.deltaTime);
            if(transform.position  == targetPosition.position)
            {
                isPlayerMoving = false;
                AI_Activity_UIHandler.instance.jammoPlayer.GetComponent<Animator>().SetFloat("Blend", 0);
                AI_Activity_UIHandler.instance.BtnInteractable(true);
                AI_Activity_UIHandler.instance.stepCount += stepsToMove;
                AI_Activity_UIHandler.instance.countText.text = AI_Activity_UIHandler.instance.stepCount.ToString();
                Debug.Log("DistanceBetween" + aiBoardPosMatrix[rowIndex,colIndex]);
                rowPosition = rowIndex;
                colPosition = colIndex;
                Debug.Log("Positions_1 " + rowPosition + " " + colPosition);
            }
        }
       else if(isRightRotate)
        {       
            PlayerRightRotate(45f);
        }
        else if(isLeftRotate)
        {
            PlayerLeftRotate(-45f);
        }
    }

    public void PlayerMove(int step)
    {
        playerStep = step;
        PlayerMove();
    }

    public void PlayerMove()
    {
        // Check if the player is looking at the forward direction
        if (IsLookingAtDirection(playerForward))
        {
            if ((rowIndex + playerStep) >= rowLength)
            {
                if(rowIndex!=rowLength-1)
                {
                    stepsToMove = rowLength - rowIndex-1;
                    rowIndex = rowLength - 1;
                    targetPosition = aiBoardPosMatrix[rowIndex, colIndex];
                    AI_Activity_UIHandler.instance.jammoPlayer.GetComponent<Animator>().SetFloat("Blend", 0.5f);
                    isPlayerMoving = true;
                }
                else
                {
                    isPlayerMoving = false;
                }
            }
            else      /// when block avilable to move
            {
                rowIndex += playerStep;
                targetPosition = aiBoardPosMatrix[rowIndex, colIndex];
                AI_Activity_UIHandler.instance.jammoPlayer.GetComponent<Animator>().SetFloat("Blend", 0.5f);
                stepsToMove = playerStep;
                isPlayerMoving = true;
            }
        }
        else 
        // Check if the player is looking at the backward direction
        if (IsLookingAtDirection(-playerForward))
        {
            if ((rowIndex - playerStep) < 0)
            {
                if (rowIndex != 0)
                {
                    stepsToMove = rowIndex;
                    rowIndex = 0;
                    targetPosition = aiBoardPosMatrix[rowIndex, colIndex];
                    AI_Activity_UIHandler.instance.jammoPlayer.GetComponent<Animator>().SetFloat("Blend", 0.5f);
                    isPlayerMoving = true;
                }
                else
                {
                    isPlayerMoving = false;
                }
            }
            else   /// when block avilable to move
            {
                rowIndex -= playerStep;
                targetPosition = aiBoardPosMatrix[rowIndex, colIndex];
                isPlayerMoving = true;
                stepsToMove = playerStep;
                AI_Activity_UIHandler.instance.jammoPlayer.GetComponent<Animator>().SetFloat("Blend", 0.5f);
            }
        }
        else
        // Check if the player is looking at the right direction
        if (IsLookingAtDirection(playerRight))
        {
            Debug.Log("Player is looking right.");
            if ((colIndex + playerStep) >= colLength)
            {
                    if (colIndex != colLength - 1)
                    {
                        stepsToMove = colLength - colIndex - 1;
                        colIndex = colLength - 1;
                        targetPosition = aiBoardPosMatrix[rowIndex, colIndex];
                        AI_Activity_UIHandler.instance.jammoPlayer.GetComponent<Animator>().SetFloat("Blend", 0.5f);
                        Debug.Log("case 0 ");
                        isPlayerMoving = true;
                    }
                    else
                    {
                        isPlayerMoving = false;

                    }
            }
            else
            {
                colIndex += playerStep;
                targetPosition = aiBoardPosMatrix[rowIndex, colIndex];
                isPlayerMoving = true;
                stepsToMove = playerStep;
                AI_Activity_UIHandler.instance.jammoPlayer.GetComponent<Animator>().SetFloat("Blend", 0.5f);
                Debug.Log("Moving++90");
            }
        }
        else
        // Check if the player is looking at the left direction
        if (IsLookingAtDirection(-playerRight))
        {
            if ((colIndex - playerStep) < 0)
            {
                if (colIndex != 0)
                {
                    stepsToMove = colIndex;
                    colIndex = 0;
                    targetPosition = aiBoardPosMatrix[rowIndex, colIndex];
                    AI_Activity_UIHandler.instance.jammoPlayer.GetComponent<Animator>().SetFloat("Blend", 0.5f);
                    Debug.Log("case 0 ");
                    isPlayerMoving = true;
                }
                else
                {
                    isPlayerMoving = false;
                }
            }
            else
            {
                colIndex -= playerStep;
                targetPosition = aiBoardPosMatrix[rowIndex, colIndex];
                isPlayerMoving = true;
                stepsToMove = playerStep;
                AI_Activity_UIHandler.instance.jammoPlayer.GetComponent<Animator>().SetFloat("Blend", 0.5f);
                Debug.Log("Moving-90");
            }
            Debug.Log("Player is looking left.");
        }
        else
        {
            if (Mathf.Abs(transform.localEulerAngles.y - 45f) <= 0.1f || (Mathf.Abs(transform.localEulerAngles.y - 45f) % 360 == 0))
            {
                Debug.Log("Player step_0");

                if ((rowIndex + playerStep) < rowLength && (colIndex + playerStep) < colLength)
                {
                    Debug.Log("Player step_1");
                    rowIndex += playerStep;
                    colIndex += playerStep;
                    targetPosition = aiBoardPosMatrix[rowIndex, colIndex];
                    AI_Activity_UIHandler.instance.jammoPlayer.GetComponent<Animator>().SetFloat("Blend", 0.5f);
                    isPlayerMoving = true;
                    stepsToMove = playerStep;
                }
                else
                {
                    Debug.Log("Player step_3");
                    if (rowIndex != rowLength-1 && colIndex != colLength-1 )
                    {
                        Debug.Log("Player step_4");
                        int Row = rowLength - rowIndex - 1;
                        int Column = colLength - colIndex - 1;

                        stepsToMove = (Row < Column) ? Row : Column;

                        if ((rowIndex + playerStep) >= rowLength) rowIndex = rowLength - 1; else rowIndex += playerStep;
                        if ((colIndex + playerStep) >= colLength) colIndex = colLength - 1; else colIndex += playerStep;
                        targetPosition = aiBoardPosMatrix[rowIndex, colIndex];
                        AI_Activity_UIHandler.instance.jammoPlayer.GetComponent<Animator>().SetFloat("Blend", 0.5f);
                        isPlayerMoving = true;
                    }
                    else
                    {
                        isPlayerMoving = false;
                    }
                }
            }
            else if (Mathf.Abs(transform.localEulerAngles.y - 135) <= 0.1f || (Mathf.Abs(transform.localEulerAngles.y - 135f) % 360 == 0))
            {
                Debug.Log("135" + rowIndex + colIndex + playerStep);
                if ((rowIndex - playerStep) >= 0 && (colIndex + playerStep) < colLength)
                {
                    rowIndex -= playerStep;
                    colIndex += playerStep;
                    targetPosition = aiBoardPosMatrix[rowIndex, colIndex];
                    AI_Activity_UIHandler.instance.jammoPlayer.GetComponent<Animator>().SetFloat("Blend", 0.5f);
                    isPlayerMoving = true;
                    stepsToMove = playerStep;
                }
                else
                {
                   if((rowIndex != 0) && colIndex != colLength-1)
                    {
                        int Rows = rowIndex;
                        int Columns = colLength - colIndex - 1;
                        stepsToMove = (Rows < Columns) ? Rows : Columns;
                        if ((rowIndex - playerStep < 0)) rowIndex = 0; else rowIndex -= playerStep;
                        if ((colIndex + playerStep) >= colLength) colIndex = colLength - 1; else colIndex += playerStep;
                        targetPosition = aiBoardPosMatrix[rowIndex, colIndex];
                        AI_Activity_UIHandler.instance.jammoPlayer.GetComponent<Animator>().SetFloat("Blend", 0.5f);
                        isPlayerMoving = true;
                    }
                    else
                    {
                        isPlayerMoving = false;
                    }
                }
            }
            else if ((Mathf.Abs(transform.localEulerAngles.y + 135f) <= 0.1f) || (Mathf.Abs(transform.localEulerAngles.y + 135f) % 360==0))
            {
                Debug.Log("-135 " + rowIndex +" " + colIndex+" " + playerStep+" ");
                if ((rowIndex - playerStep) >= 0 && (colIndex - playerStep) >= 0)
                {
                    rowIndex -= playerStep;
                    colIndex -= playerStep;
                    targetPosition = aiBoardPosMatrix[rowIndex, colIndex];
                    AI_Activity_UIHandler.instance.jammoPlayer.GetComponent<Animator>().SetFloat("Blend", 0.5f);
                    stepsToMove = playerStep;
                    isPlayerMoving = true;
                }
                else
                {
                    if((rowIndex != 0) && (colIndex != 0))
                    {
                        int rows = rowIndex;
                        int columns = colIndex;
                        stepsToMove = (rows < columns) ? rows : columns;
                        if ((rowIndex - playerStep < 0)) rowIndex = 0; else rowIndex -= playerStep;
                        if ((colIndex - playerStep < 0)) colIndex = 0; else colIndex -= playerStep;
                        targetPosition = aiBoardPosMatrix[rowIndex, colIndex];
                        AI_Activity_UIHandler.instance.jammoPlayer.GetComponent<Animator>().SetFloat("Blend", 0.5f);
                        isPlayerMoving = true;
                    }
                    else
                    {
                        isPlayerMoving = false;
                    }
                    Debug.Log("45 Degree_5");
                   
                }
            }
            else if (Mathf.Abs(transform.localEulerAngles.y + 45f) <= 0.1f || (Mathf.Abs(transform.localEulerAngles.y + 45f) % 360 == 0))
            {
                Debug.Log("-135 " + rowIndex + " " + colIndex + " " + playerStep + " ");
                if ((rowIndex + playerStep) < rowLength && (colIndex - playerStep) >= 0)
                {
                    rowIndex += playerStep;
                    colIndex -= playerStep;
                    targetPosition = aiBoardPosMatrix[rowIndex, colIndex];
                    AI_Activity_UIHandler.instance.jammoPlayer.GetComponent<Animator>().SetFloat("Blend", 0.5f);
                    isPlayerMoving = true;
                    stepsToMove = playerStep;
                }
                else
                {
                    if ((rowIndex != rowLength -1) && (colIndex != 0))
                    {
                        int rows = rowLength - rowIndex - 1;
                        int colums = colIndex;
                        stepsToMove = (rows < colums) ? rows : colums;
                        if ((rowIndex + playerStep >= rowLength)) rowIndex = rowLength - 1; else rowIndex += playerStep;
                        if ((colIndex - playerStep) < 0) colIndex = 0; else colIndex -= playerStep;
                        targetPosition = aiBoardPosMatrix[rowIndex, colIndex];
                        AI_Activity_UIHandler.instance.jammoPlayer.GetComponent<Animator>().SetFloat("Blend", 0.5f);
                        isPlayerMoving = true;
                    }
                    else
                    {
                        isPlayerMoving = false;
                    }
                }
            }
            Debug.Log("transform" + transform.localEulerAngles);
            Debug.Log("Calcualte " + Vector3.Angle(transform.localEulerAngles, new Vector3(0, +(135* Mathf.PI)/Mathf.PI, 0)));
        }
    }

    bool IsLookingAtDirection(Vector3 directionToCheck)
    {
        // Normalize the direction vectors
        Vector3 playerForward = transform.forward;
        Vector3 normalizedDirection = directionToCheck.normalized;
        // Use the dot product to check if the player is looking at the specified direction
        float dotProduct = Vector3.Dot(playerForward, normalizedDirection);
        // You can adjust this threshold based on your needs
        float threshold = 0.8f;
        return dotProduct > threshold;
    }
    bool IsLookingWithinRange(Vector3 referenceDirection, Vector3 directionToCheck, float angleRange)
    {
        // Normalize the direction vectors
        Vector3 normalizedReferenceDirection = referenceDirection.normalized;
        Vector3 normalizedDirection = directionToCheck.normalized;

        // Use the dot product to check if the player is looking within the specified angle range
        float dotProduct = Vector3.Dot(normalizedReferenceDirection, normalizedDirection);
        float angle = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;

        // You can adjust the angle range based on your needs
        return angle <= angleRange;
    }

    public void PlayerRightRotate(float angle)
    {
        Debug.Log("Click right Btn");
        isRightRotate = false;
        if (IsLookingAtDirection(playerForward))
        {
            Debug.Log("PlayerForward");
            transform.eulerAngles = new Vector3(0, 45, 0);
        }
        else
        if (IsLookingAtDirection(playerRight))
        {
            Debug.Log("PlayerRight");
            //-45
            transform.eulerAngles = new Vector3(0, 135, 0);
        }
        else
        if (IsLookingAtDirection(-playerForward))
        {
            Debug.Log("-----PlayerForward");
            transform.eulerAngles = new Vector3(0, -135, 0);
        }
        else
        if (IsLookingAtDirection(-playerRight))
        {
            Debug.Log("-----PlayerRight");
            //-45
            transform.eulerAngles = new Vector3(0, -45, 0);
        }
        else
        {
            Debug.Log("New Angle");
            float newAngle = transform.eulerAngles.y + angle;
            transform.eulerAngles = new Vector3(0, newAngle, 0);
        }
    }

    public void PlayerLeftRotate(float angle)
    {
        isLeftRotate = false;
        Debug.Log("Click Left Btn");
        if (IsLookingAtDirection(playerForward))
        {
            Debug.Log("PlayerleftForward");
            //-45
            transform.eulerAngles = new Vector3(0, -45, 0);
        }
        else
        if (IsLookingAtDirection(playerRight))
        {
            Debug.Log("PlayerleftRight");
            //-45
            transform.eulerAngles = new Vector3(0, 45, 0);
        }
        else
        if (IsLookingAtDirection(-playerForward))
        {
            Debug.Log("----PlayerleftForward");
            transform.eulerAngles = new Vector3(0, 135, 0);
        }
        else
            if (IsLookingAtDirection(-playerRight))
        {
            //-45
            Debug.Log("----PlayerleftRight");
            transform.eulerAngles = new Vector3(0, -135, 0);
        }
        else
        {
            Debug.Log("New Angle on Right");
            float newAngle = transform.eulerAngles.y + angle;
            transform.eulerAngles = new Vector3(0, newAngle, 0);
        }

    }

    public void RandomRangeTrigger()
    {
        rowRandom = Random.Range(10,rowLength);
        columnRandom = Random.Range(10, colLength);
        highligter.transform.position = aiBoardPosMatrix[rowRandom, columnRandom].position;
        //highligter.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
         if(other.gameObject.transform.name.Equals(highligter.name))
        {
            Debug.Log("Completed");
            AI_Activity_UIHandler.instance.BtnOnOff(false);
            isPlayerMoving = false;
            AI_Activity_UIHandler.instance.jammoPlayer.GetComponent<Animator>().SetFloat("Blend", 0);
            AI_Activity_UIHandler.instance.wellDonePanel.SetActive(true);
            //AI_Activity_UIHandler.instance.celebrationEffect.SetActive(true);
            DistanceCalculateForHighlighter();
            AI_Activity_UIHandler.instance.XpEarned();
            AI_Activity_MultiDescription.instance.Set8Desc_Von();
        }
    }

    public void DistanceCalculateForHighlighter()
    {
        int rowValue = Mathf.Abs(rowRandom - rowPosition);
        int colValue = Mathf.Abs(columnRandom - colPosition);
       if (rowValue < colValue && rowValue != 0)
         {
            stepsToMove = rowValue;
         }
        else
        {
            if(colValue != 0)
            {
                stepsToMove = colValue;
            }
            else
            {
                stepsToMove = rowValue;
            }
        }
        AI_Activity_UIHandler.instance.stepCount += stepsToMove;
        AI_Activity_UIHandler.instance.countText.text = AI_Activity_UIHandler.instance.stepCount.ToString();
        AI_Activity_UIHandler.instance.stepTakenTxt.text = AI_Activity_UIHandler.instance.stepCount.ToString();
        
    }
}
