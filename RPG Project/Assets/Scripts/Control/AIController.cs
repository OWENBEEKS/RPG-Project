using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chasingDistance = 5f;
        void Update()
        {
            if (DistanceToPlayer() <= chasingDistance)
            {
                print("PLAYER is being chased by " + gameObject.name);
            }
        }

        private float DistanceToPlayer()
        {
            GameObject player = GameObject.FindWithTag("Player");
            return Vector3.Distance(player.transform.position, transform.position);
        }
    }
}

