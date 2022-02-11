using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Creator.Utilities
{
    /// <summary>
    /// Override Unity Editor labels for serialized properties
    /// </summary>
    /// <example>
    /// [LabelOverride( "Awesome Player" )] [SerializeField] Character character;
    /// </example>
    public class OverrideLabelAttribute : PropertyAttribute
    {
        /// <summary>
        /// Text to show
        /// </summary>
        private readonly string _label;
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="label">Name of property</param>
        public OverrideLabelAttribute ( string label )
        {
            this._label = label;
        }
 
#if UNITY_EDITOR
        /// <summary>
        /// Drawer for this override property
        /// </summary>
        [CustomPropertyDrawer( typeof(OverrideLabelAttribute) )]
        public class ThisPropertyDrawer : PropertyDrawer
        {
            /// <summary>
            /// Override property name in editor UI
            /// </summary>
            /// <param name="position">Position of label</param>
            /// <param name="property">Property</param>
            /// <param name="label">GUI content</param>
            public override void OnGUI ( Rect position , SerializedProperty property , GUIContent label )
            {
                try
                {
                    var propertyAttribute = this.attribute as OverrideLabelAttribute;
                    if( !IsItBloodyArrayTho( property ) )
                    {
                        if (propertyAttribute != null) label.text = propertyAttribute._label;
                    } else
                    {
                        if (propertyAttribute != null)
                            Debug.LogWarningFormat(
                                "{0}(\"{1}\") doesn't support arrays ",
                                nameof(OverrideLabelAttribute),
                                propertyAttribute._label
                            );
                    }
                    EditorGUI.PropertyField( position , property , label );
                } catch ( System.Exception ex ) { Debug.LogException( ex ); }
            }

            static bool IsItBloodyArrayTho ( SerializedProperty property  )
            {
                var path =  property.propertyPath;
                var dot = path.IndexOf('.');
                if( dot==-1 ) return false;
                var propName = path.Substring( 0 , dot );
                var p = property.serializedObject.FindProperty( propName );
                return p.isArray;
            }
        }
#endif
    }
}
