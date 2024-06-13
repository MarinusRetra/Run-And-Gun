using System;
using Unity.VisualScripting;
using UnityEngine;

    [CreateAssetMenu(fileName = "AIController", menuName = "inputController/AIController")]
    public class AiController : ScriptableObject
    {
        public float JumpForce;
        public float MoveSpeed;
        bool SwapPoint = false;
        private int currentMovePoint = 0;
        private float nextMovePointDistance = 0.1f; //dit is hoe dichtbij de AI moet zijn voordat er een nieuwe movepoint wordt gebruikt
        
        public bool ReturnJumpInput()
        {
            return true;
        }

        public float ReturnMovementInput(GameObject[] movePointsIn, Vector2 AIPos)
        {
            // zet de currentMovePoint op 0 of 1 op basis van SwapPoint
            currentMovePoint = Convert.ToInt32(SwapPoint);

            float currentMovePointX = movePointsIn[currentMovePoint].transform.position.x;

            //toggled de swapoint elke keer wanneer de AI in nextMovePointDistance is
            if (Vector2.Distance(AIPos, movePointsIn[currentMovePoint].transform.position) < nextMovePointDistance)
            {
                SwapPoint = !SwapPoint;   
            }


            //return 1 of -1 op basis van de x coordinaat van currentMovePoint
            if (currentMovePointX < AIPos.x)
            {
                 return -1f;
            }
            if (currentMovePointX > AIPos.x)
            {
                 return 1f;
            }
            return 0f;
        }
    }