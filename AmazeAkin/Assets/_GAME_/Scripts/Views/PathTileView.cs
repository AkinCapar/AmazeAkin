using System;
using System.Collections;
using System.Collections.Generic;
using Amaze.Controllers;
using Amaze.Settings;
using Amaze.Utilities;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Amaze.Views
{
    public class PathTileView : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;
        [SerializeField] private float _jumpPower;
        [SerializeField] private float _jumpSpeed;
        private bool _isJumped;
        private bool _isPainted;
        
        #region Injection

        private LevelController _levelController;

        [Inject]
        private void Construct(LevelController levelController)
        {
            _levelController = levelController;
        }

        #endregion

        private void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.Tags.Ball) && !_isJumped)
            {
                _isJumped = true;
                Vector3 position = transform.localPosition;
                gameObject.transform.DOLocalJump(position, _jumpPower, 1, _jumpSpeed);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.Tags.Ball) && !_isPainted)
            {
                _isPainted = true;
                _levelController.ManagePaintedTile(this);
                _meshRenderer.material.DOColor(Color.magenta, _jumpSpeed);
            }
        }
    }
}
