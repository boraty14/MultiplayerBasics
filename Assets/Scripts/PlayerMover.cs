using System;
using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMover : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;

    private Camera _mainCamera;

    #region Server

    [Command]
    private void CmdMove(Vector3 position)
    {
        if (!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f,NavMesh.AllAreas)) return;
        agent.SetDestination(hit.position);
    }

    #endregion

    #region Client

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        _mainCamera = Camera.main;
    }

    [ClientCallback]
    private void Update()
    {
        if (!hasAuthority) return;
        if (!Input.GetMouseButtonDown(0)) return;
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) return;
        CmdMove(hit.point);
    }

    #endregion
    
    

}
