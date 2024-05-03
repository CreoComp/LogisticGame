using Unity.VisualScripting;
using UnityEngine;

public class Point : MonoBehaviour
{
    public int Weight;

    private void OnDrawGizmos()
    {
        switch (Weight)
        {
            case 1:
                Gizmos.color = Color.yellow;

                break;

            case 2:
                Gizmos.color = Color.blue;
                break;

            case 3:
                Gizmos.color = Color.red;
                break;
        }
        Gizmos.DrawSphere(transform.position, Weight * 0.1f);
    }
}
