using System;
using System.Collections;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

namespace _Scripts.Character_Creation
{
    public class CharacterCreator : MonoBehaviour
    {
        [SerializeField] private GameObject selectionPanel;
        [SerializeField] private GameObject optionsPanel;
        [SerializeField] private Button closePanel;

        [SerializeField] private Button genderBtn;
        [SerializeField] private Button hairBtn;
        [SerializeField] private Button facialBtn;

        [SerializeField] private Material characterShader;

        /*[SerializeField] private Button hairColorBtn;
        [SerializeField] private Button skinColorBtn;*/

        [SerializeField] private GameObject maleChar;
        [SerializeField] private GameObject femaleChar;
        
        [SerializeField] private GameObject hairParent;
        

        [SerializeField] private CinemachineVirtualCamera cam1;
        [SerializeField] private CinemachineVirtualCamera cam2;
        private bool _cameraChanged;
        private void Awake()
        {
            DOTween.Init();
            
            //closePanel.onClick.AddListener(ClosePanel);
            
            genderBtn.onClick.AddListener(ShowGender);
            hairBtn.onClick.AddListener(ShowHair);
            facialBtn.onClick.AddListener(ShowFacial);
        }

        #region Button Functions

        private void ClosePanel()
        {
            selectionPanel.transform.DOScale(Vector3.one, 0.5f);
        }

        private void ShowGender()
        {
            if(!_cameraChanged)
                StartCoroutine(nameof(ShowPanel));
            else
            {
                DisablePanels();
                optionsPanel.transform.GetChild(1).gameObject.SetActive(true);
            }

        }

        private IEnumerator ShowPanel()
        {
            cam2.m_Priority++;
            optionsPanel.transform.GetChild(1).gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(2f);
            optionsPanel.transform.DOScale(Vector3.one, 0.5f);
            _cameraChanged = true;
        }
        private void ShowHair()
        {
            DisablePanels();
            optionsPanel.transform.GetChild(2).gameObject.SetActive(true);
        }
        
        private void ShowFacial()
        {
            DisablePanels();
            optionsPanel.transform.GetChild(3).gameObject.SetActive(true);
        }

        #endregion

        public void SelectMale()
        {
            maleChar.SetActive(true);
            
            femaleChar.SetActive(false);
        }
        
        public void SelectFemale()
        {
            femaleChar.SetActive(true);
            
            maleChar.SetActive(false);
        }

        private void DisablePanels()
        {
            var count = optionsPanel.transform.childCount;
            for (var i = 1; i < count ; i++)
            {
                optionsPanel.transform.GetChild(i).gameObject.SetActive(false);
            }
            
        }
    }
}
