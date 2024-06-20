using System;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "AIController", menuName = "inputController/AIController")]
    public class AiController : ScriptableObject
    {
        public float JumpForce;
        public float MoveSpeed;
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

        public KeyValuePair<float,bool> ReturnMovementInput(GameObject[] movePointsIn, Vector2 AIPos, bool swapPointIn)
        {
        // zet de currentMovePoint op 0 of 1 op basis van SwapPoint
            currentMovePoint = Convert.ToInt32(swapPointIn);

            float currentMovePointX = movePointsIn[currentMovePoint].transform.position.x;

            //toggled swapPoint elke keer wanneer de AI in nextMovePointDistance is
            if (Vector2.Distance(AIPos, movePointsIn[currentMovePoint].transform.position) < nextMovePointDistance)
            {
                swapPointIn = !swapPointIn;
            }

            //return 1 of -1 op basis van de x coordinaat van currentMovePoint
            if (currentMovePointX < AIPos.x)
            {
                return new(-1,swapPointIn);
            }
            if (currentMovePointX > AIPos.x)
            {
                return new(1,swapPointIn);
            }
            return new(0f,swapPointIn);
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