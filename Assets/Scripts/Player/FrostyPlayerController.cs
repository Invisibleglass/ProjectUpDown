using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostyPlayerController : BasePlayerController
{
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject body2;
    [SerializeField] private GameObject body3;
    [SerializeField] private GameObject body4;

    private float delay = 0.1f;

    IEnumerator FollowHead()
    {
        float currentPosition = this.gameObject.transform.position.y;
        float bodyPosition = body.transform.position.y;
        float body2Position = body.transform.position.y;
        float body3Position = body.transform.position.y;

        yield return new WaitForSeconds(delay);
        body.transform.position = new Vector3 (body.transform.position.x, currentPosition, body.transform.position.z);
        body2.transform.position = new Vector3(body2.transform.position.x, bodyPosition, body2.transform.position.z);
        yield return new WaitForSeconds(delay);
        body3.transform.position = new Vector3(body3.transform.position.x, body2Position, body3.transform.position.z);
        yield return new WaitForSeconds(delay);
        body4.transform.position = new Vector3(body4.transform.position.x, body3Position, body4.transform.position.z);
    }

    protected override void Update()
    {
        base.Update();

        StartCoroutine(FollowHead());
    }
}
