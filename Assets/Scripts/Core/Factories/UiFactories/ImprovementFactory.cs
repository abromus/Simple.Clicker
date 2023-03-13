using Clicker.Game.Screens;
using UnityEngine;

namespace Clicker.Core.Factories
{
    public sealed class ImprovementFactory : UiFactory
    {
        [SerializeField] private Improvement _improvementPrefab;

        public override UiFactoryType UiFactoryType => UiFactoryType.ImprovementFactory;

        public override MonoBehaviour Create(BaseOptions options, Transform container)
        {
            var improvement = Instantiate(_improvementPrefab, container);

            improvement.Init(options as ImprovementOptions);

            return improvement;
        }
    }
}
