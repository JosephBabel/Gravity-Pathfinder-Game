using UnityEngine;

namespace Utility
{
    public static class MovementUtil
    {
        /// <summary>
        /// Checks if this collider is sitting on an edge.
        /// </summary>
        /// <param name="entity">Collider for edge detection.</param>
        /// <returns>Returns true if this collider is sitting over an edge.</returns>
        public static bool CheckOnEdge(Collider entity)
        {
            RaycastHit hitInfo;
            Rigidbody rb = entity.attachedRigidbody;
            Vector3 LeftOrigin = entity.transform.position + entity.transform.TransformDirection(new Vector3(-entity.bounds.extents.x, 0));
            Vector3 RightOrigin = entity.transform.position + entity.transform.TransformDirection(new Vector3(entity.bounds.extents.x, 0));

            if (rb.velocity.x < 0)
            {
                Physics.Raycast(LeftOrigin, entity.transform.TransformDirection(Vector3.down), out hitInfo, entity.bounds.extents.y + 0.4f, Globals.Ground, QueryTriggerInteraction.Ignore);
            }
            else
            {
                Physics.Raycast(RightOrigin, entity.transform.TransformDirection(Vector3.down), out hitInfo, entity.bounds.extents.y + 0.4f, Globals.Ground, QueryTriggerInteraction.Ignore);
            }

            return hitInfo.collider == null;
        }

        /// <summary>
        /// Jump to position at specified angle.
        /// </summary>
        /// <param name="rb">Rigidbody to jump.</param>
        /// <param name="angle">Angle of jump.</param>
        /// <param name="target">Position to jump to.</param>
        public static void JumpToPosition(Collider col, Vector3 target, float maxVelocity = Mathf.Infinity)
        {
            Rigidbody rb = col.attachedRigidbody;

            Vector3 origin = col.transform.position - new Vector3(0, col.bounds.extents.y);
            float gravity = Physics.gravity.magnitude;
            float angleRad = CalculateJumpAngle(origin, target, 30f, 80f) * Mathf.Deg2Rad;

            Vector3 planarTarget = new Vector3(target.x, 0, target.z);
            Vector3 planarPostion = new Vector3(origin.x, 0, origin.z);

            float distance = Vector3.Distance(planarTarget, planarPostion);
            float yOffset = origin.y - target.y;

            float initialVelocity = 0f;
            float sqrtDenominator = distance * Mathf.Tan(angleRad) + yOffset;

            if (sqrtDenominator > 0)
            {
                initialVelocity = (1 / Mathf.Cos(angleRad)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / sqrtDenominator);
            }

            if (initialVelocity > 0)
            {
                Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angleRad), initialVelocity * Mathf.Cos(angleRad));

                float angleBetweenObjects = Vector3.SignedAngle(Vector3.forward, planarTarget - planarPostion, Vector3.up);

                Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity * rb.mass;
                finalVelocity += finalVelocity.normalized * 1.05f;

                finalVelocity = Vector3.ClampMagnitude(finalVelocity, maxVelocity);

                rb.velocity = Vector3.zero;
                rb.AddForce(finalVelocity, ForceMode.Impulse);
            }
        }

        static float CalculateJumpAngle(Vector3 origin, Vector3 target, float minJumpAngle, float maxJumpAngle)
        {
            float jumpAngle = minJumpAngle;

            if (target.y > origin.y)
            {
                Vector3 projectedVector = Vector3.ProjectOnPlane(target - origin, Vector3.up);

                float targetDirectionAngle = Vector3.Angle(target - origin, projectedVector);

                if (targetDirectionAngle < minJumpAngle)
                {
                    jumpAngle = minJumpAngle;
                }
                else if (targetDirectionAngle > maxJumpAngle)
                {
                    jumpAngle = maxJumpAngle;
                } 
                else
                {
                    jumpAngle = targetDirectionAngle;
                }

                while (jumpAngle < maxJumpAngle && Physics.Raycast(origin, Quaternion.AngleAxis(jumpAngle, Vector3.Cross(projectedVector, Vector3.up)) * projectedVector, Vector3.Distance(target, origin), Globals.Ground, QueryTriggerInteraction.Ignore))
                {
                    jumpAngle++;
                }
            }

            return jumpAngle + 10f;
        }

        public static bool IsGrounded(Collider col)
        {
            return Physics.Raycast(col.transform.position, Vector3.down, col.bounds.extents.y + 0.4f, Globals.Ground, QueryTriggerInteraction.Ignore);
        }

        public static bool TryGetGround(Collider col, out RaycastHit groundInfo)
        {
            Physics.Raycast(col.transform.position, Vector3.down, out RaycastHit hitInfo, col.bounds.extents.y + 0.4f, Globals.Ground, QueryTriggerInteraction.Ignore); 
            groundInfo = hitInfo;
            return hitInfo.collider != null;
        }

        public static void ApplyHorizontalDrag(Rigidbody rb, float horizontalDrag)
        {
            rb.velocity = new Vector3(rb.velocity.x * (1 - Time.deltaTime * horizontalDrag), rb.velocity.y, rb.velocity.z * (1 - Time.deltaTime * horizontalDrag));
        }
    }
}
