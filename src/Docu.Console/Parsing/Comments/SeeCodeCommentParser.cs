using System.Xml;
using Docu.Documentation;
using Docu.Documentation.Comments;
using Docu.Parsing.Model;

namespace Docu.Parsing.Comments
{
    internal class SeeCodeCommentParser : CommentParserBase
    {
        public IComment Parse(XmlNode content)
        {
            var referenceTarget = Identifier.FromString(content.Attributes["cref"].Value);
            IReferencable reference = null;

            if (referenceTarget is NamespaceIdentifier)
                reference = Namespace.Unresolved((NamespaceIdentifier)referenceTarget);
            else if (referenceTarget is TypeIdentifier)
                reference = DeclaredType.Unresolved((TypeIdentifier)referenceTarget, Namespace.Unresolved(referenceTarget.CloneAsNamespace()));
            else if (referenceTarget is MethodIdentifier)
                reference = Method.Unresolved((MethodIdentifier)referenceTarget);
            else if (referenceTarget is PropertyIdentifier)
                reference = Property.Unresolved((PropertyIdentifier)referenceTarget);
            else if (referenceTarget is EventIdentifier)
                reference = Event.Unresolved((EventIdentifier)referenceTarget);
            else if (referenceTarget is FieldIdentifier)
                reference = Field.Unresolved((FieldIdentifier)referenceTarget);

            return new See(reference);
        }
    }
}