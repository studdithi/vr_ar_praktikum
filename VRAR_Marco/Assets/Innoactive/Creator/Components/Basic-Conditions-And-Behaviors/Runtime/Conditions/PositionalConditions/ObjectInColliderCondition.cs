using System.Runtime.Serialization;
using Innoactive.Creator.Core.Attributes;
using Innoactive.Creator.Core.SceneObjects;
using Innoactive.Creator.Core.Properties;
using Innoactive.Creator.Core.Utils;
using Innoactive.Creator.Core.Validation;

namespace Innoactive.Creator.Core.Conditions
{
    /// <summary>
    /// Condition which is completed when `TargetObject` gets inside `TriggerProperty`'s collider.
    /// </summary>
    [DataContract(IsReference = true)]
    public class ObjectInColliderCondition : Condition<ObjectInColliderCondition.EntityData>
    {
        /// <summary>
        /// The "object in collider" condition's data.
        /// </summary>
        [DisplayName("Move Object into Collider")]
        [DataContract(IsReference = true)]
        public class EntityData : IObjectInTargetData
        {
            /// <summary>
            /// The collider with trigger to enter.
            /// </summary>
            [DataMember]
            [DisplayName("Object to collide into")]
#if CREATOR_PRO
            [CheckForCollider]
            [ColliderAreTrigger]
#endif
            public ScenePropertyReference<ColliderWithTriggerProperty> TriggerProperty { get; set; }

            /// <summary>
            /// The object that has to enter the collider.
            /// </summary>
            [DataMember]
            [DisplayName("Target object")]
            public SceneObjectReference TargetObject { get; set; }

            /// <inheritdoc />
            public bool IsCompleted { get; set; }

            /// <inheritdoc />
            [DataMember]
            [HideInTrainingInspector]
            public string Name { get; set; }

            /// <inheritdoc />
#if CREATOR_PRO
            [OptionalValue]
#endif
            [DataMember]
            [DisplayName("Required seconds inside")]
            public float RequiredTimeInside { get; set; }

            /// <inheritdoc />
            public Metadata Metadata { get; set; }
        }

        public ObjectInColliderCondition() : this("", "")
        {
        }

        // ReSharper disable once SuggestBaseTypeForParameter
        public ObjectInColliderCondition(ColliderWithTriggerProperty targetPosition, ISceneObject targetObject, float requiredTimeInTarget = 0, string name = null)
            : this(TrainingReferenceUtils.GetNameFrom(targetPosition), TrainingReferenceUtils.GetNameFrom(targetObject), requiredTimeInTarget, name)
        {
        }

        public ObjectInColliderCondition(string targetPosition, string targetObject, float requiredTimeInTarget = 0, string name = "Move Object into Collider")
        {
            Data.TriggerProperty = new ScenePropertyReference<ColliderWithTriggerProperty>(targetPosition);
            Data.TargetObject = new SceneObjectReference(targetObject);
            Data.RequiredTimeInside = requiredTimeInTarget;
            Data.Name = name;
        }

        private class ActiveProcess : ObjectInTargetActiveProcess<EntityData>
        {
            public ActiveProcess(EntityData data) : base(data)
            {
            }

            /// <inheritdoc />
            protected override bool IsInside()
            {
                return Data.TriggerProperty.Value.IsTransformInsideTrigger(Data.TargetObject.Value.GameObject.transform);
            }
        }

        private class EntityAutocompleter : Autocompleter<EntityData>
        {
            public EntityAutocompleter(EntityData data) : base(data)
            {
            }

            /// <inheritdoc />
            public override void Complete()
            {
                Data.TriggerProperty.Value.FastForwardEnter(Data.TargetObject.Value);
            }
        }

        /// <inheritdoc />
        public override IProcess GetActiveProcess()
        {
            return new ActiveProcess(Data);
        }

        /// <inheritdoc />
        protected override IAutocompleter GetAutocompleter()
        {
            return new EntityAutocompleter(Data);
        }
    }
}
