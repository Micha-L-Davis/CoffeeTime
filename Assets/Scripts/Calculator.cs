using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace eMeLDi.CoffeeTime
{
    public class Calculator : MonoBehaviour
    {
        [Header("Input Fields")]
        [SerializeField]
        TMP_InputField _ratioInputA;
        [SerializeField]
        TMP_InputField _ratioInputB;
        [SerializeField]
        TMP_InputField _numCupsInput;
        [SerializeField]
        TMP_InputField _coffeeGramsInput;
        [SerializeField]
        TMP_InputField _waterGramsInput;

        [Header("Alert Text")]
        [SerializeField]
        TMP_Text _bloomText;
        [SerializeField]
        TMP_Text _firstPourText;
        [SerializeField]
        TMP_Text _secondPourText;

        [Header("Raw Data")]
        [SerializeField]
        float _ratioQuantityA;
        [SerializeField]
        float _ratioQuantityB;
        float _numCups;
        [SerializeField]
        float _coffeeGrams;
        [SerializeField]
        float _waterGrams;
        [SerializeField, Space, Tooltip("Represents volume in ml. Default is 356ml (12oz).")]
        float _cupVolume = 356.0f;

        private void Start()
        {
            ReadValues();
        }

        private void ReadValues()
        {
            _ratioQuantityA = float.Parse(_ratioInputA.text);
            _ratioQuantityB = float.Parse(_ratioInputB.text);
            _numCups = float.Parse(_numCupsInput.text);
            _coffeeGrams = float.Parse(_coffeeGramsInput.text);
            _waterGrams = float.Parse(_waterGramsInput.text);
        }

        public void WaterPerCup()
        {
            Debug.Log("Changing number of cups.");
            ReadValues();
            _waterGrams = _numCups * _cupVolume;
            _waterGramsInput.text = System.Convert.ToString(_waterGrams);
            var firstPortion = _waterGrams * 0.6f;
            _firstPourText.text = "First Pour (to " + firstPortion + ")";
            _secondPourText.text = "Second Pour (to " + _waterGrams + ")";
        }

        public void UpdateCoffeeGrounds()
        {
            Debug.Log("Updating Coffee Amount.");
            ReadValues();
            _coffeeGrams = _waterGrams * _ratioQuantityA / _ratioQuantityB;
            _coffeeGramsInput.text = System.Convert.ToString(Mathf.RoundToInt(_coffeeGrams));
            var bloomPortion = _coffeeGrams * 2.5;
            _bloomText.text = "Bloom (to " + bloomPortion + ")";
        }

        public void UpdateWater()
        {
            Debug.Log("Updating Water Amount.");
            ReadValues();
            _waterGrams = _coffeeGrams * _ratioQuantityB / _ratioQuantityA;
            _waterGramsInput.text = System.Convert.ToString(_waterGrams);
            var firstPortion = _waterGrams * 0.6f;
            _firstPourText.text = "First Pour (to " + firstPortion + ")";
            _secondPourText.text = "Second Pour (to " + _waterGrams + ")";
        }
    }
}
