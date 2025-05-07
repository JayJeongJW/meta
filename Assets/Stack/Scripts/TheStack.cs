using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StackMiniGame;

namespace StackMiniGame
{

    public class TheStack : MonoBehaviour
    {
        private const float BoundSize = 3.5f;
        private const float MovingBoundSize = 3f;
        private const float StackMovingSpeed = 5.0f;
        private const float BlockMovingSpeed = 3.5f;
        private const float ErrorMargin = 0.1f;
        private const float BlockHeight = 1f;

        [SerializeField]
        private GameObject originBlock = null;

        private Vector3 prevBlockPostiion;
        private Vector3 desiredPosition;
        private Vector3 stackBounds = new Vector3(BoundSize, BoundSize, BoundSize);

        Transform lastBlock = null;
        float blockTransition = 0f;
        float secondaryPosition = 0f;

        int stackCount = 0;
        public int Score { get { return stackCount; } }

        int comboCount = 0;
        public int ComboCount { get { return comboCount; } set { comboCount = value; } }

        private int maxCombo = 0;
        public int MaxCombo { get => maxCombo; }

        public Color prevColor;
        public Color nextColor;

        bool isMovingX = true;
        bool isMoving = false;

        int bestScore = 0;
        public int BestScore { get => bestScore; }

        int bestCombo = 0;
        public int BestCombo { get => bestCombo; }

        private const string BestScoreKey = "BestScore";
        private const string BestComboKey = "BestCombo";

        private bool isGameOver = true;

        void Start()
        {
            if (originBlock == null)
            {
                Debug.Log("originBlock is null");
                return;
            }

            bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
            bestCombo = PlayerPrefs.GetInt(BestComboKey, 0);

            prevColor = GetRandomColor();
            nextColor = GetRandomColor();

            GameObject baseBlock = Instantiate(originBlock);
            Transform baseTrans = baseBlock.transform;
            baseTrans.parent = this.transform;
            baseTrans.localPosition = Vector3.zero;
            baseTrans.localRotation = Quaternion.identity;
            baseTrans.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);
            ColorChange(baseBlock);

            prevBlockPostiion = baseTrans.localPosition;
            Spawn_Block();
        }

        void Update()
        {
            if (isGameOver) return;

            MoveBlock();
            transform.position = Vector3.Lerp(transform.position, desiredPosition, StackMovingSpeed * Time.deltaTime);

            if (Input.GetMouseButtonDown(0))
            {
                if (PlaceBlock())
                {
                    Spawn_Block();
                }
                else
                {
                    Debug.Log("Game Over");
                    UpdateScore();
                    isGameOver = true;
                    GameOverEffect();
                    UIManager.Instance.SetScoreUI();
                }

            }
        }

        bool Spawn_Block()
        {
            GameObject newBlock = Instantiate(originBlock);
            if (newBlock == null)
            {
                Debug.Log("Newblock Instantiate Failed");
                return false;
            }

            Transform newTrans = newBlock.transform;
            newTrans.parent = this.transform;
            newTrans.localPosition = prevBlockPostiion + Vector3.up * BlockHeight;
            newTrans.localRotation = Quaternion.identity;
            newTrans.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);
            ColorChange(newBlock);

            lastBlock = newTrans;
            stackCount++;
            desiredPosition = Vector3.down * stackCount;
            blockTransition = 0f;
            isMovingX = !isMovingX;

            UIManager.Instance.UpdateScore();
            return true;
        }

        Color GetRandomColor()
        {
            float r = Random.Range(100f, 250f) / 255f;
            float g = Random.Range(100f, 250f) / 255f;
            float b = Random.Range(100f, 250f) / 255f;

            return new Color(r, g, b);
        }

        void ColorChange(GameObject go)
        {
            Color applyColor = Color.Lerp(prevColor, nextColor, (stackCount % 11) / 10f);
            Renderer rn = go.GetComponent<Renderer>();

            if (rn == null)
            {
                Debug.Log("Renderer is not null");
                return;
            }

            rn.material.color = applyColor;
            Camera.main.backgroundColor = applyColor - new Color(0.1f, 0.1f, 0.1f);

            if (applyColor.Equals(nextColor))
            {
                prevColor = nextColor;
                nextColor = GetRandomColor();
            }
        }

        void MoveBlock()
        {
            blockTransition += Time.deltaTime * BlockMovingSpeed;
            float movePostiion = Mathf.PingPong(blockTransition, BoundSize) - BoundSize / 2;
            float y = prevBlockPostiion.y + BlockHeight;

            if (isMovingX)
            {
                lastBlock.localPosition = new Vector3(movePostiion * MovingBoundSize, y, secondaryPosition);
            }
            else
            {
                lastBlock.localPosition = new Vector3(secondaryPosition, y, movePostiion * MovingBoundSize);
            }
        }

        bool PlaceBlock()
        {
            Vector3 lastPosition = lastBlock.localPosition;

            if (isMovingX)
            {
                float deltaX = Mathf.Abs(prevBlockPostiion.x - lastPosition.x);
                bool isNegativeNum = (deltaX < 0);

                if (deltaX > ErrorMargin)
                {
                    stackBounds.x -= deltaX;
                    if (stackBounds.x <= 0f) return false;

                    float middle = (prevBlockPostiion.x + lastPosition.x) / 2f;
                    lastBlock.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);
                    lastBlock.localPosition = new Vector3(middle, lastPosition.y, lastPosition.z);

                    CreateRubble(new Vector3(middle, lastPosition.y, lastPosition.z), new Vector3(stackBounds.x, 1, stackBounds.y));

                    float rubbleHalfScale = deltaX / 2f;
                    CreateRubble(
                        new Vector3(
                            isNegativeNum
                                ? lastPosition.x + stackBounds.x / 2 + rubbleHalfScale
                                : lastPosition.x - stackBounds.x / 2 - rubbleHalfScale,
                            lastPosition.y,
                            lastPosition.z),
                        new Vector3(deltaX, 1, stackBounds.y)
                    );

                    comboCount = 0;
                }
                else
                {
                    ComboCheck();
                    lastBlock.localPosition = prevBlockPostiion + Vector3.up * BlockHeight;
                }
            }
            else
            {
                float deltaZ = Mathf.Abs(prevBlockPostiion.z - lastPosition.z);
                bool isNegativeNum = (deltaZ < 0);

                if (deltaZ > ErrorMargin)
                {
                    stackBounds.y -= deltaZ;
                    if (stackBounds.y <= 0f) return false;

                    float middle = (prevBlockPostiion.z + lastPosition.z) / 2f;
                    lastBlock.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);
                    lastBlock.localPosition = new Vector3(lastPosition.x, lastPosition.y, middle);

                    float rubbleHalfScale = deltaZ / 2f;
                    CreateRubble(
                        new Vector3(
                            lastPosition.x,
                            lastPosition.y,
                            isNegativeNum
                                ? lastPosition.z + stackBounds.y / 2 + rubbleHalfScale
                                : lastPosition.z - stackBounds.y / 2 - rubbleHalfScale),
                        new Vector3(stackBounds.x, 1, deltaZ)
                    );

                    comboCount = 0;
                }
                else
                {
                    ComboCheck();
                    lastBlock.localPosition = prevBlockPostiion + Vector3.up * BlockHeight;
                }
            }

            prevBlockPostiion = lastBlock.localPosition;
            secondaryPosition = isMovingX ? lastBlock.localPosition.x : lastBlock.localPosition.z;

            return true;
        }

        void CreateRubble(Vector3 pos, Vector3 scale)
        {
            GameObject go = Instantiate(lastBlock.gameObject);
            go.transform.parent = this.transform;
            go.transform.localPosition = pos;
            go.transform.localScale = scale;
            go.transform.localRotation = Quaternion.identity;

            go.AddComponent<Rigidbody>();
            go.name = "Rubble";
        }

        void ComboCheck()
        {
            comboCount++;

            if (comboCount > maxCombo)
                maxCombo = comboCount;

            if (comboCount % 5 == 0)
            {
                Debug.Log("5 Combo Success!");
                stackBounds += new Vector3(0.5f, 0.5f);
                stackBounds.x = (stackBounds.x > BoundSize) ? BoundSize : stackBounds.x;
                stackBounds.y = (stackBounds.y > BoundSize) ? BoundSize : stackBounds.y;
            }
        }

        void UpdateScore()
        {
            if (bestScore < stackCount)
            {
                Debug.Log("최고 점수 갱신");
                bestScore = stackCount;
                bestCombo = maxCombo;

                PlayerPrefs.SetInt(BestScoreKey, bestScore);
                PlayerPrefs.SetInt(BestComboKey, bestCombo);
            }
        }

        void GameOverEffect()
        {
            int childCount = this.transform.childCount;

            for (int i = 1; i < 20; i++)
            {
                if (childCount < i) break;

                GameObject go = transform.GetChild(childCount - i).gameObject;

                if (go.name.Equals("Rubble")) continue;

                Rigidbody rigid = go.AddComponent<Rigidbody>();

                rigid.AddForce(
                    (Vector3.up * Random.Range(0, 10f) + Vector3.right * (Random.Range(0, 10f) - 5f))
                    );


            }
        }

        public void Restart()
        {
            int childCount = this.transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                Destroy(this.transform.GetChild(i).gameObject);
            }

            isGameOver = false;

            lastBlock = null;
            desiredPosition = Vector3.zero;
            stackBounds = new Vector3(BoundSize, BoundSize);

            stackCount = 0;
            isMovingX = true;
            blockTransition = 0f;
            secondaryPosition = 0f;

            comboCount = 0;
            maxCombo = 0;

            prevBlockPostiion = Vector3.zero;

            prevColor = GetRandomColor();
            nextColor = GetRandomColor();

            GameObject baseBlock = Instantiate(originBlock);
            Transform baseTrans = baseBlock.transform;
            baseTrans.parent = this.transform;
            baseTrans.localPosition = Vector3.zero;
            baseTrans.localRotation = Quaternion.identity;
            baseTrans.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);
            ColorChange(baseBlock);

            prevBlockPostiion = baseTrans.localPosition;
            Spawn_Block();
        }
    }
}