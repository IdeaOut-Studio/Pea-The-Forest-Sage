using System.Collections;
using UnityEngine;

namespace PeaTFS
{
    public class NotificationObject : MonoBehaviour
    {
        public Direction direction;

        private Vector3 startingPos;
        private Vector3 finalPos;


        private void Start()
        {
            StartCoroutine(ContinuousRotation());
        }

        public void Activate()
        {
            Vector3 _dir = CheckDirection(direction);

            //if(startingPos == transform.position)
            StartCoroutine(SmoothLerp(0.1f, _dir));
        }

        private Vector3 CheckDirection(Direction _direction)
        {
            Vector3 _dir;

            switch (_direction)
            {
                case Direction.up:
                    _dir = Vector3.up;
                    break;
                case Direction.down:
                    _dir = Vector3.down;
                    break;
                case Direction.left:
                    _dir = Vector3.left;
                    break;
                case Direction.right:
                    _dir = Vector3.right;
                    break;
                case Direction.forward:
                    _dir = Vector3.forward;
                    break;
                case Direction.backward:
                    _dir = Vector3.back;
                    break;
                default:
                    _dir = Vector3.forward;
                    break;

            }

            return _dir;
        }

        IEnumerator ContinuousRotation()
        {
            while (true)
            {
                transform.Rotate(Vector3.one * 1f);
                yield return new WaitForSeconds(0.01f);
            }
        }

        private IEnumerator SmoothLerp(float time, Vector3 _dir)
        {
            startingPos = transform.position;
            finalPos = transform.position + (_dir * 1);

            float elapsedTime = 0;

            while (elapsedTime < time)
            {
                transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }


        public void DestroyNotif()
        {
            StartCoroutine(DeactiveObject());
        }
        private IEnumerator DeactiveObject()
        {
            GetComponent<MeshRenderer>().enabled = false;
            yield return new WaitForSeconds(0.2f);
            Destroy(gameObject);
        }

    }

}

[System.Serializable]
public enum Direction
{
    up, down, left, right, forward, backward
}