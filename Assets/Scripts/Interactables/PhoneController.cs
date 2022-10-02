using DG.Tweening;
using UnityEngine;

public class PhoneController : MonoBehaviour {

    [SerializeField] private Vector3 origPos = new Vector3(0f, 0.996000469f, -0.516754508f);
    [SerializeField] private Vector3 origRot = new Vector3(270.019775f, 0f, 0f);
    [SerializeField] private Vector3 pickupPos = new Vector3(1.67999995f, 1.19000006f, 4.05999994f);
    [SerializeField] private Vector3 pickupRot = new Vector3(356.150787f, 184.682541f, 90.2463837f);

    public void PickUp() {
        transform.DOLocalMove(pickupPos, 1.5f);
        transform.DOLocalRotate(pickupRot, 1.5f);
    }

    public void HangUp() {
        transform.DOLocalMove(origPos, 1.5f);
        transform.DOLocalRotate(origRot, 1.5f);
    }
}
