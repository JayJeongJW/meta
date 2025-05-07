using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StackMiniGame;

namespace StackMiniGame
{


    public class DestroyZone : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name.Equals("Rubble"))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
