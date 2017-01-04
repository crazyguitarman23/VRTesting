using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandController : MonoBehaviour 
{
	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input ((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;

	HashSet<InteractableItem> objectsHoveringOver = new HashSet<InteractableItem>();

	public InteractableItem closestItem;
	public InteractableItem interactingItem;

	void Start () 
	{
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	void Update () 
	{

		if (controller == null) 
		{
			Debug.Log ("controller not initialized");
			return;
		}

		if (controller.GetPressDown (gripButton))
		{
			float minDistance = float.MaxValue;

			float distance;
			foreach (InteractableItem item in objectsHoveringOver) 
			{
				distance = (item.transform.position - transform.position).sqrMagnitude;
				if (distance < minDistance) 
				{
					minDistance = distance;
					closestItem = item;
				}
			}

			interactingItem = closestItem;
			closestItem = null;

			if (interactingItem)
			{
				if (interactingItem.IsInteracting())
				{
					interactingItem.EndInteraction (this);
				}

				interactingItem.BeginInteraction (this);
			}
		}

		if (controller.GetPressUp (gripButton) && interactingItem != null)
		{
			interactingItem.EndInteraction (this);
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		InteractableItem collidedItem = collider.GetComponent<InteractableItem> ();
		if (collidedItem)
		{
			objectsHoveringOver.Add (collidedItem);
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		InteractableItem collidedItem = collider.GetComponent<InteractableItem> ();
		if (collidedItem) 
		{
			objectsHoveringOver.Remove (collidedItem);
		}
	}
}
