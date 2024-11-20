using System;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// Represents a collection of <see cref="EffectAnnotation"/> objects.
    /// </summary>
    public class EffectAnnotationCollection : IEnumerable<EffectAnnotation>
	{
        internal static readonly EffectAnnotationCollection Empty = new([]);

	    private readonly EffectAnnotation[] _annotations;

        internal EffectAnnotationCollection(EffectAnnotation[] annotations)
        {
            _annotations = annotations;
        }

        /// <summary>
        /// Gets the number of elements contained in the collection.
        /// </summary>
		public int Count
            => _annotations.Length;

        /// <summary>
        /// Retrieves the <see cref="EffectAnnotation"/> at the specified index in the collection.
        /// </summary>
        public EffectAnnotation this[int index]
            => _annotations[index];


        /// <summary>
        /// Retrieves a <see cref="EffectAnnotation"/> from the collection, given the name of the annotation.
        /// </summary>
        /// <param name="name">The name of the annotation to retrieve.</param>
        public EffectAnnotation this[string name]
        {
            get
            {
                var span = new ReadOnlySpan<EffectAnnotation>(_annotations);
				for (int i = 0; i < span.Length; ++i)
                {
					if (span[i].Name == name)
						return span[i];
				}
				return null;
			}
        }

        /// <inheritdoc/>
		public IEnumerator<EffectAnnotation> GetEnumerator()
            => ((IEnumerable<EffectAnnotation>)_annotations).GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            => _annotations.GetEnumerator();
	}
}
