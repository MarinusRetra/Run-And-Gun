using System;
using System.Collections;
using UnityEngine;

    [CreateAssetMenu(fileName = "AIController", menuName = "inputController/AIController")]
    public class AiController : ScriptableObject
    {
        public float JumpForce;
        public float MoveSpeed;
        [SerializeField] bool SwapPoint = false;
        private int currentMovePoint = 0;

        public float nextMovePointDistance = 0.2f; 
        //Dit is hoe dichtbij de AI moet zijn voordat er een nieuwe movepoint wordt geselecteerd

        /// <summary>
        /// Return true als de AI op een jump point staat
        /// </summary>
        /// <returns></returns>
        public bool ReturnJumpInput(Transform[] jumpPointsIn, Vector2 AIPos)
        {
            var nextJumpPoint = ReturnClosestJumpPoint(jumpPointsIn, AIPos);
            if (Vector2.Distance(AIPos, nextJumpPoint.transform.position) < nextMovePointDistance)
            {
               Debug.Log("jump");
               JumpForce = nextJumpPoint.GetComponent<JumPower>().JumpPower;
               return true;
            }
            return false;
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
        Transform ReturnClosestJumpPoint(Transform[] jumpPointsIn, Vector2 AIPos)
        {
            Transform closestPointTransform = null;
            float minDist = 1000f;
            foreach (Transform t in jumpPointsIn)
            {
                float dist = Vector3.Distance(t.position, AIPos);// kan later een andere manier verzinnen dan .Distance
                if (dist < minDist)
                {
                   closestPointTransform = t;
                   minDist = dist;
                }
            }
            return closestPointTransform;
        }
}