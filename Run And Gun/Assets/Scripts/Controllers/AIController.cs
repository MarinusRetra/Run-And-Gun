using UnityEngine;

    [CreateAssetMenu(fileName = "AIController", menuName = "inputController/AIController")]
    public class AiController : ScriptableObject
    {
        public float JumpForce;
        public float MoveSpeed;
        private int currentMovePoint = 0;
        private float savedPosition;
        private float nextMovePointDistance;

        public bool ReturnJumpInput()
        {
            return true;
        }

        public float ReturnMovementInput(GameObject[] movePointsIn, GameObject AI, Rigidbody2D rb, GameObject LastMovePoint)
        {
            // currentPointX is de point waar de ai op dit moment naartoe werkt
            float currentPointX = movePointsIn[currentMovePoint].transform.position.x;
            // ik heb het hier aangemaakt om alles iets leesbaarder te houden

            if (LastMovePoint != movePointsIn[currentMovePoint])
            { 
                savedPosition = currentPointX;
            }
            
            if (currentPointX > AI.transform.position.x)
            { 
                 return -1f;
            }
            if (currentPointX < AI.transform.position.x)
            {
                 return 1f;
            }
            return 0f;



    }
    }