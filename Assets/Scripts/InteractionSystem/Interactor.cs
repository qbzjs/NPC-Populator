using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.05f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private InteractionPromptUI _interactionPromptUI;

    private readonly Collider[] _colliders = new Collider[3];

    [SerializeField] private int _numFound;

    private IInteractable _interactable;

    private void Update(){
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        if(_numFound > 0){
            _interactable = _colliders[0].GetComponent<IInteractable>();

            if(_interactable != null){
                _interactionPromptUI.SetUp(_interactable.InteractionPrompt);

                if(Keyboard.current.eKey.wasPressedThisFrame) _interactable.Interact(this);
            }
        } else {
            if(_interactable != null) _interactable = null;
            if(_interactionPromptUI.isDisplayed) _interactionPromptUI.Close();
        }
    }

    private void OnGizmosDrawn(){
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
