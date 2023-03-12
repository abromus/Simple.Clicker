﻿using Clicker.Core;
using UnityEngine;

namespace Clicker.Game
{
    public class ImprovementFactory : MonoBehaviour
    {
        [SerializeField] private Improvement _improvementPrefab;

        public Improvement Create(GameWorld world, LocalizationSystem localizationSystem, ImprovementData improvementData, Transform container, int businessId, int improvementId)
        {
            var improvement = Instantiate(_improvementPrefab, container);

            improvement.Init(world, localizationSystem, improvementData, businessId, improvementId);

            return improvement;
        }
    }
}
